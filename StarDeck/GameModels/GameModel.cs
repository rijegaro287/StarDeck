using Stardeck.Models;

namespace Stardeck.GameModels;

public class GameModel
{
        public string Roomid { get; set; }

        public Player Player1 { get; set; }

        public Player Player2 { get; set; }

        public string? Winner { get; set; }

        public long? Bet { get; set; }

        public int Turn { get; set; } = 0;

        public int TurnTimer { get; set; } = 0;
        
        public Player? FirstToShow { get; set; }
        public List<Territory> Territories { get; set; }
        
        [System.Text.Json.Serialization.JsonIgnore]
        protected Territory? Territory3;

        protected Gamelog? Gamelog { get; set; }

        
        [System.Text.Json.Serialization.JsonIgnore]
        protected Gameroom? Room{ get; set; }

}