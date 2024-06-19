using CoreFlags;
using flags_game.Models;

namespace flags_game.Pages.Shared.Components.ColorTagList;

public class ColorTagListModel
{
    public List<FlagComboTag> tags { get; internal set; }

    public string TagColorCss { get; set; }
    public string TagParamsStr { get; set; }

}