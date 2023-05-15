using Stardeck.Models;
using System.Collections.Generic;
using System.Timers;
using Microsoft.EntityFrameworkCore;
using Stardeck.GameModels;

namespace Stardeck.Logic
{
    public class GameLogic
    {
        private static StardeckContext _context;
        private static readonly List<GameModels.GameRoom> ActiveRooms = new List<GameModels.GameRoom>();

        public GameLogic(StardeckContext context)
        {
            _context = context;
        }
        


        public static async Task<GameRoom?> IsWaiting(string playerId)
        {

            var player1 = _context.Accounts.Find(playerId);
            if (player1 is null)
            {
                throw new Exception("Player not founded");
            }

            var counter = 0;
            player1.isInMatchMacking = true;
            while (player1.isInMatchMacking==true && counter < 15)
            {
                counter += 1;
                var players = _context.Accounts.Include(x => x.FavoriteDeck).ToList()
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

        public bool? PutInMatchMaking(string accountId, bool isInMatchMacking)
        {
            Account? account = _context.Accounts.Find(accountId);
            if (account is null)
            {
                return null;
            }

            account.isInMatchMacking = isInMatchMacking;
            return isInMatchMacking;
        }
    }
}