using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Stardeck.Models;

public partial class Gamelog
{
    public string Gameid { get; set; } = null!;

    public string? Log { get; set; }
    [JsonIgnore]
    public virtual Gameroom Game { get; set; } = null!;
}
