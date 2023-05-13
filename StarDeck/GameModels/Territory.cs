namespace Stardeck.GameModels
{

    public class Territory
    {
        public string? Id { get; set; }

        public string Name { get; set; }

        public long Type { get; set; }

        public bool? Active { get; set; }

        public TerritoryAbility? Ability { get; set; }

        public List<GameCard> player1Cards { get; set; } = new();

        public List<GameCard> player2Cards { get; set; } = new();   

        public Territory(Models.Planet data)
        {
            Id = data.Id;
            Name = data.Name;
            Type = data.Type;
            Active = data.Active;
            Ability = new TerritoryAbility(data.Ability);
        }
    }
}
