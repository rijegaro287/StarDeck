using Stardeck.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Stardeck.GameModels
{
    public class PlayerModel
    {

        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        internal Account? Account { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Nickname { get; set; }

        public long Avatar { get; set; }

        public string? Config { get; set; }

        public long Points { get; set; }

        public long Coins { get; set; }

        [JsonIgnore] public long Energy { get; set; }

        [JsonIgnore] public List<GameCard> Deck { get; set; } = new List<GameCard>();

        [JsonIgnore] public List<GameCard> Hand { get; set; } = new List<GameCard>();

        [JsonIgnore] public List<List<GameCard>> TmpTerritories { get; set; } = new(3)
            {new(),new(),new()};
        
        
        
    }
}