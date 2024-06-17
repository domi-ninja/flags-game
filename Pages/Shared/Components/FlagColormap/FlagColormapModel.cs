using flags_game.Models;
using System.Drawing;

namespace flags_game.Pages.Shared.Components.FlagColormapComponent;

public class FlagColormapModel
{
    public List<Dictionary<Color, Color>> colorResult { get; internal set; }
    public List<Flag> flags { get; internal set; }
}