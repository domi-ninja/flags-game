using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Runtime.InteropServices.Marshalling;
using System.Text.Json;
using flags_game;
using flags_game.Models;
using flags_game.Pages.Shared.Components.ColorTagList;
using flags_game.Pages.Shared.Components.ColorUsage;
using flags_game.Pages.Shared.Components.FlagColormapComponent;
using flags_game.Pages.Shared.Components.FlagList;
using flags_game.Pages.Shared.Components.FlagListAnswerable;
using flags_game.Pages.Shared.Components.TagsList;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;

namespace CoreFlags
{
    public class ColorController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly FlagAppDbContext dbContext;

        public ColorController(IWebHostEnvironment webHostEnvironment, FlagAppDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.dbContext = dbContext;


        }


        public void LoadCombos()
        {
            if (combosCache == null) {  
                this.combos = new List<HashSet<COLOR>>();
                GetCombosRec(combos, 2, 5, new());
                combosCache = this.combos;
            } else
            {
                this.combos = combosCache;
            }
        }

        public (List<FlagColor>, List<Flag>) LoadData()
        {
            var flagColor = dbContext.flagColor
                .ToList();

            var flags = this.dbContext.flags
                .Include(f => f.colorTags)
                    .ThenInclude(ft => ft.FlagColor)
                .ToList();

            return (flagColor, flags);
        }

        public enum COLOR
        {
            WHITE,
            BLACK,
            RED,
            GREEN,
            BLUE,
            YELLOW,
            ORANGE,
            //VIOLET,
            LIGHTBLUE,
        }

        public static Dictionary<Color, COLOR> hueToColorMap = new()
        {
            {  Color.Red, COLOR.RED },
            {  Color.Green, COLOR.GREEN },
            {  Color.FromArgb(52, 180, 50), COLOR.GREEN },
            {  Color.FromArgb(0, 122, 94), COLOR.GREEN },
            {  Color.Blue, COLOR.BLUE },
            {  Color.FromArgb(0, 60, 120), COLOR.BLUE },
            {  Color.FromArgb(94, 180, 230), COLOR.LIGHTBLUE },
            {  Color.Turquoise, COLOR.LIGHTBLUE },
            {  Color.FromArgb(255, 175, 0), COLOR.YELLOW },
            {  Color.FromArgb(255, 135, 0), COLOR.ORANGE },
            //{  Color.Purple, COLOR.VIOLET },
        };
        private static List<HashSet<COLOR>> combosCache;

        public List<HashSet<COLOR>> combos { get; private set; }

        public static Dictionary<Color, long> GetImageColorsSlow(System.Drawing.Bitmap bmp)
        {
            var result = new Dictionary<Color, long>();

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color pixelColor = bmp.GetPixel(x, y);

                    if (result.ContainsKey(pixelColor))
                    {
                        result[pixelColor]++;
                    }
                    else
                    {
                        result.Add(pixelColor, 1);
                    }
                }
            }

            return result;
        }

        string ColorToHex(Color color) => "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");

        private (List<Dictionary<Color, Color>>, Dictionary<string, HashSet<COLOR>>) SyncFlagColors(int count, int offset)
        {
            List<Dictionary<Color, Color>> debugResult = new();
            var semanticResult = new Dictionary<string, HashSet<COLOR>>();

            foreach (var flag in this.dbContext.flags.Skip(offset).Take(count))
            {
                var request = WebRequest.Create("http://localhost:5209/" + flag.url);
                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    Bitmap image = Image.FromStream(stream) as Bitmap;

                    var mapDebugDict = new Dictionary<Color, Color>();
                    debugResult.Add(mapDebugDict);
                    var enum_colorsList = new HashSet<COLOR>();
                    semanticResult.Add(flag.url, enum_colorsList);

                    var clampedColors = new Dictionary<Color, long>()
                    {
                    };
                    var colors = GetImageColorsSlow(image);
                    colors = colors.Where(colors => colors.Value > 0.01 * image.Size.Width * image.Size.Height )
                        .OrderBy(colors => colors.Value)
                        .ToDictionary(colors => colors.Key, colors => colors.Value);
                    foreach (var (color, amount) in colors)
                    {
                        var hue = color.GetHue();
                        var saturation = color.GetSaturation();
                        var brightness = color.GetBrightness();
                        Color clampedColor;
                        COLOR enum_color;
                        if (saturation < 0.2)
                        {
                            if (brightness < 0.35)
                            {
                                clampedColor = Color.Black;
                                enum_color = COLOR.BLACK;
                            }
                            else if (brightness > 0.7)
                            {
                                clampedColor = Color.White;
                                enum_color = COLOR.WHITE;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            var colorsByHowClose = hueToColorMap.OrderBy(kvp =>
                            {
                                double a = Math.Abs(kvp.Key.GetHue() - hue + 360);
                                double b = Math.Abs(kvp.Key.GetHue() - hue - 360);
                                double c = Math.Abs(kvp.Key.GetHue() - hue);
                                var hueDistance = Math.Min(Math.Min(a, b), c);
                                var lightDist = Math.Abs(kvp.Key.GetBrightness() - brightness) * 360;
                                return Math.Sqrt( hueDistance * hueDistance + lightDist * lightDist);
                            });
                            clampedColor = colorsByHowClose.First().Key;
                            enum_color = colorsByHowClose.First().Value;
                        }

                        mapDebugDict.Add(color, clampedColor);
                        enum_colorsList.Add(enum_color);

                        if (clampedColors.ContainsKey(clampedColor))
                        {
                            clampedColors[clampedColor] += amount;
                        }
                        else
                        {
                            clampedColors.Add(clampedColor, amount);
                        }
                    }
                }
            }

            return (debugResult, semanticResult);
        }

        [HttpGet]
        public IActionResult Analyze(int count = 5, int offset=0)
        {

            var flagColors = this.dbContext.flagColor.ToList();
            foreach (var color in Enum.GetValues(typeof(COLOR)))
            {
                string rgb = ColorToHex(hueToColorMap.FirstOrDefault(kvp => kvp.Value == (COLOR)color).Key);
                string colorName = color.ToString().ToLower();
                if (!flagColors.Any(fc => fc.name == colorName))
                {
                    this.dbContext.flagColor.Add(new FlagColor()
                    {
                        name = colorName,
                        rgb = rgb,
                    });
                }
            }
            dbContext.SaveChanges();
            flagColors = this.dbContext.flagColor.ToList();


            var (colorResult, updateDbResult) = SyncFlagColors(count, offset);

            foreach (var (flagUrl, colors) in updateDbResult)
            {
                var flag = this.dbContext.flags.FirstOrDefault(f => f.url == flagUrl);

                // purge existing color tags and recreate
                foreach (var oldColorTag in this.dbContext.colorTags.Where(ct => ct.FlagId == flag.Id))
                {
                    this.dbContext.colorTags.Remove(oldColorTag);
                }

                foreach (var color in colors)
                {
                    string colorName = color.ToString().ToLower();
                    var flagColor = this.dbContext.flagColor.FirstOrDefault(fc => fc.name == colorName);
                    this.dbContext.colorTags.Add(new ColorTag()
                    {
                        FlagId = flag.Id,
                        FlagColorId = flagColor.Id,
                    });
                }
                this.dbContext.SaveChanges();
            }

            return ViewComponent(typeof(FlagColormapViewComponent), new FlagColormapModel()
            {
                colorResult = colorResult,
                flags = this.dbContext.flags.Skip(offset).Take(count).ToList(),
            });
        }

        private void GetCombosRec(List<HashSet<COLOR>> combResult, int min, int max, List<COLOR> sofar)
        {
            if (sofar.Count > max)
            {
                return;
            }

            if (sofar.Count >= min)
            {
                if (!combResult.Any( v => v.SetEquals( new HashSet<COLOR>(sofar) )))
                {
                    combResult.Add(new HashSet<COLOR>(sofar));
                }
            }

            foreach (var color in Enum.GetValues<COLOR>())
            {
                if (sofar.Contains(color))
                {
                    continue;
                }
                else
                {
                    GetCombosRec(combResult, min, max, new List<COLOR>(sofar) { color });
                }

            }
        }

        public IActionResult RelevantCombos( TagSearchModel tagSearchModel)
        {
            var (flagColors, flags) = LoadData();
            LoadCombos();

            var relevantComboList = new List<FlagComboTag>();
            foreach (var c in this.combos)
            {
                var matchingFlags = flags.Where(f =>
                {
                    var flagColors = f.colorTags.Select(ct => ct.FlagColor).Select(fc => fc.name);
                    if ( flagColors.Count() != c.Count ) return false;
                    return c.All(color => flagColors.Contains(color.ToString().ToLower()));
                }).ToList();

                var colors = c.Select(col => new ColorPair()
                {
                    color = flagColors.FirstOrDefault(fc => fc.name == col.ToString().ToLower()).rgb,
                    colorId = flagColors.FirstOrDefault(fc => fc.name == col.ToString().ToLower()).Id,
                });

                if ( matchingFlags.Count < 3 )
                {
                    continue;
                } 

                relevantComboList.Add(new FlagComboTag()
                {
                    colors = colors,
                    flagsCount = matchingFlags.Count,
                    flags = matchingFlags.ToArray(),
                });
            }

            relevantComboList = relevantComboList
                .OrderBy(rc => rc.colors.Count()  )
                .OrderByDescending( rc => rc.flagsCount )
                .ToList();

            return ViewComponent(typeof(ColorTagListViewComponent), new ColorTagListModel()
            {
               tags = relevantComboList,
               TagColorCss = tagSearchModel.TagColorCss,
               TagParamsStr = tagSearchModel.TagParamsStr,
            });
        }

        [HttpGet]
        public IActionResult Combinations()
        {
            //var colorResult = SyncFlagColors(count);
            LoadCombos();
            var combResult = new List<CombResult>();
            foreach (var combo in combos)
            {
                combResult.Add(new CombResult()
                {
                    colors = combo,
                });
            }
            var (_, colorResult) = SyncFlagColors(-1, 0);
            foreach (var flagColorSet in colorResult)
            {
                var match = combResult.Where(combo =>
                {
                    return combo.colors.SetEquals(flagColorSet.Value);
                }).FirstOrDefault();
                if (match != null)
                {
                    match.flagUrls.Add(flagColorSet.Key);
                }
            }
            return ViewComponent(typeof(ColorUsageViewComponent), new ColorUsageModel()
            {
                combResult = combResult,
            });
        }


    }

    public class CombResult
    {
        public List<string> flagUrls { get; internal set; } = new();
        public HashSet<ColorController.COLOR> colors { get; internal set; }
    }
}