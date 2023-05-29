using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Stardeck.Logic;
using Stardeck.Models;

namespace Stardeck.GameModels
{
    public class GameRoom : GameModel
    {
        /// <summary>
        /// Pointer to the function that will be executing the game loop
        /// </summary>
        private Task<GameRoom>? _loop;

        /// <summary>
        /// Sincronization member to check if both players have ended their turn
        /// </summary>
        protected PlayerEndTurnFlag EndTurnFlag = new();

        private CancellationTokenSource _tokenSource = new();
        private CancellationToken _token;

        protected struct PlayerEndTurnFlag
        {
            public bool player1 = false;
            public bool player2 = false;
            public Task? Timer = null;

            public PlayerEndTurnFlag()
            {
            }

            public bool Check()
            {
                return player1 & player2;
            }

            public void Reset()
            {
                player1 = false;
                player2 = false;
                Timer?.Dispose();
            }
        }


        /// <summary>
        ///  Create a Runtime GameRoom from a database GameRoom and initialize it Gamelog if needed
        /// </summary>
        /// <param name="data"></param>
        public GameRoom(Gameroom? data)
        {
            _token = _tokenSource.Token;
            //assign space for terrutories
            Territories = new List<Territory>(new Territory[3]);
            //create object from room data
            if (data is null) throw new Exception("Error al crear la partida");
            Roomid = data.Roomid;
            Player1 = new Player(data.Player1Navigation);
            Player2 = new Player(data.Player2Navigation);
            data.Gamelog ??= new Gamelog { Gameid = data.Roomid, Game = data };
            Gamelog = data.Gamelog;
            Room = data;
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
            _loop = Task.Run(TurnLoop);
            return this;
        }

        private async Task<GameRoom> TurnLoop()
        {
            while (Turn <= 8)
            {
                StartTurn();
                //wait the 20second timer to end, changes are async so no need to check for the flag
                if (EndTurnFlag.Timer != null) await EndTurnFlag.Timer;

                FinishTurn();
            }

            CheckAndSetFinalWinner();
            _loop.Dispose();
            return this;
        }

        /// <summary>
        /// function to sync the turn timer with the client 
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        public async Task<int?> InitTurn(string idUser)
        {
            var index = FindPlayerIndexOnGame(idUser);
            if (index is null) return null;
            if (EndTurnFlag.Timer is not null) await EndTurnFlag.Timer;
            return Turn;
        }


        private int? StartTurn()
        {
            Resetflags();

            if (Turn == 3)
            {
                SwapPlanet();
            }

            var player = RevealCardsOrder();
            FirstToShow = FindPlayerOnGame(player);
            return Turn;
        }

        /// <summary>
        /// Reset the flags to start a new turn including the timer
        /// </summary>
        private void Resetflags()
        {
            EndTurnFlag.player1 = false;
            EndTurnFlag.player2 = false;
            EndTurnFlag.Reset();
            var reseted = _tokenSource.TryReset();
            if (reseted)
            {
                EndTurnFlag.Timer = Task.Delay(30000, _token);
            }
            else
            {
                _tokenSource = new CancellationTokenSource();
                _token = _tokenSource.Token;
                EndTurnFlag.Timer = Task.Delay(30000, _token);
            }

            TurnTimer = 20;
        }


        /// <summary>
        /// Start the secuence to end the turn by time
        /// </summary>
        /// <returns></returns>
        private int? FinishTurn()
        {
            ShowCards();

            Turn += 1;
            Player2.SetEnergy(Turn);
            Player1.SetEnergy(Turn);

            return Turn;
        }

        /// <summary>
        /// Start the secuence to end the turn by player request
        /// </summary>
        /// <param name="idUser"></param>
        public async Task<bool> EndTurn(string idUser)
        {
            var playerindex = FindPlayerIndexOnGame(idUser);
            switch (playerindex)
            {
                case null:
                    return false;
                case 1:
                    EndTurnFlag.player1 = true;
                    break;
                default:
                    EndTurnFlag.player2 = true;
                    break;
            }

            if (EndTurnFlag.Check())
            {
                _tokenSource.Cancel();
                return true;
            }

            if (EndTurnFlag.Timer != null) await EndTurnFlag.Timer;
            return true;
        }

        private void ShowCards()
        {
            foreach (var playerterritory in Player1.TmpTerritories.Select((value, i) => new { i, value }))
            {
                Territories[playerterritory.i].PlayCardPlayer1(playerterritory.value);
            }

            foreach (var playerterritory in Player2.TmpTerritories.Select((value, i) => new { i, value }))
            {
                Territories[playerterritory.i].PlayCardPlayer2(playerterritory.value);
            }

            Player1.CleanTmpTerritories();
            Player2.CleanTmpTerritories();
        }

        private void SwapPlanet()
        {
            if (Territory3 == null) return;
            Territory3.player1Cards = Territories[3].player1Cards;
            Territory3.player2Cards = Territories[3].player2Cards;
            Territories[3] = Territory3;
        }


        private string? CheckAndSetFinalWinner()
        {
            foreach (var territory in Territories)
            {
                territory.checkWinner();
            }

            var player1Win = Territories.Count(x => x.Winner == Player1.Id);
            var player2Win = Territories.Count(x => x.Winner == Player2.Id);

            if (player1Win > player2Win)
            {
                Winner = Player1.Id;
                Room.Winner = Player1.Id;
                return Player1.Id;
            }

            if (player2Win > player1Win)
            {
                Winner = Player2.Id;
                Room.Winner = Player2.Id;
                return Player2.Id;
            }

            var playerWithMorePoints = GetPlayerWithMaxPoints();
            if (playerWithMorePoints is null)
            {
                Winner = "Draw";
                Room.Winner = null;
                return "Draw";
            }

            Winner = playerWithMorePoints;
            Room.Winner = playerWithMorePoints;
            return playerWithMorePoints;
        }

        /// <summary>
        /// Draw a card from the player deck
        /// </summary>
        /// <param name="playerid"></param>
        public void DrawCard(string playerid)
        {
            var player = FindPlayerOnGame(playerid);
            if (player is null) return;
            DrawCard(player);
        }

        /// <summary>
        ///  Draw a card from the player deck
        /// </summary>
        /// <param name="player"></param>
        private void DrawCard(Player player)
        {
            var drawed = player.DrawCard();
            if (drawed is null)
            {
                Gamelog?.LogDrawError(player.Id);
            }
            else
            {
                Gamelog?.LogDraw(player.Id, drawed.Id);
            }
        }


        public string RevealCardsOrder()
        {
            if (Turn.Equals(1))
            {
                return GetRandomPlayerId();
            }

            var player = GetPlayerWithMaxPoints();

            if (player is null)
            {
                return GetRandomPlayerId();
            }

            return player;
        }


        /// <summary>
        /// Play a card on the temporary player territory
        /// </summary>
        /// <param name="playerid"></param>
        /// <param name="cardid"></param>
        /// <param name="territoryindex"></param>
        /// <returns>True if card played and energy reduced, false if not enough energy and null if invalid id</returns>
        public bool? PlayCard(string playerid, string cardid, int territoryindex)
        {
            var player = FindPlayerOnGame(playerid);
            if (player is null)
            {
                return null;
            }
            var played = player.PlayCard(cardid, territoryindex-1);
            if (played is not null) return played;
            var territoryid = Territories[territoryindex - 1].Id;
            if (territoryid != null)
                Gamelog?.LogCard(playerid, cardid, territoryid);

            return played;
        }

        /// <summary>
        /// Get the player reference from the player id
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns>player reference or null if not found</returns>
        public Player? FindPlayerOnGame(string playerId)
        {
            if (playerId == Player1.Id)
            {
                return Player1;
            }

            if (playerId == Player2.Id)
            {
                return Player2;
            }

            return null;
        }

        /// <summary>
        /// Get the player index from the player id
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns>player index or null if not found</returns>
        public int? FindPlayerIndexOnGame(string playerId)
        {
            if (playerId == Player1.Id)
            {
                return 1;
            }

            if (playerId == Player2.Id)
            {
                return 2;
            }

            return null;
        }

        /// <summary>
        /// Get the data of the player that is not included in the game data getter
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns>Json string</returns>
        public string GetPlayerData(string playerId)
        {
            return JsonConvert.SerializeObject(playerId == Player1.Id ? Player1 : Player2,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
        }

        /// <summary>
        /// Get the player with the most points including all the territories
        /// </summary>
        /// <returns> player with the most points or null if draw</returns>
        public string? GetPlayerWithMaxPoints()
        {
            Territory.Points points = new();
            Territories.ForEach(t => { points += t.GetPlayersPoints(); });
            if (points.player1 == points.player2)
            {
                return null;
            }

            if (points.player1 > points.player2)
            {
                return Player1.Id;
            }

            return Player2.Id;
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
        /// <returns> true id assigned false if failed</returns>
        private bool AssignTerritories()
        {
            Territories[0] = GetRandomPlanet();
            Territories[1] = GetRandomPlanet();
            Territories[2] = new Territory
            {
                Id = "0"
            };
            Territory3 = GetRandomPlanet();

            if (Territories.Any(t => t.Id is null))
            {
                return false;
            }

            return Territory3.Id is not null;
        }


        /// <summary>
        ///  Get a random planet using the planet list of the game and their probability
        /// </summary>
        /// <returns></returns>
        private static Territory GetRandomPlanet()
        {
            var random = new Random();
            var probability = random.Next(0, 100);
            var logic = new PlanetLogic(new StardeckContext());
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

        private string GetRandomPlayerId()
        {
            return Random.Shared.Next(0, 2) == 0 ? Player1.Id : Player2.Id;
        }


        private void SaveToDb()
        {
            var context = new StardeckContext();
            Debug.Assert(Room != null, nameof(Room) + " != null");
            Room.Player1Navigation = null;
            Room.Player2Navigation = null;
            var room = context.Gamerooms.Find(Roomid) ?? context.Gamerooms.Add(Room).Entity;
            Debug.Assert(room != null, nameof(room) + " != null");
            room.Gamelog = Gamelog;
            room.Winner = Winner;
            room.Bet = Bet;
            context.SaveChanges();
        }
    }
}