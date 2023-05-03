using System;
using System.Collections.Generic;

namespace Stardeck.Models;

public partial class FavoriteDeck
{
    public string Accountid { get; set; } = null!;

    public string? Deckid { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Deck? Deck { get; set; }
}
