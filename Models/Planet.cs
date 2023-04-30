using System;
using System.Collections.Generic;

namespace Stardeck.Models;

/// <summary>
/// 0 raro
/// 1 basico
/// 2 popular
/// </summary>
public partial class Planet
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public long Type { get; set; }

    public byte[][]? Image { get; set; }

    public bool? Active { get; set; }

    public string? Description { get; set; }

    public string? Ability { get; set; }
}
