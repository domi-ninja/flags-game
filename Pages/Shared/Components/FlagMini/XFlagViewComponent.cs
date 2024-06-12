using Microsoft.AspNetCore.Mvc;
using flags_game.Models;

namespace flags_game;

[ViewComponent(Name = "FlagMini")]
public class FlagMiniViewComponent : ViewComponent
{

    public IViewComponentResult Invoke(Models.Flag model)
    {
        return this.View( model);
    }

}