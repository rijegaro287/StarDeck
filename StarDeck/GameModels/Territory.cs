namespace Stardeck.GameModels
{

    public class Territory
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public long Type { get; set; }

        public bool? Active { get; set; }

        public TerritoryAbility? Ability { get; set; }

        //public Cards
    }
}
