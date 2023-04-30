using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Stardeck.Models;

public partial class Collection
{
    public string IdAccount { get; set; } = null!;

    public string[]? Collection1 { get; set; }

    [JsonIgnore]
    public virtual Account? IdAccountNavigation { get; set; }
}
