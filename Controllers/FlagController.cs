using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Text.Json;
using flags_game;
using flags_game.Models;
using flags_game.Pages.Shared.Components.FlagColormapComponent;
using flags_game.Pages.Shared.Components.FlagList;
using flags_game.Pages.Shared.Components.FlagListAnswerable;
using flags_game.Pages.Shared.Components.TagsList;
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
            this.webHostEnvironment = webHostEnvironment;
            this.dbContext = dbContext;
        }


        public (List<Tag>, List<Flag>) LoadData(int? tagId = null, List<int> flagIds = null, int? minPop = null)
        {
            var tags = dbContext.tags
                .Include(t => t.flagTags)
                .OrderBy(r => r.name)
                .ToList();
            var flags = this.dbContext.flags
                .Where(f =>
                    (
                    (flagIds != null && flagIds.Count > 0 ? flagIds.Contains(f.Id) : true)
                    &&
                    (minPop != null ? f.population >= minPop : true)
                    )
                )
                .Include(f => f.flagTags)
                    .ThenInclude(ft => ft.Tag)
                .Include(f => f.colorTags)
                    .ThenInclude(ct => ct.FlagColor)
                .Where(f =>
                    tagId.HasValue ? f.flagTags.Any(ft => ft.TagId == tagId) : true
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
                .Include(flag => flag.flagTags)
                    .ThenInclude(flagTag => flagTag.Tag)
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
            var oldTag = dbContext.tags.Find(updateModel.Id);
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
        public IActionResult Tag(int tagId)
        {
            dbContext.tags.Remove(dbContext.tags.Find(tagId));
            dbContext.SaveChanges();

            var tags = dbContext.tags.ToList();
            return Content(""); //ViewComponent(typeof(FlagTagListViewComponent), new FlagTagListModel() { Tags = tags });
        }

        [HttpGet]
        public IActionResult TagButtons(TagSearchModel searchModel)
        {
            var tags = this.dbContext.tags.ToList();
            return ViewComponent(typeof(TagsListViewComponent), new TagsListModel()
            {
                Tags = tags,
                Random = searchModel.random,
                TagParamsStr = searchModel.TagParamsStr,
                TagColorCss = searchModel.TagColorCss,
            });
        }

        [HttpGet]
        public async Task<IActionResult> List(TagSearchModel searchModel)
        {
            var (tags, flags) = LoadData(searchModel.tagId);

            return ViewComponent(typeof(FlagListViewComponent), new FlagListModel() { Flags = flags, Tags = tags });
        }

        [HttpGet]
        public async Task<IActionResult> Names()
        {
            var (tags, flags) = LoadData();

            return Json(flags.Select(f=>f.name));
        }


        [HttpPost]
        public async Task<IActionResult> ListQuestion(TagSearchModel searchModel)
        {
            if (searchModel.answers)
            {
                var (tags2, flags2) = LoadData(searchModel.tagId, flagIds: searchModel.flagIds);
               
                return ViewComponent(typeof(FlagListViewComponent), new FlagListModel() { 
                    Flags = flags2, 
                    Tags = tags2,  
                });
            }

            int minPop = searchModel.minPop ? 1000_000 : 0;

            var (tags, flags) = LoadData(tagId: searchModel.tagId, flagIds: searchModel.flagIds, minPop: minPop);
            foreach (var flag in flags)
            {
                flag.population = 0;
                flag.flagTags = new List<FlagTag>();
            }

            Dictionary<int, AnswerStat> answeringStats = null;
            if (!String.IsNullOrEmpty(searchModel.answeringStats))
            {
                answeringStats = JsonSerializer.Deserialize<Dictionary<int, AnswerStat>>(searchModel.answeringStats);
            }

            if (!String.IsNullOrEmpty(searchModel.fails))
            {
                var fails = JsonSerializer.Deserialize<Dictionary<string, bool>>(searchModel.fails);

                Console.WriteLine("xxxxx");
                Console.WriteLine(searchModel);
                Console.WriteLine(string.Join(",", fails.Keys));
                Console.WriteLine(string.Join(",", fails.Values));

                if (fails.Keys.Count > 0)
                {
                    flags = flags.Where(f =>
                      fails.ContainsKey(f.Id.ToString()) &&
                      !fails[f.Id.ToString()]
                    ).ToList();
                }
            }

            if (searchModel.random)
            {
                flags = flags.Randomize().ToList();
            } else if ( answeringStats !=null )
            {
                flags = flags
                        .OrderBy(f => (answeringStats?.GetValueOrDefault(f.Id)?.rank() ?? 0))
                        .ToList();
            }
            return ViewComponent(typeof(FlagListAnswerableViewComponent),
              new FlagListAnswerableModel()
              {
                  Flags = flags,
                  Tags = tags,
                  Random = searchModel.random,
                  answeringStats = answeringStats,
              }
            );
        }


        private int SearchRanking(string dataName, string searchName)
        {
            int result = 0;
            result += dataName.ToLower().StartsWith(searchName.ToLower()) ? 2 : 0;
            result += dataName.ToLower().Contains(searchName.ToLower()) ? 1 : 0;
            return result;
        }


        [HttpPost]
        public IActionResult ListByIds(List<int> flagIds)
        {
            var (tags, flags) = LoadData(flagIds: flagIds);

            return ViewComponent(typeof(FlagListViewComponent), new FlagListModel() { Flags = flags, Tags = tags });
        }

        [HttpPost]
        public async Task<IActionResult> SearchCountry(string search)
        {
            if (search==null || search.Length < 3) return Content("");

            string result = "";
            var flags = this.dbContext.flags.ToList()
              .Where(f => SearchRanking(f.name, search) > 0)
              .OrderBy(f => -SearchRanking(f.name, search));
            //.OrderBy( f=> editDistanceWith2Ops(f.name.ToLower(), search.ToLower()) ).Take(5);
            foreach (var flag in flags)
            {
                result += $"<li>{flag.name}</li>";
            }
            return Content(result);
        }

        private int Compute(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // Verify arguments.
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Initialize arrays.
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Begin looping.
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    // Compute cost.
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                    d[i, j] = Math.Min(
                    Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                    d[i - 1, j - 1] + cost);
                }
            }
            // Return cost.
            return d[n, m];
        }

        static int editDistanceWith2Ops(String X,
                                        String Y)
        {
            // Find LCS 
            int m = X.Length, n = Y.Length;
            int[,] L = new int[m + 1, n + 1];
            for (int i = 0; i <= m; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    if (i == 0 || j == 0)
                    {
                        L[i, j] = 0;
                    }
                    else if (X[i - 1] == Y[j - 1])
                    {
                        L[i, j] = L[i - 1, j - 1] + 1;
                    }
                    else
                    {
                        L[i, j] = Math.Max(L[i - 1, j],
                                            L[i, j - 1]);
                    }
                }
            }
            int lcs = L[m, n];

            // Edit distance is delete operations + 
            // insert operations. 
            return (m - lcs) + (n - lcs);
        }

    }



}