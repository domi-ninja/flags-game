using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;

namespace CoreFlags
{
    public class FlagController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public FlagController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment= webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Edit()
        {
            List<Flag> flags = this.SyncFlags();

            return ViewComponent("Edit", new Edit() { Flags = flags });
        }


        [HttpGet]
        public IActionResult List()
        {
            List<Flag> flags = this.SyncFlags();
         
            return ViewComponent("FlagList", new Edit() { Flags = flags });
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

    public class Flag
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Tag
    {
        public string name { get; set; }
    }

}