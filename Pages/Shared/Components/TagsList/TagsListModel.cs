using flags_game.Models;

namespace flags_game.Pages.Shared.Components.TagsList;

public class TagsListModel
{
    public List<Tag> Tags { get; set; }
    public bool Random { get; set;}

    public string TagParamsStr { get; set;}
    public string TagColorCss { get; set;}
}