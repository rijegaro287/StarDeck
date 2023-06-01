using Stardeck.Models;
using System.Collections.Generic;
using System.Timers;
using Microsoft.EntityFrameworkCore;
using Stardeck.GameModels;
using Stardeck.DbAccess;
using Stardeck.Pages;

namespace Stardeck.Logic
{
    public class GameLogic
    {
        private static StardeckContext gameContext;
        private static readonly List<GameModels.GameRoom> ActiveRooms = new List<GameModels.GameRoom>();
        private readonly GameDb gameDB;

        public GameLogic(StardeckContext gameContext)
        {
            GameLogic.gameContext = gameContext;
            this.gameDB=new GameDb(gameContext);
        }
        


        public async Task<GameRoom?> IsWaiting(string playerId)
        {

            var player1 = gameContext.Accounts.Find(playerId);
            if (player1 is null)
            {
                throw new Exception("Player not found");
            }

            var counter = 0;
            player1.isInMatchMacking = true;
            while (player1.isInMatchMacking==true && counter < 15)
            {
                counter += 1;
                var players = gameContext.Accounts.Include(x => x.FavoriteDeck).ToList()
                    .Where(x => x.isInMatchMacking == true).ToList();
                var inRangePlayers =
                    players.Where(x => x.Id != playerId
                                       && Math.Abs(x.Points - player1.Points) < 101).ToList();

                if (inRangePlayers.Count == 0)
                {
                   await Task.Delay(1500);
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
                var room = new GameRoom(battle);
                room.Init();
                ActiveRooms.Add(room);
                return room;
            }

            return null;

        }

        public async Task<bool?> PutInMatchMaking(string accountId, bool isInMatchMacking)
        {
            Account? account = await gameContext.Accounts.FindAsync(accountId);
            if (account is null)
            {
                return null;
            }

            account.isInMatchMacking = isInMatchMacking;
            return isInMatchMacking;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="game"> gameid</param>
        /// <param name="idPlayer">playerid</param>
        /// <param name="cardid">card to play</param>
        /// <param name="planetid">planet where play card</param>
        /// <returns>1 if succes, 0 if not enough energy, null if GameRoom not founded, -1 if invalid player,card or planet id </returns>
        public async Task<int?> PlayCard(string game,string idPlayer, string cardid, string planetid)
        {
                var room = GetGameRoomData(game);
                if (room is null)
                {
                    return null;
                }
                var result=room.PlayCard(idPlayer,cardid,planetid);
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
            if(roomList is null)
            {
                return null;
            }
            return roomList;
        }

        public Gameroom GetGameroom(string id)
        {
            Gameroom room = gameDB.GetGameroom(id);
            if (room is null)
            {
                return null;
            }
            return room;
        }

        public GameRoom? GetGameRoomData(string id)
        {
            GameRoom? room = ActiveRooms.Find(x => x.Roomid == id);
            return room;
        }


    }
}