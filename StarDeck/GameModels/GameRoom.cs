using Stardeck.Models;

namespace Stardeck.GameModels
{
    public class GameRoom
    {
        public string Roomid { get; set; } = null!;

        public string Player1 { get; set; } = null!;

        public string Player2 { get; set; } = null!;

        public string? Winner { get; set; }

        public long? Bet { get; set; }

        public int? Turn { get; set; }

        public Territory[] territories { get; set; }= new Territory[2];

        public virtual Gamelog? Gamelog { get; set; }
    }
}
