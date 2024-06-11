using Microsoft.AspNetCore.Mvc;

namespace flags_game.Pages.Shared.Components.FlagList;

public class FlagListViewComponent : ViewComponent
{

    public IViewComponentResult Invoke(FlagListModel model)
    {
        return this.View( model);
    }

}