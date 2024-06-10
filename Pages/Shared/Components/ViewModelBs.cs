using CoreFlags;
using Microsoft.AspNetCore.Mvc;

namespace CoreFlags;
public class ViewModelBs : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View(new
        {
            Model = this,
        });
    }
}