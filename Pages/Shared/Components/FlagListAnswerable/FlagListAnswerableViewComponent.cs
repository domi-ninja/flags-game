using flags_game.Pages.Shared.Components.FlagListAnswerable;
using Microsoft.AspNetCore.Mvc;

namespace flags_game;

public class FlagListAnswerableViewComponent : ViewComponent
{

    public IViewComponentResult Invoke(FlagListAnswerableModel model)
    {
        return this.View( model );
    }

}