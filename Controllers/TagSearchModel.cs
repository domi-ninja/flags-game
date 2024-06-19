using System.Text.Json;

namespace CoreFlags
{
    public class TagSearchModel
    {
        public int? tagId { get; set; }
        public bool random {get; set;}
        public string fails {get; set;}
        public string TagParamsStr { get; set; }
        public string TagColorCss { get; set; }
        public bool answers { get; set; }

        public List<int> flagIds { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

    }
}