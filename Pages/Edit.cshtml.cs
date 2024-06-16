using System.Diagnostics;
using flags_game.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using flags_game.Data;
using System.Text.Json.Serialization;
using System.Text.Json;

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
        public string Logs {get; set; } = "";

        public override string ToString(){
            return JsonSerializer.Serialize( new{
                Tags,
                Flags,
                Logs

            }, new JsonSerializerOptions(){
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
                .OrderBy(r => r.population)
                .Reverse()
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

        public void OnPostSyncFlags() {
            this.Logs = SyncFlags();
            LoadData();
            // return Redirect(Request.Path);
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
                logs+=$"Missing flag added: {mf}\n";
            }

            var deletedFlags = existingFlags.Where(ef =>
            {
                return !diskFlags.Any((Models.Flag f) => f.url == ef.url);
            });
            foreach (var df in deletedFlags)
            {
                this.dbContext.flags.Remove(df);
                logs+=$"Deleted flags removed: {df}\n";
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
                    logs+=$"Missing population dict entry: {flag.name}\n";
                    continue;
                } else
                {
                    if (flag.population!=pop.population) {
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
