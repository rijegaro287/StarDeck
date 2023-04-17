﻿using System;
using System.Collections.Generic;

namespace Stardeck.Models;

public partial class Account
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Nickname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool? Active { get; set; } = true;

    public long Avatar { get; set; } = 0;

    public string? Config { get; set; }

    public long Points { get; set; }

    public long Coins { get; set; }

    public virtual Avatar AvatarNavigation { get; set; } = null!;

    public virtual Deck? Deck { get; set; }

    public virtual ICollection<Avatar> Avatars { get; set; } = new List<Avatar>();
}
