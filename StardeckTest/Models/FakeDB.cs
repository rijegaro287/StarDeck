namespace Stardeck.Models.Tests;
using Stardeck.Models;
public class FakeDb:Stardeck.Models.StardeckContext
{
    public override int SaveChanges()
    {
        
        return 0;
    }

}