using flags_game.Pages.Shared.Components.FlagList;
using Microsoft.AspNetCore.Mvc;

namespace flags_game;

public class FlagListViewComponent : ViewComponent
{

    public IViewComponentResult Invoke(FlagListModel model)
    {
        return this.View( model );
    }

}