using System;
using System.Collections.Generic;

namespace Stardeck.Models;

public partial class Parameter
{
    public string Key { get; set; } = null!;

    public string? Value { get; set; }
}
