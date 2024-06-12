using System.Diagnostics;
using flags_game.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace flags_game.Pages
{
    public class EditTagModel : PageModel
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly FlagAppDbContext dbContext;
        private readonly ILogger<EditModel> logger;

        public EditTagModel(IWebHostEnvironment webHostEnvironment, FlagAppDbContext dbContext, ILogger<EditModel> logger)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.dbContext = dbContext;
            this.logger = logger;

        }

        public Tag Tag { get; set; }
        public List<Flag> Flags { get; set; } = new List<Flag>();

        public void LoadData( int flagId )
        {
            this.Tag = dbContext.tags
                .Include(t => t.flagTags)
                    .ThenInclude(ft => ft.Flag)
                .First(f => f.Id == flagId);
            this.Flags = this.dbContext.flags
                .Include( f => f.flagTags )
                    //.ThenInclude( ft => ft.Tag )
                .OrderBy(r => r.population)
                .Reverse()
                .ToList();
        }

        public void OnGet()
        {
            LoadData(int.Parse(Request.Query["tagId"]) );
        }

        public ActionResult OnPostAddFlagTag(FlagTag flagTag)
        {
            this.dbContext.flagTags.Add(flagTag);
            this.dbContext.SaveChanges();
            LoadData(flagTag.TagId);
            return Redirect(Request.Path + "?tagId=" + flagTag.TagId );
        }


        public ActionResult OnPostRemoveFlagTag(FlagTag flagTag)
        {
            this.dbContext.flagTags.Remove(flagTag);
            this.dbContext.SaveChanges();
            LoadData(flagTag.TagId);
            return Redirect(Request.Path + "?tagId=" + flagTag.TagId);
        }

    }

}
