using flags_game.Pages.Shared.Components.ColorTagList;
using Microsoft.AspNetCore.Mvc;

namespace flags_game;

public class ColorTagListViewComponent : ViewComponent
{

    public IViewComponentResult Invoke(ColorTagListModel model)
    {
        return this.View( model );
    }

}