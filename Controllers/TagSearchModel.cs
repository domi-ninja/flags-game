using System.Text.Json;

namespace CoreFlags
{
    public class TagSearchModel
    {
        public int? tagId { get; set; }
        public int seed {get; set;}
        public string fails {get; set;}
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

    }
}