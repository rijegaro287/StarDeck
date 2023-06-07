using Stardeck.Models;
using System.Collections.Generic;
using System.Timers;
using Microsoft.EntityFrameworkCore;
using Stardeck.GameModels;
using Stardeck.DbAccess;
using Stardeck.Engine;
using Stardeck.Pages;
using Stardeck.Controllers;
using System.Numerics;

namespace Stardeck.Logic
{
    public class GameLogic
    {
        private static readonly StardeckContext MatchMackingcontext = new();
        private readonly StardeckContext gameContext;
        private static readonly List<GameRoom> ActiveRooms = new List<GameRoom>();
        private readonly GameDb gameDB;
        private readonly ILogger<GameController> _logger;

        public GameLogic(StardeckContext gameContext, ILogger<GameController> logger)
        {
            this.gameContext = gameContext;
            _logger = logger;
            this.gameDB = new GameDb(gameContext);
            
        }

        public async Task<GameRoom?> IsWaiting(string playerId)
        {
            var player1 = await MatchMackingcontext.Accounts.FindAsync(playerId);
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
                var players = MatchMackingcontext.Accounts.ToList()
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
                room = GameRoomBuilder.Init(gameContext, room);
                ActiveRooms.Add(room);
                _logger.LogInformation("Request IsWaiting completada");
                return room;
            }

            player1.isInMatchMacking = false;
            return CheckIfPlaying(player1);
        }

        public GameRoom? CheckIfPlaying(Account player)
        {
            if (!(bool)player.isplaying) return null;
            var room = ActiveRooms.FirstOrDefault(x =>
                (x.Player2.Id == player.Id | x.Player1.Id == player.Id) && x.Turn <= 8);
            if (room is not null)
            {
                _logger.LogInformation("Request CheckIfPlaying para {id} completada",player.Id);
                return room;
            }
            _logger.LogWarning("No se encontró al jugador {player.Id} en el Request CheckIfPlaying ", player.Id);
            return null;
        }

        public async Task<bool?> PutInMatchMaking(string accountId, bool isInMatchMacking)
        {
            Account? account = await MatchMackingcontext.Accounts.FindAsync(accountId);
            if (account is null)
            {
                _logger.LogWarning("No se encontró al jugador {accountId} en el Request PutInMatchMaking ", accountId);
                return null;
            }

            var selectedDeck = await gameContext.FavoriteDecks.FindAsync(accountId);
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

        public List<Gameroom> GetAllGamerooms()
        {
            List<Gameroom> roomList = gameDB.GetAllGamerooms();
            if (roomList is null)
            {
                _logger.LogWarning("No se encontró ningún Gameroom");
                return null;
            }
            _logger.LogInformation("Request GetAllGameRooms completada");
            return roomList;
        }

        public Gameroom GetGameroom(string id)
        {
            Gameroom room = gameDB.GetGameroom(id);
            if (room is null)
            {
                _logger.LogWarning("No se encontró ningún Gameroom con {id}", id);
                return null;
            }
            _logger.LogInformation("Request GetAllGameRooms para {id} completada",id);
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
            _logger.LogInformation("Request EndTurn para partida {idRoom} y usuario {idUser} completada", idRoom, idUser);
            return true;
        }
    }
}