namespace Stardeck.Models;

public partial class Gameroom:IAlphanumericID
{
    public string Roomid { get; set; } = null!;

    public string Player1 { get; set; } = null!;

    public string Player2 { get; set; } = null!;

    public string? Winner { get; set; }

    public long? Bet { get; set; }

    public virtual Gamelog? Gamelog { get; set; }

    public virtual Account? Player1Navigation { get; set; }

    public virtual Account? Player2Navigation { get; set; }

    public virtual Account? WinnerNavigation { get; set; }
    public string Id { get => Roomid; set => Roomid=value; }

    public void generateID()
    {
        ((IAlphanumericID)this).generateIdWithPrefix("G");
    }
}
