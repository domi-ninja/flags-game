using flags_game.Pages.Shared.Components.TagsList;
using Microsoft.AspNetCore.Mvc;

namespace flags_game;

public class TagsListViewComponent : ViewComponent
{

    public IViewComponentResult Invoke(TagsListModel model)
    {
        return this.View( model );
    }

}