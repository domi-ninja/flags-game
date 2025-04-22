using flags_game.Models;
using flags_game.Pages.Shared.Components.FlagList;
using Microsoft.Identity.Client;

namespace flags_game;

public class XFlagModel
{

    public Models.Flag flag {get; set;}

    public bool answerable {get; set;}
    public AnswerStat answeringStats { get; set; }
}