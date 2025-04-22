using CoreFlags;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


[ViewComponent(Name = "XxYy")]
public class XxYyViewComponent : ViewComponent
{

    public IViewComponentResult Invoke(FlagEditModel model)
    {
        return View( model);
    }

}