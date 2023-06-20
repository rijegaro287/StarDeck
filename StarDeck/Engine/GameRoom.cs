using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog.Extensions.Logging;
using Stardeck.GameModels;
using Stardeck.Logic;
using Stardeck.Models;

namespace Stardeck.Engine
{
    public class GameRoom : GameRoomModel
    {
        /// <summary>
        /// Pointer to the function that will be executing the game loop
        /// </summary>
        internal Task<GameRoom>? _loop;

        /// <summary>
        /// Sincronization member to check if both players have ended their turn
        /// </summary>
        protected PlayerEndTurnFlag EndTurnFlag = new();

        public CancellationTokenSource TokenSource = new();
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

        private string GetRandomPlayerId()
        {
            return Random.Shared.Next(0, 2) == 0 ? Player1.Id : Player2.Id;
        }

        internal async Task<GameRoom> TurnLoop()
        {
            while (Turn < 8 && Winner is null)
            {
                StartTurn();
                //wait the 20second timer to end, changes are async so no need to check for the flag
                await awaitTurnToEnd();

                FinishTurn();
            }

            if (Winner is null)
            {
                CheckAndSetFinalWinner();
            }

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
            await awaitTurnToEnd();
            return Turn;
        }

        private async Task<bool?> awaitTurnToEnd()
        {
            if (EndTurnFlag.Timer != null)
            {
                try
                {
                    await EndTurnFlag.Timer;
                    return true;
                }
                catch (TaskCanceledException)
                {
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            else
            {
                return null;
            }
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

        public PlayerModel FirstToShow { get; set; }

        /// <summary>
        /// Reset the flags to start a new turn including the timer
        /// </summary>
        private void Resetflags()
        {
            EndTurnFlag.player1 = false;
            EndTurnFlag.player2 = false;
            EndTurnFlag.Reset();
            var reseted = TokenSource.TryReset();
            _token = TokenSource.Token;
            if (reseted)
            {
                EndTurnFlag.Timer = Task.Delay(30000, _token);
            }
            else
            {
                TokenSource = new CancellationTokenSource();
                _token = TokenSource.Token;
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
            DrawCard(Player2);

            Player1.SetEnergy(Turn);
            DrawCard(Player1);

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
                TokenSource.Cancel();
                return true;
            }

            await awaitTurnToEnd();
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
            Territory3.player1Cards = Territories[2].player1Cards;
            Territory3.player2Cards = Territories[2].player2Cards;
            Territories[2] = Territory3;
        }


        private string? CheckAndSetFinalWinner()
        {
            Player1.Account!.isplaying = false;
            Player2.Account!.isplaying = false;

            AccountLogic _accountLogic = new AccountLogic(new StardeckContext(),
                new SerilogLoggerFactory().CreateLogger(type: typeof(AccountLogic)));


            foreach (var territory in Territories)
            {
                territory.CheckWinner();
            }

            //Means that some player surrender
            if (Winner is not null)
            {
                _accountLogic.ManageEnd(Room.Bet, Winner, Winner != Player1.Id ? Player1.Id : Player2.Id);
                return Winner;
            }

            var player1Win = Territories.Count(x => x.Winner == "player1");
            var player2Win = Territories.Count(x => x.Winner == "player2");

            if (player1Win > player2Win)
            {
                Winner = Player1.Id;
                Room!.Winner = Player1.Id;
                _accountLogic.ManageEnd(Room.Bet, Player1.Id, Player2.Id);

                return Player1.Id;
            }

            if (player2Win > player1Win)
            {
                Winner = Player2.Id;
                Room!.Winner = Player2.Id;

                _accountLogic.ManageEnd(Room.Bet, Player2.Id, Player1.Id);


                return Player2.Id;
            }

            var playerWithMorePoints = GetPlayerWithMaxPoints();
            if (playerWithMorePoints is null)
            {
                Winner = "Draw";
                Room!.Winner = null;
                return "Draw";
            }

            Winner = playerWithMorePoints;
            Room!.Winner = playerWithMorePoints;
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
        /// <param name="playerLogicModel"></param>
        private void DrawCard(PlayerLogic playerLogicModel)
        {
            var drawed = playerLogicModel.DrawCard();
            if (drawed is null)
            {
                Gamelog?.LogDrawError(playerLogicModel.Id);
            }
            else
            {
                Gamelog?.LogDraw(playerLogicModel.Id, drawed.Id);
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

            var played = player.PlayCard(cardid, territoryindex - 1);
            if (played is null) return played;
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
        public PlayerLogic? FindPlayerOnGame(string playerId)
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
            PlayableTerritory.Points points = new();
            Territories.ForEach(t => { points += t.GetPlayersPoints(); });
            if (points.player1 == points.player2)
            {
                return null;
            }

            return points.player1 > points.player2 ? Player1.Id : Player2.Id;
        }

        public bool? Surrender(string idUser)
        {
            PlayerLogic? player;

            if (idUser == Player1.Id)
                player = Player2;
            else if (idUser == Player2.Id)
                player = Player1;
            else
                player = null;

            if (player is null) return null;


            Winner = player.Id;
            Room!.Winner = player.Id;
            Turn = 8;
            TokenSource.Cancel();
            return true;
        }
#if DEBUG
        public Task? GetTimer()
        {
            return _loop;
        }

        public Task? GetTurnTimer()
        {
            return EndTurnFlag.Timer;
        }

        public async Task<bool?> PublicawaitTurnToEnd()
        {
            return await awaitTurnToEnd();
        }
#endif
    }
}