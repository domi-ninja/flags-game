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

            string newPath = Request.Path;
            if (!newPath.Contains("#sync-logs")) {
                newPath+="#sync-logs";
            }
        }

        private string SyncFlags()
        {
            string logs = $"{DateTime.Now}\n";

            var countries = StaticData.LoadCountries();
            var dbCountries = this.dbContext.flags.ToList();

            foreach (var dbc in dbCountries ) {
                if ( !countries.Any ( c => c.name == dbc.name  ) ) {
                    logs+= "Orphaned db country " + dbc.name + "\n";
                }
            }

            foreach ( var c in countries) {
                if (  !dbCountries.Any( dbc => dbc.name == c.name ) ) {
                    logs+= "Missing country " + c.name + "\n";
                }
                // dbContext.flags.Add(  new Flag() {
                //     name = c.name,
                //     shortName = c.@short,
                //     population = c.population,

                // });
            }

            foreach ( var c in countries) {
                Console.WriteLine(c.@short);

                var match = dbCountries.FirstOrDefault  ( dbc => dbc.name == c.name  );
                if (match!=null) {
                    if ( c.population > 0 && match.population!=c.population ) {
                        match.population=c.population;
                        dbContext.flags.Update( match );
                    }
                    
                    if ( !String.IsNullOrEmpty( c.@short ) && match.shortName != c.@short ) {
                        match.shortName=c.@short;
                        dbContext.flags.Update( match );
                    }
                }
            }

            this.dbContext.SaveChanges();

            var x = 1;
            // this.Flags = null;
            // var diskFlags = new List<Flag>();
            // string flagDir = Path.Join(webHostEnvironment.WebRootPath, "/flags/");
            // var files = Directory.GetFiles(flagDir, "*.png", SearchOption.TopDirectoryOnly);
            // foreach (var file in files)
            // {
            //     string fileName = new FileInfo(file).Name;
            //     string countryName = fileName.Replace(".png", "").Replace("_", " ");

            //     diskFlags.Add(new Flag()
            //     {
            //         name = countryName,
            //         url = "/flags/" + fileName,
            //     });
            // }

            // var existingFlags = this.dbContext.flags.ToList();
            // var missingFlags = diskFlags.Where(df =>
            // {
            //     return !existingFlags.Any((Models.Flag f) => f.url == df.url);
            // });
            // foreach (var mf in missingFlags)
            // {
            //     this.dbContext.flags.Add(mf);
            //     logs += $"Missing flag added: {mf}\n";
            // }

            // var deletedFlags = existingFlags.Where(ef =>
            // {
            //     return !diskFlags.Any((Models.Flag f) => f.url == ef.url);
            // });
            // foreach (var df in deletedFlags)
            // {
            //     this.dbContext.flags.Remove(df);
            //     logs += $"Deleted flags removed: {df}\n";
            // }

            // this.dbContext.SaveChanges();

            Console.WriteLine(logs);
            
            return logs;
        }

    }

}
