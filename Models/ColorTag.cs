using flags_game.Models;

namespace flags_game.Models
{
    public class ColorTag
    {

        public FlagColor FlagColor { get; set; }
        public int Id { get; set; }
        public int FlagId { get; set; }
        public int TagId { get; set; }
        public Flag Flag { get; set; }
    }
}