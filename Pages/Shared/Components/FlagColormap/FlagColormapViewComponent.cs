using flags_game.Pages.Shared.Components.FlagColormapComponent;
using flags_game.Pages.Shared.Components.FlagList;
using Microsoft.AspNetCore.Mvc;

namespace flags_game;

public class FlagColormapViewComponent : ViewComponent
{

    public IViewComponentResult Invoke(FlagColormapModel model)
    {
        return this.View(model);
    }

}