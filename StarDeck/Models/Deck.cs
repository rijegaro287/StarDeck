namespace Stardeck.Models;

public partial class Deck
{
    public string IdAccount { get; set; } = null!;

    public string[] Cardlist { get; set; } = null!;

    public string IdDeck { get; set; } = null!;

    public string? DeckName { get; set; }

    public virtual ICollection<FavoriteDeck> FavoriteDecks { get; set; } = new List<FavoriteDeck>();

    public virtual Account? IdAccountNavigation { get; set; }
}
