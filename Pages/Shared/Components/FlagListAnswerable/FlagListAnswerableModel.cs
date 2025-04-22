using flags_game.Models;

namespace flags_game.Pages.Shared.Components.FlagListAnswerable;

public class FlagListAnswerableModel
{
    public List<flags_game.Models.Flag> Flags { get; set; }
    public List<Tag> Tags { get; set; }

    public bool Random {get; set;}
}