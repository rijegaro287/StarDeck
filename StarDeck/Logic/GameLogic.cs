using Microsoft.EntityFrameworkCore;
using Stardeck.DbAccess;
using Stardeck.Engine;
using Stardeck.Models;

namespace Stardeck.Logic
{
    public class GameLogic
    {
        protected static readonly StardeckContext MatchMackingContext = new();

        private static readonly List<Account>
            loadPlayers = MatchMackingContext.Accounts.ToList(); //needed to preload DO NOT DELETE

        private readonly StardeckContext _gameContext;
        private static readonly List<GameRoom> ActiveRooms = new List<GameRoom>();
        private readonly GameDb _gameDb;
        private readonly ILogger _logger;
        private static readonly object Locker = new object();

        public GameLogic(StardeckContext gameContext, ILogger logger)
        {
            this._gameContext = gameContext;
            _logger = logger;
            this._gameDb = new GameDb(gameContext);
        }

        public async Task<GameRoom?> IsWaiting(string playerId)
        {
            Account? player1;
            lock (MatchMackingContext)
            {
                player1 = MatchMackingContext.Accounts.Find(playerId);
            }

            if (player1 is null)
            {
                _logger.LogWarning("Jugador no encontrado");
                throw new Exception("Player not found");
            }

            GameRoom? room;
            room = CheckIfPlaying(player1);
            if (room is not null)
            {
                return room;
            }

            await PutInMatchMaking(playerId, true);

            var counter = 0;
            while (player1.isInMatchMacking == true && counter < 15)
            {
                counter += 1;
                List<Account> accounts;
                accounts = await MatchMackingContext.Accounts.ToListAsync();


                var players = accounts
                    .Where(x => x.isInMatchMacking == true).ToList();
                var inRangePlayers =
                    players.Where(x => x.Id != playerId
                                       && Math.Abs(x.Points - player1.Points) < 101).ToList();


                if (inRangePlayers.Count == 0)
                {
                    await Task.Delay(1500);
                    room = CheckIfPlaying(player1);
                    if (room is not null)
                    {
                        return room;
                    }

                    continue;
                }


                lock (Locker)
                {
                    room = CheckIfPlaying(player1);
                    if (room is not null)
                    {
                        return room;
                    }

                    var battle = new Gameroom();
                    var rnd = new Random();
                    var randIndex = rnd.Next(inRangePlayers.Count);
                    var opponent = inRangePlayers[randIndex];

                    battle.Player2 = opponent.Id;
                    battle.Player2Navigation = opponent;
                    battle.Player1 = player1.Id;
                    battle.Player1Navigation = player1;
                    battle.generateID();
                    room = GameRoomBuilder.CreateInstance(battle);
                    room = GameRoomBuilder.Init(_gameContext, room);

                    ActiveRooms.Add(room);
                    GameRoomBuilder.SaveToDb(_gameContext, room);
                    room = CheckIfPlaying(player1);
                    _logger.LogInformation("Request IsWaiting completada");
                    return room;
                }
            }

            player1.isInMatchMacking = false;
            return CheckIfPlaying(player1);
        }

        public GameRoom? CheckIfPlaying(Account player)
        {
            switch (player.isplaying)
            {
                case null:
                    throw new InvalidOperationException("Estado del jugador invalido");
                case false:
                    return null;
            }

            var room = ActiveRooms.FirstOrDefault(x =>
                (x.Player2.Id == player.Id | x.Player1.Id == player.Id) && x.Turn <= 7);
            if (room is not null)
            {
                _logger.LogInformation("Request CheckIfPlaying para {Id} completada", player.Id);
                return room;
            }

            _logger.LogWarning("No se encontró al jugador {PlayerId} en el Request CheckIfPlaying ", player.Id);
            return null;
        }

        public async Task<bool?> PutInMatchMaking(string accountId, bool isInMatchMacking)
        {
            Account? account = await MatchMackingContext.Accounts.FindAsync(accountId);
            if (account is null)
            {
                _logger.LogWarning("No se encontró al jugador {accountId} en el Request PutInMatchMaking ", accountId);
                return null;
            }

            var selectedDeck = await _gameContext.FavoriteDecks.FindAsync(accountId);
            account.FavoriteDeck = selectedDeck;
            account.isInMatchMacking = isInMatchMacking;
            _logger.LogInformation("Request CheckIfPlaying para {accountId} completada", accountId);
            return isInMatchMacking;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="game"> gameid</param>
        /// <param name="idPlayer">playerid</param>
        /// <param name="cardid">card to play</param>
        /// <param name="planetindex">planet where play card from 1 to 3 inclusive</param>
        /// <returns>1 if succes, 0 if not enough energy, null if GameRoom not founded, -1 if invalid player,card or planet id </returns>
        public async Task<int?> PlayCard(string game, string idPlayer, string cardid, int planetindex)
        {
            var room = GetGameRoomData(game);
            if (room is null)
            {
                _logger.LogWarning("No se encontró partida {game} en el Request PlayCard ", game);
                return null;
            }

            var result = room.PlayCard(idPlayer, cardid, planetindex);
            _logger.LogInformation("Request PlayCard para {idPlayer} completada", idPlayer);
            return result switch
            {
                null => -1,
                true => 1,
                false => 0
            };
        }

        public async Task<List<Gameroom>?> GetAllGamerooms()
        {
            var roomList = await _gameDb.GetAllGamerooms();
            if (roomList is null)
            {
                _logger.LogWarning("No se encontró ningún Gameroom");
                return null;
            }

            _logger.LogInformation("Request GetAllGameRooms completada");
            return roomList;
        }

        public async Task<Gameroom?>? GetGameroom(string id)
        {
            Gameroom? room = await _gameDb.GetGameroom(id);
            if (room is null)
            {
                _logger.LogWarning("No se encontró ningún Gameroom con {id}", id);
                return null;
            }

            _logger.LogInformation("Request GetAllGameRooms para {id} completada", id);
            return room;
        }

        public GameRoom? GetGameRoomData(string id)
        {
            GameRoom? room = ActiveRooms.Find(x => x.Roomid == id);
            _logger.LogInformation("Request GetGameRoomData para {id} completada", id);
            return room;
        }

        public async Task<bool?> EndTurn(string idRoom, string idUser)
        {
            var task = GetGameRoomData(idRoom)?.EndTurn(idUser);
            if (task is null)
            {
                _logger.LogWarning("No se encontró ningún Jugador con {id}, en partida {idRoom}", idUser, idRoom);
                return null;
            }

            await task;
            _logger.LogInformation("Request EndTurn para partida {idRoom} y usuario {idUser} completada", idRoom,
                idUser);
            return true;
        }

        public static async Task<bool?> IsInGame(string id)
        {
            var Accounts = await MatchMackingContext.Accounts.ToListAsync();
            var account = Accounts.Find(x => x.Id == id);
            return account is null ? null : account.isplaying;
        }

        public async Task<bool?> Surrender(string idRoom, string idUser)
        {
            var room = ActiveRooms.Find(x => x.Roomid == idRoom);
            if (room is null)
            {
                _logger.LogWarning("No se encontró ningún Jugador con {id}, en partida {idRoom}", idUser, idRoom);
                return null;
            }

            return room.Surrender(idUser);
        }
    }

    public class FakeGameLogic : GameLogic
    {
        public FakeGameLogic(StardeckContext gameContext, ILogger logger) : base(gameContext, logger)
        {
            FakeGameLogic.MatchMackingContext = gameContext;
        }

        public static StardeckContext MatchMackingContext { get; set; }
    }
}