using Stardeck.Models;
using System.Collections.Generic;
using System.Timers;
using Microsoft.EntityFrameworkCore;
using Stardeck.GameModels;

namespace Stardeck.Logic
{
    public class GameLogic
    {
        private static readonly StardeckContext MatchMackingcontext = new();
        private StardeckContext _context;
        private static readonly List<GameModels.GameRoom> ActiveRooms = new List<GameModels.GameRoom>();

        public GameLogic(StardeckContext context)
        {
            _context = context;
        }


        public async Task<GameRoom?> IsWaiting(string playerId)
        {
            await PutInMatchMaking(playerId, true);
            var player1 = await MatchMackingcontext.Accounts.FindAsync(playerId);
            if (player1 is null)
            {
                throw new Exception("Player not found");
            }

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

            player1.isInMatchMacking = false;
            return null;
        }

        public async Task<bool?> PutInMatchMaking(string accountId, bool isInMatchMacking)
        {
            Account? account = await MatchMackingcontext.Accounts.FindAsync(accountId);
            if (account is null)
            {
                return null;
            }

            var selectedDeck = await _context.FavoriteDecks.FindAsync(accountId);
            account.FavoriteDeck = selectedDeck;
            account.isInMatchMacking = isInMatchMacking;
            return isInMatchMacking;
        }

        public List<Gameroom?>? GetAllGamerooms()
        {
            List<Gameroom?> roomList = _context.Gamerooms.ToList();
            return roomList.Count == 0 ? null : roomList;
        }

        public Gameroom? GetGameroom(string id)
        {
            Gameroom? room = _context.Gamerooms.Find(id);
            return room;
        }

        public GameRoom? GetGameRoomData(string id)
        {
            GameRoom? room = ActiveRooms.Find(x => x.Roomid == id);
            return room;
        }
    }
}