using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;

namespace Stardeck.Models;
[MetadataType(typeof(AccountMetadata))]
public partial class Account
{
    [NotMapped]
    public DateTime? lastacces { get; set; } = null;
    [NotMapped]
    public bool? logged { get; set; } = false;

    [NotMapped]
    public bool? isplaying { get; set; } = false;

    [NotMapped]
    public bool? isInMatchMacking { get; set; } = false;

    [NotMapped]
    public string? roomid { get; set; }= null;
}

public class AccountMetadata
{
    [RegularExpression(@"^C-[a-zA-Z0-9]{12}")]
    [StringLength(maximumLength:14,MinimumLength =14)]
    public string Id { get; set; } = null!;
    [EmailAddress]
    public string Email { get; set; } = null!;
}
