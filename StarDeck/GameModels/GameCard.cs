namespace Stardeck.GameModels
{
    public class GameCard
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public int Energy { get; set; }

        public int Battlecost { get; set; }

        public int Type { get; set; }

        public CardAbility? Ability { get; set; }

        public string? Race { get; set; }

        public bool? Showed { get; set; }

        public int? Turnplayed { get; set; }
    }
}
