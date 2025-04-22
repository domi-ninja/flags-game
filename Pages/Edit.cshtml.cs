using System.Diagnostics;
using flags_game.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using flags_game.Data;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Net;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO.Pipelines;
using Microsoft.Identity.Client;

using flags_game;
using flags_game.Pages.Shared.Components.FlagColormapComponent;
namespace flags_game.Pages
{
    public class EditModel : PageModel
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly FlagAppDbContext dbContext;
        private readonly ILogger<EditModel> logger;

        public EditModel(IWebHostEnvironment webHostEnvironment, FlagAppDbContext dbContext, ILogger<EditModel> logger)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.dbContext = dbContext;
            this.logger = logger;

        }

        public List<Tag> Tags { get; set; } = new List<Tag>();
        public List<Flag> Flags { get; set; } = new List<Flag>();
        public List<ColorTag> ColorTags { get; set; } = new List<ColorTag>();

        public string Logs { get; set; } = "";

        public override string ToString()
        {
            return JsonSerializer.Serialize(new
            {
                Tags,
                Flags,
                Logs

            }, new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
            });
        }

        public void LoadData()
        {
            this.Tags = dbContext.tags
                .Include(t => t.flagTags)
                    .ThenInclude(ft => ft.Flag)
                .OrderBy(r => r.name)
                .ToList();
            this.Flags = this.dbContext.flags
                .Include(f => f.flagTags)
                //.ThenInclude( ft => ft.Tag )
                .Include(f => f.colorTags)
                    .ThenInclude(ft => ft.FlagColor)
                .OrderByDescending(r => r.population)
                .ToList();
        }

        public ActionResult OnPostTag(string newTagName)
        {
            if (this.dbContext.tags.Any(t => t.name == newTagName) || String.IsNullOrEmpty(newTagName))
            {
            }
            else
            {
                this.dbContext.tags.Add(new Tag()
                {
                    name = newTagName
                });
                this.dbContext.SaveChanges();
                LoadData();
            }

            return Redirect(Request.Path);
        }


        public void OnGet()
        {
            LoadData();
        }

        // public void OnReloadFlags()
        // {
        //     this.Flags = SyncFlags();
        // }

        public void OnPostSyncFlags()
        {
            this.Logs = SyncFlags();
            // this.Logs += SyncFlagColors();
            LoadData();
            // return Redirect(Request.Path);
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
            LIGHT_BLUE,
        }

        public Dictionary<Color, COLOR> hueToColorMap = new()
        {
            {  Color.Red, COLOR.RED },
            {  Color.Green, COLOR.GREEN },
            {  Color.FromArgb(255, 148, 0), COLOR.GREEN },
            {  Color.Blue, COLOR.BLUE },
            {  Color.FromArgb(255, 175, 0), COLOR.YELLOW },
            {  Color.FromArgb(255, 135, 0), COLOR.ORANGE },
            {  Color.Turquoise, COLOR.LIGHT_BLUE },
        };

        

        private string ColorToHex( Color color ) => "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
        private List<Dictionary<Color, Color>> SyncFlagColors()
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

            List<Dictionary<Color, Color>> result = new();

            foreach (var flag in this.dbContext.flags)
            {
                var request = WebRequest.Create("http://localhost:5209/" + flag.url);
                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    Bitmap image = Image.FromStream(stream) as Bitmap;

                    var mapDebugDict = new Dictionary<Color, Color>();
                    result.Add(mapDebugDict);

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
                        if (saturation < 0.35)
                        {
                            if (brightness < 0.2)
                            {
                                clampedColor = Color.Black;
                            }
                            else if (brightness > 0.8)
                            {
                                clampedColor = Color.White;
                            }
                            else
                            {
                                throw new Exception("Gray");
                            }
                        }
                        else
                        {
                            var colorsByHowClose = hueToColorMap.OrderBy(kvp => Math.Abs(kvp.Key.GetHue() - hue));
                            clampedColor = colorsByHowClose.First().Key;
                        }

                        mapDebugDict.Add(color, clampedColor);
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

            return result;
        }

        public static Dictionary<Color, long> GetImageColorsSlow(System.Drawing.Bitmap bmp)
        {
            var result = new Dictionary<Color, long>();

            for (int x = 0; x< bmp.Width; x++)
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

        public static Dictionary<Color, long> GetImageColors(System.Drawing.Bitmap bmp)
        {
            var result = new Dictionary<Color, long>();

            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);

            System.Drawing.Imaging.BitmapData bmpData =
                bmp.LockBits(rect,
                    System.Drawing.Imaging.ImageLockMode.ReadOnly,
                    bmp.PixelFormat);

            IntPtr ptr = bmpData.Scan0;

            int bytes = bmpData.Stride * bmp.Height;
            byte[] rgbValues = new byte[bytes];

            System.Runtime.InteropServices.Marshal.Copy(ptr,
                           rgbValues, 0, bytes);

            byte red = 0;
            byte green = 0;
            byte blue = 0;

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    //See the link above for an explanation 
                    //of this calculation
                    int position = (y * bmpData.Stride) + (x * Image.GetPixelFormatSize(bmpData.PixelFormat) / 8);
                    try
                    {
                        blue = rgbValues[position];
                        green = rgbValues[position + 1];
                        red = rgbValues[position + 2];
                    }
                    catch (System.IndexOutOfRangeException e)
                    {

                    }

                    if (result.ContainsKey(Color.FromArgb(red, green, blue)))
                    {
                        result[Color.FromArgb(red, green, blue)]++;
                    }
                    else
                    {
                        result.Add(Color.FromArgb(red, green, blue), 1);
                    }
                }
            }
            bmp.UnlockBits(bmpData);

            return result;
        }

        private string SyncFlags()
        {
            string logs = $"{DateTime.Now}\n";
            this.Flags = null;
            var diskFlags = new List<Flag>();
            string flagDir = Path.Join(webHostEnvironment.WebRootPath, "/flags/");
            var files = Directory.GetFiles(flagDir, "*.png", SearchOption.TopDirectoryOnly);
            foreach (var file in files)
            {
                string fileName = new FileInfo(file).Name;
                string countryName = fileName.Replace(".png", "").Replace("_", " ");

                diskFlags.Add(new Flag()
                {
                    name = countryName,
                    url = "/flags/" + fileName,
                });
            }

            var existingFlags = this.dbContext.flags.ToList();
            var missingFlags = diskFlags.Where(df =>
            {
                return !existingFlags.Any((Models.Flag f) => f.url == df.url);
            });
            foreach (var mf in missingFlags)
            {
                this.dbContext.flags.Add(mf);
                logs += $"Missing flag added: {mf}\n";
            }

            var deletedFlags = existingFlags.Where(ef =>
            {
                return !diskFlags.Any((Models.Flag f) => f.url == ef.url);
            });
            foreach (var df in deletedFlags)
            {
                this.dbContext.flags.Remove(df);
                logs += $"Deleted flags removed: {df}\n";
            }

            //  TODO update
            // foreach (var ef in existingFlags)
            // {
            //     var diskFlag = diskFlags.FirstOrDefault(df => ef.name.Contains(df.name) );
            //     if (diskFlag != null)
            //     {
            //         if ( diskFlag.url != ef.url)
            //         {
            //             ef.url = diskFlag.url;
            //             this.dbContext.flags.Update(ef);
            //         }

            //         if (diskFlag.name != ef.name)
            //         {
            //             ef.name = diskFlag.name;
            //             this.dbContext.flags.Update(ef);
            //         }

            //     }
            // }
            this.dbContext.SaveChanges();

            var allFlags = this.dbContext.flags;
            foreach (var flag in allFlags)
            {
                var pop = StaticData.pop.FirstOrDefault(pd => flag.name.Contains(pd.country));
                if (pop == null)
                {
                    logs += $"Missing population dict entry: {flag.name}\n";
                    continue;
                }
                else
                {
                    if (flag.population != pop.population)
                    {
                        flag.population = pop.population;
                        this.dbContext.flags.Update(flag);
                    }
                }

            }

            this.dbContext.SaveChanges();
            Debug.WriteLine(logs);
            return logs;
        }

    }

}
