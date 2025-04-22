using System.Collections.Generic;
using System.Diagnostics;
using flags_game;
using flags_game.Models;
using flags_game.Pages.Shared.Components.FlagList;
using flags_game.Pages.Shared.Components.XxYy;
using Microsoft.AspNetCore.Mvc;

namespace CoreFlags
{
    public class FlagController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly FlagAppDbContext dbContext;

        public FlagController(IWebHostEnvironment webHostEnvironment, FlagAppDbContext dbContext)
        {
            this.webHostEnvironment= webHostEnvironment;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Edit()
        {
            List<Flag> flags = this.SyncFlags();
            var tags = dbContext.tags.ToList();

            return ViewComponent("XxYy", new FlagEditModel() { Flags = flags, Tags = tags });
        }

        [HttpPost]
        public IActionResult Tag(string newTagName) 
        { 
            var newTag = dbContext.tags.Add(new Tag() { name = newTagName });
            dbContext.SaveChanges();

            var tags = dbContext.tags.ToList();
            return ViewComponent(typeof(FlagTagListViewComponent), new FlagTagListModel() { Tags = tags });
        }

        [HttpGet]
        public IActionResult Tags()
        {
            var tags = dbContext.tags.ToList();

            return ViewComponent(typeof(FlagTagListViewComponent), new FlagTagListModel() { Tags = tags });
        }


        [HttpDelete]
        public IActionResult Tag( int tagId )
        {
            dbContext.tags.Remove( dbContext.tags.Find(tagId) );
            dbContext.SaveChanges();

            var tags = dbContext.tags.ToList();
            return ViewComponent(typeof(FlagTagListViewComponent), new FlagTagListModel() { Tags = tags });
        }


        [HttpGet]
        public async Task<IActionResult> List()
        {
            List<Flag> flags = this.SyncFlags();
            var tags = dbContext.tags.ToList();

            return ViewComponent("FlagList", new FlagListModel() { Flags = flags, Tags = tags });
        }

        private List<Flag> SyncFlags()
        {
            var result = new List<Flag>();
            string flagDir = Path.Join(webHostEnvironment.WebRootPath, "/flags/");
            var files = Directory.GetFiles(flagDir, "*.png", SearchOption.TopDirectoryOnly);
            foreach (var file in files)
            {
                string fileName = new FileInfo(file).Name;
                string countryName = fileName.Split(".")[0].Split("Flag_of_")[1].Replace("_", " ");
                result.Add( new Flag()
                {
                    name =  countryName,
                    url = "/flags/" + fileName,
                } );
            }

            result = result.OrderBy( r  => r.name ).ToList();
            return result;
        }
    }



}