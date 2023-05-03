namespace Stardeck.Models;

public partial class Gamelog
{
    public string Gameid { get; set; } = null!;

    public string? Log { get; set; }

    public virtual Gameroom Game { get; set; } = null!;
}
