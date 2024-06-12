using System.Diagnostics;
using flags_game.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

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

        public void LoadData()
        {
            this.Tags = dbContext.tags
                .Include( t => t.flagTags )
                    .ThenInclude( ft => ft.Flag )
                .OrderBy(r => r.name)
                .ToList();
            this.Flags = this.dbContext.flags
                .Include( f => f.flagTags )
                    //.ThenInclude( ft => ft.Tag )
                .OrderBy(r => r.name)
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
            SyncFlags();
            LoadData();
        }

        // public void OnReloadFlags()
        // {
        //     this.Flags = SyncFlags();
        // }

        private void SyncFlags()
        {
            this.Flags = null;
            var diskFlags = new List<Flag>();
            string flagDir = Path.Join(webHostEnvironment.WebRootPath, "/flags/");
            var files = Directory.GetFiles(flagDir, "*.png", SearchOption.TopDirectoryOnly);
            foreach (var file in files)
            {
                string fileName = new FileInfo(file).Name;
                string countryName = fileName.Split(".")[0].Split("Flag_of_")[1].Replace("_", " ");
                diskFlags.Add(new Flag()
                {
                    name = countryName,
                    url = "/flags/" + fileName,
                });
            }

            var existingFlags = this.dbContext.flags;
            var missingFlags = diskFlags.Where(df =>
            {
                return !existingFlags.Any((Models.Flag f) => f.name == df.name);
            });


            // var missingFlags 
            foreach (var mf in missingFlags)
            {
                this.dbContext.flags.Add(mf);
            }

            this.dbContext.SaveChanges();
        }

 
    }

}
