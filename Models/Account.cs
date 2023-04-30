using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Stardeck.Models;

public partial class Account
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Nickname { get; set; } = null!;

    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    public string Email { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool? Active { get; set; }

    public long Avatar { get; set; }

    public string? Config { get; set; }

    public long Points { get; set; }

    public long Coins { get; set; }
    [JsonIgnore]
    public virtual Avatar? AvatarNavigation { get; set; }
    [JsonIgnore]
    public virtual Collection? Collection { get; set; }
    [JsonIgnore]
    public virtual ICollection<Deck> Decks { get; set; } = new List<Deck>();

    public virtual FavoriteDeck? FavoriteDeck { get; set; }

    [JsonIgnore]
    public virtual ICollection<Gameroom>? GameroomPlayer1Navigations { get; set; } = new List<Gameroom>();
    [JsonIgnore]
    public virtual ICollection<Gameroom>? GameroomPlayer2Navigations { get; set; } = new List<Gameroom>();
    [JsonIgnore]
    public virtual ICollection<Gameroom>? GameroomWinnerNavigations { get; set; } = new List<Gameroom>();

    public virtual ICollection<Avatar> Avatars { get; set; } = new List<Avatar>();
}
