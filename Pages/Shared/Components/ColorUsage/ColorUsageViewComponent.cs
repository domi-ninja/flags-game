using flags_game.Pages.Shared.Components.ColorUsage;
using Microsoft.AspNetCore.Mvc;

namespace flags_game;

public class ColorUsageViewComponent : ViewComponent
{

    public IViewComponentResult Invoke(ColorUsageModel model)
    {
        return this.View(model);
    }

}