using Microsoft.AspNetCore.Mvc;
using flags_game.Models;
using flags_game.Pages.Shared.Components.TagsList;

namespace flags_game;

[ViewComponent(Name = "XFlag")]
public class XFlagViewComponent : ViewComponent
{

    public IViewComponentResult Invoke(XFlagModel model)
    {
        return this.View( model );
    }

}