using flags_game.Models;

namespace flags_game.Pages.Shared.Components.FlagList;

public class FlagListModel
{
    public List<flags_game.Models.Flag> Flags { get; set; }
    public List<Tag> Tags { get; set; }
    public Dictionary<int, AnswerStat> answeringStats { get; set; }
}

public class AnswerStat
{
    public int right { get; set; }
    public int wrong { get; set; }

    public int rank()
    {
        return right - wrong;
    }
}