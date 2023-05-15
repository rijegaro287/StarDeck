namespace Stardeck.Models;

/// <summary>
/// 0-basica
/// 
/// 1-normal
/// 
/// 2-rara
/// 
/// 3-Muy Rara
/// 
/// 4-Ultra Rara
/// </summary>
public partial class Card:IAlphanumericID
{
    public string Id { get; set; } = null!;
    public void generateID()
    {
        ((IAlphanumericID)this).generateIdWithPrefix("C");
    }



    public string Name { get; set; } = null!;

    public int Energy { get; set; }

    public int Battlecost { get; set; }

    public byte[] Image { get; set; } = null!;

    public bool? Active { get; set; } = true;

    public int Type { get; set; }

    public string? Ability { get; set; }

    public string? Description { get; set; }

    public string? Race { get; set; }
    
}
