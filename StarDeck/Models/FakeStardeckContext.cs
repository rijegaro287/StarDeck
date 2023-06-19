namespace Stardeck.Models
{
    public class FakeStardeckContext : StardeckContext
    {

        public override int SaveChanges()
        {
            return 0;
        }
    }
}
