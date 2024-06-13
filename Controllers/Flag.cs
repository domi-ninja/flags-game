using System.Collections.Generic;
using System.Diagnostics;
using flags_game;
using flags_game.Models;
using flags_game.Pages.Shared.Components.FlagList;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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


        public ( List<Tag>, List<Flag> ) LoadData(int? tagId)
        {
            var tags = dbContext.tags
                .Include(t => t.flagTags)
                .OrderBy(r => r.name)
                .ToList();
            var flags = this.dbContext.flags
                .Include(f => f.flagTags)
                    .ThenInclude( ft => ft.Tag )
                .Where( f => 
                    tagId.HasValue ? ( tagId == -1 ? !f.flagTags.Any() : f.flagTags.Any(ft => ft.TagId == tagId) ) : f.population > 1000000
                 )
                .OrderBy(r => r.population)
                .Reverse()
                .ToList();

            return (tags, flags);
        }

        [HttpGet]
        public IActionResult Edit()
        {
            List<Flag> flags = dbContext.flags
                .Include( flag => flag.flagTags )
                    .ThenInclude( flagTag => flagTag.Tag )
                .ToList();
            var tags = dbContext.tags.ToList();
            return Ok();
        }

        [HttpPost]
        public IActionResult Tag(string newTagName) 
        { 
            var newTag = dbContext.tags.Add(new Tag() { name = newTagName });
            dbContext.SaveChanges();

            var tags = dbContext.tags.ToList();
            return ViewComponent(typeof(FlagTagListViewComponent), new FlagTagListModel() { Tags = tags });
        }

        [HttpPost]
        public IActionResult FlagTag(FlagTag flagTag)
        {
            //var tag = dbContext.tags.Find(flagTag.TagId);
            dbContext.flagTags.Add(flagTag);
            dbContext.SaveChanges();

            var tags = dbContext.tags.ToList();
            return Redirect("/Edit"); // ViewComponent(typeof(FlagTagListViewComponent), new FlagTagListModel() { Tags = tags });
        }

        

        [HttpPut]
        public IActionResult Tag(Tag updateModel)
        {
            var oldTag = dbContext.tags.Find( updateModel.Id );
            oldTag.name = updateModel.name;
            dbContext.tags.Update(oldTag);

            // Update(tag);
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
            return Content(""); //ViewComponent(typeof(FlagTagListViewComponent), new FlagTagListModel() { Tags = tags });
        }


        [HttpGet]
        public async Task<IActionResult> List( TagSearchModel  searchModel )
        {
            var (tags, flags) = LoadData(searchModel.tagId);

            return ViewComponent("FlagList", new FlagListModel() { Flags = flags, Tags = tags });
        }


    }



}