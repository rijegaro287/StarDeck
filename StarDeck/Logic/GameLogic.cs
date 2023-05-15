using Stardeck.Models;
using System.Collections.Generic;

namespace Stardeck.Logic
{
    public class GameLogic
    {
        
        private readonly StardeckContext context;

        public GameLogic(StardeckContext context)
        {
            this.context = context;
        }


        public List<Account> IsWaitng(string playerId)
        {
            Account player1 = context.Accounts.Find(playerId);
            Account opponet;
            List<Account> players=context.Accounts.ToList().Where(x => x.isInMatchMacking == true).ToList();
            
            if (players.Count>1 )
            {
                List<Account> battle = new List<Account>();
                Random rnd = new Random();
                int randIndex = rnd.Next(players.Count);
                opponet = players[randIndex];
                if( opponet == player1 )
                {
                    opponet = players[rnd.Next(players.Count)];
                }
                battle.Add(opponet);
                battle.Add(player1 );
                return battle;
            }
            return null;
            
        }

        public List<Account> SetActive(string accountId, bool isInMatchMacking)
        {
            Account account = context.Accounts.Find(accountId);
            if (account == null) { return null; }
            account.isInMatchMacking = isInMatchMacking;
            context.SaveChanges();
            List<Account> players = context.Accounts.ToList().Where(x => x.isInMatchMacking == true).ToList();
            return players;
        }

    }
}
