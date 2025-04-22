using CoreFlags;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CoreFlags;
public class EditViewComponent : ViewComponent
{

    public IViewComponentResult Invoke(Edit model)
    {
        return View( model);
    }

}