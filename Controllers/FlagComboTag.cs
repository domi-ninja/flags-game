
using flags_game.Models;

namespace CoreFlags
{
    public class FlagComboTag
    {
        public IEnumerable<ColorPair> colors { get; internal set; }
        public int flagsCount { get; internal set; }
        public Flag[] flags { get; internal set; }
    }
}