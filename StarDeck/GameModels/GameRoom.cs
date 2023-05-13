using System.Diagnostics;
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
        public bool AssignTerritories()
        {
            for (var i = 0; i < Territories.Length; i++)
            {
                Territories[i] = GetRandomPlanet();
                if (Territories[i].Id is null)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///  Get a random planet using the planet list of the game and their probability
        /// </summary>
        /// <returns></returns>
        public static Territory GetRandomPlanet()
        {
            var random = new Random();
            var probability = random.Next(0, 100);
            var logic = new Logic.PlanetLogic(new StardeckContext());
            var planets = logic.GetAll().GroupBy(x => x.Type).OrderByDescending(x => x.Key);
            var planetsList = probability switch
            {
                < 10 => planets.First(x => x.Key == 1).ToList(),
                < 30 => planets.First(x => x.Key == 2).ToList(),
                < 60 => planets.First(x => x.Key == 3).ToList(),
                < 90 => planets.First(x => x.Key == 4).ToList(),
                _ => planets.First(x => x.Key == 5).ToList()
            };

            //select random planet from list
            var planet = planetsList[random.Next(0, planetsList.Count)];

            return new Territory(planet);
        }

        public List<string?> GetTerritories()
        {
            var list = Territories.Select(x=>x.Id).ToList();
            if (Turn<3)
            {
                list[2] = "0";
            }
            return list;
        }

        public void SaveToDb()
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



