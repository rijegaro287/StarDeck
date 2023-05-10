using Stardeck.Models;
using System.ComponentModel.DataAnnotations;

namespace Stardeck.GameModels
{
    public class Player
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Nickname { get; set; } = null!;

        public long Avatar { get; set; }

        public string? Config { get; set; }

        public long Points { get; set; }

        public long Coins { get; set; } 
        public long Energy { get; set; }

        public virtual List<GameCard> Deck { get; set; } = new List<GameCard>();

    }
}
