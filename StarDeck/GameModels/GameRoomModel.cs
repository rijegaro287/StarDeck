using Stardeck.Engine;
using Stardeck.Models;

namespace Stardeck.GameModels;

/// <summary>
/// Data to send to Client 
/// </summary>
public class GameRoomModel
{
        public string Roomid { get; set; }

        public PlayerLogic Player1 { get; set; }

        public PlayerLogic Player2 { get; set; }

        public string? Winner { get; set; }

        public long? Bet { get; set; }

        public int Turn { get; set; } = 0;

        public int TurnTimer { get; set; } = 0;
        
        public string? FirstToShow { get; set; }
        public List<PlayableTerritory> Territories { get; set; }
        
        [System.Text.Json.Serialization.JsonIgnore]
        protected internal PlayableTerritory? Territory3;

        protected internal Gamelog? Gamelog { get; set; }

        
        [System.Text.Json.Serialization.JsonIgnore]
        protected internal Gameroom? Room{ get; set; }

}