namespace Stardeck.Models.Tests;
public class FakeDb : Stardeck.Models.StardeckContext
{
    public override int SaveChanges()
    {

        return 0;
    }

}