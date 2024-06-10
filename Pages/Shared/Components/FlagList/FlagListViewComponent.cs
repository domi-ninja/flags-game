using CoreFlags;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CoreFlags;
public class FlagListViewComponent : ViewComponent
{

    public IViewComponentResult Invoke(Edit model)
    {
        return View( model);
    }

}