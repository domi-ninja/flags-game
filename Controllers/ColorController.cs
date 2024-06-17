using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Runtime.InteropServices.Marshalling;
using System.Text.Json;
using flags_game;
using flags_game.Models;
using flags_game.Pages.Shared.Components.ColorUsage;
using flags_game.Pages.Shared.Components.FlagColormapComponent;
using flags_game.Pages.Shared.Components.FlagList;
using flags_game.Pages.Shared.Components.FlagListAnswerable;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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


        public (List<Tag>, List<Flag>) LoadData(int? tagId)
        {
            var tags = dbContext.tags
                .Include(t => t.flagTags)
                .OrderBy(r => r.name)
                .ToList();
            var flags = this.dbContext.flags
                .Include(f => f.flagTags)
                    .ThenInclude(ft => ft.Tag)
                .Where(f =>
                    tagId.HasValue ? (tagId == -1 ? !f.flagTags.Any() : f.flagTags.Any(ft => ft.TagId == tagId)) : f.population > 1000000
                 )
                .OrderBy(r => r.population)
                .Reverse()
                .ToList();

            return (tags, flags);
        }



        public enum COLOR
        {
            WHITE,
            BLACK,
            RED,
            GREEN,
            BLUE,
            YELLOW,
            //ORANGE,
        }

        public static Dictionary<Color, COLOR> hueToColorMap = new()
        {
            {  Color.Red, COLOR.RED },
            {  Color.Green, COLOR.GREEN },
            {  Color.FromArgb(0, 255, 148), COLOR.GREEN },
            {  Color.Blue, COLOR.BLUE },
            {  Color.FromArgb(0, 148, 255), COLOR.BLUE },
            {  Color.FromArgb(255, 175, 0), COLOR.YELLOW },
            //{  Color.FromArgb(255, 135, 0), COLOR.ORANGE },
            //{  Color.Turquoise, COLOR.LIGHT_BLUE },
        };


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


        private (List<Dictionary<Color, Color>>, Dictionary<string, HashSet<COLOR>>) SyncFlagColors(int count)
        {
            //var flagColors = this.dbContext.flagColor.ToList();
            //foreach (var color in Enum.GetValues(typeof(COLOR)))
            //{
            //    string rgb = ColorToHex(hueToColorMap.FirstOrDefault(kvp => kvp.Value == (COLOR)color).Key);
            //    string colorName = color.ToString().ToLower();
            //    if (!flagColors.Any(fc => fc.name == colorName))
            //    {
            //        this.dbContext.flagColor.Add(new FlagColor()
            //        {
            //            name = colorName,
            //            rgb = rgb,
            //        });
            //    }
            //}
            //dbContext.SaveChanges();
            //flagColors = this.dbContext.flagColor.ToList();

            List<Dictionary<Color, Color>> debugResult = new();
            var semanticResult = new Dictionary<string, HashSet<COLOR>>();

            foreach (var flag in this.dbContext.flags)
            {
                count--;
                if (count == 0)
                {
                    break;
                }
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
                    colors = colors.Where(colors => colors.Value > 100)
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
                                return Math.Min(Math.Min(a, b), c);
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

                    foreach (var cc in clampedColors)
                    {

                    }
                }
            }

            return (debugResult, semanticResult);
        }

        [HttpGet]
        public IActionResult Analyze(int count = 5)
        {
            var (colorResult, _) = SyncFlagColors(count);
            return ViewComponent(typeof(FlagColormapViewComponent), new FlagColormapModel()
            {
                colorResult = colorResult,
                flags = this.dbContext.flags.ToList(),
            });
        }


        private void GetCombosRec(List<HashSet<COLOR>> combResult, int min, int max, List<COLOR> sofar)
        {
            if (sofar.Count >= max)
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

        [HttpGet]
        public IActionResult Combinations()
        {
            //var colorResult = SyncFlagColors(count);
            var combos = new List<HashSet<COLOR>>();
            GetCombosRec(combos, 2, 4, new());
            var combResult = new List<CombResult>();
            foreach (var combo in combos)
            {
                combResult.Add(new CombResult()
                {
                    colors = combo,
                });
            }
            var (_, colorResult) = SyncFlagColors(-1);
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