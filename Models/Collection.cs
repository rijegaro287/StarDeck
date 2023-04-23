using System;
using System.Collections.Generic;

namespace Stardeck.Models;

public partial class Collection
{
    public string IdAccount { get; set; } = null!;

    public string Collection1 { get; set; } = null!;

    public virtual Account? IdAccountNavigation { get; set; }
}
