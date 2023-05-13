using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using Stardeck.Models;

namespace Stardeck.GameModels
{
    public class GameRoom
    {
        public string Roomid { get; set; }

        public Player Player1 { get; set; }

        public Player Player2 { get; set; }

        public string? Winner { get; set; }

        public long? Bet { get; set; }

        public int? Turn { get; set; }
        
        public Territory[] Territories { get; set; } = new Territory[2];
        
        [JsonIgnore]
        private Territory? _territory3;

        public Gamelog? Gamelog { get; set; }
        
        public Models.Gameroom Room{ get; set; }

        public GameRoom(Models.Gameroom data)
        {
            //create object from room data
            Roomid = data.Roomid;
            Player1 = new Player(data.Player1Navigation);
            Player2 = new Player(data.Player2Navigation);
            Gamelog = data.Gamelog;
            Room= data;
        }
        /// <summary>
        /// Initialize the game. THis method don create the instance only initialize the players and territories.
        /// constructor is required to be called before this method
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public GameRoom Init()
        {
            var ready1 = Player1.Init();
            var ready2 = Player2.Init();
            var territories = AssignTerritories();

            if (!ready1 || !ready2 || !territories) throw new Exception("Error al inicializar la partida");
            SaveToDb();
            return this;


        }
        
        

        /// <summary>
        /// Get the data of the player that is not included in the game data getter
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns>Json string</returns>

        public string GetPlayerData(string playerId)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(playerId == Player1.Id ? Player1 : Player2);
        }

        /// <summary>
        ///  Return the Deck of the player as a list of string
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns>list of string or null when not founded the player</returns>
        public List<string>? GetDeck(string playerId)
        {
            if (playerId == Player1.Id)
            {
                return Player1.Deck.Select(x => x.Id).ToList();
            }

            return playerId == Player2.Id ? Player2.Deck.Select(x => x.Id).ToList() : null;
        }

        /// <summary>
        ///  Return the Hand of the player as a list of string
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns> list of string or null when not founded the player </returns>
        public List<string>? GetHand(string playerId)
        {
            if (playerId == Player1.Id)
            {
                return Player1.Hand.Select(x => x.Id).ToList();
            }

            return playerId == Player2.Id ? Player2.Hand.Select(x => x.Id).ToList() : null;
        }
        /// <summary>
        ///  Assign the territories to the game
        /// </summary>
        /// <returns></returns>
        private bool AssignTerritories()
        {
            Territories[0] = GetRandomPlanet();
            Territories[1] = GetRandomPlanet();
            Territories[2].Id = "0";
            _territory3 = GetRandomPlanet();
            
            if (Territories.Any(t => t.Id is null))
            {
                return false;
            }

            return _territory3.Id is not null;
        }

        /// <summary>
        ///  Get a random planet using the planet list of the game and their probability
        /// </summary>
        /// <returns></returns>
        private static Territory GetRandomPlanet()
        {
            var random = new Random();
            var probability = random.Next(0, 100);
            var logic = new Logic.PlanetLogic(new StardeckContext());
            var planets = logic.GetAll().GroupBy(x => x.Type).OrderByDescending(x => x.Key);
            var planetsList = probability switch
            {
                < 15 => planets.First(x => x.Key == 0).ToList(),
                < 50 => planets.First(x => x.Key == 1).ToList(),
                < 101 => planets.First(x => x.Key == 2).ToList(),
                _ => planets.First(x => x.Key == 2).ToList()
            };

            //select random planet from list
            var planet = planetsList[random.Next(0, planetsList.Count)];

            return new Territory(planet);
        }

        private void SaveToDb()
        {
            var context = new StardeckContext();
            var room = context.Gamerooms.Find(Roomid) ?? context.Gamerooms.Add(Room).Entity;
            room.Gamelog = Gamelog;
            room.Winner = Winner;
            room.Bet = Bet;
            context.SaveChanges();
        }

    }
}



