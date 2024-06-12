using CoreFlags;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace flags_game;
public class FlagTagListViewComponent : ViewComponent
{

    public IViewComponentResult Invoke(FlagTagListModel model)
    {
        return this.View( model);
    }

}