namespace Stardeck.GameModels
{
    public class TerritoryModel
    {
        public string? Id { get; set; }

        public string Name { get; set; }

        public long Type { get; set; }

        public bool? Active { get; set; }

        public TerritoryAbility? Ability { get; set; }

        public string? Winner { get; set; }
        public List<GameCard> player1Cards { get; set; } = new();

        public List<GameCard> player2Cards { get; set; } = new();

 
        
    }
}