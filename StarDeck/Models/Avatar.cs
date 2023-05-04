namespace Stardeck.Models;

public partial class Avatar
{
    public long Id { get; set; }

    public byte[] Image { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<Account> IdAccounts { get; set; } = new List<Account>();
}
