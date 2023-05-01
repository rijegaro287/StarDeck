﻿using System;
using System.Collections.Generic;

namespace Stardeck.Models;

public partial class Deck
{
    public string IdAccount { get; set; } = null!;

    public string[] Deck1 { get; set; } = null!;

    public string IdDeck { get; set; } = null!;

    public virtual ICollection<FavoriteDeck> FavoriteDecks { get; set; } = new List<FavoriteDeck>();

    public virtual Account? IdAccountNavigation { get; set; }
}
