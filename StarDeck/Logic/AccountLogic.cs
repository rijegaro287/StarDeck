using Microsoft.EntityFrameworkCore;
using Stardeck.Models;
using System.Text.RegularExpressions;
//using System.Web.Mvc;

namespace Stardeck.Logic
{
    public class AccountLogic
    {
        private readonly StardeckContext context;

        public AccountLogic(StardeckContext context)
        {
            this.context = context;
        }


        public List<Account> GetAll()
        {
            List<Account> accounts = context.Accounts.ToList();
            if (accounts.Count == 0)
            {
                return null;
            }
            return accounts;
        }

        public string[] GetCards(string accountId)
        {
            var collection = context.Collections.Find(accountId);
            if (collection?.Collection1 == null)
            {
                return null;
            }
            return collection.Collection1;

        }

        public Account? GetAccount(string id)
        {
            //var card = context.Cards.Where(x=> x.Id==id).Include(x=>x.Navigator);
            var acc = context.Accounts.Find(id);
            return acc;
        }

        public Account NewAccount(Account acc)
        {
            if (context.Avatars.Find(acc.Avatar) == null)
            {
                Avatar a = new()
                {
                    Id = 9998,
                    Image = Convert.FromBase64String("0001"),
                    Name = "joli"
                };
                context.Avatars.Add(a);
                acc.Avatar = a.Id;
            }

            context.SaveChanges();
            var accAux = new Account("{'rol':'User'}")
            {
                Id = acc.Id,
                Name = acc.Name,
                Nickname = acc.Nickname,
                Email = acc.Email,
                Country = acc.Country,
                Password = acc.Password,
                Avatar = acc.Avatar,
                Config = acc.Config,


            };
            while (!Regex.IsMatch(accAux.Id, @"^U-[a-zA-Z0-9]{12}"))
            {
                accAux.Id = string.Concat("U-", System.Guid.NewGuid().ToString().Replace("-", "").AsSpan(0, 12));
            }

            context.Accounts.Add(accAux);
            context.SaveChanges();

            //Add initial cards
            var cards = context.Cards.Where(x => x.Type == 0).ToList();
            var ran = new Random();
            for (int i = 0; i < 15; i++)
            {
                var index = ran.Next(cards.Count);
                addCardsToCollection(accAux.Id, cards[index].Id);
                cards.RemoveAt(index);
            }


            return accAux;

        }
        /// <summary>
        /// Ad a card to the player collection. If is a new player create it collection
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="cardId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"> when a card is already in collection</exception>
        public string[]? addCardsToCollection(string accountId, string cardId)
        {
            var collection = context.Collections.Find(accountId);
            if (collection == null)
            {
                collection = createAndSaveNewCollection(accountId);
            }

            if (collection.Collectionlist.Contains(cardId))
            {
                return null;
            }
            collection.Collectionlist.Add(cardId);
            context.SaveChanges();
            return collection.Collection1;


        }


        private Collection createAndSaveNewCollection(string accountId)
        {
            var collection = new Collection(new List<string>().ToArray())
            {
                IdAccount = accountId
            };
            context.Collections.Add(collection);
            context.SaveChanges();
            return collection;
        }

        public Account? UpdateAccount(string id, Account nAcc)
        {
            var acc = context.Accounts.Find(id);
            if (acc != null)
            {
                acc.Id = nAcc.Id;
                acc.Name = nAcc.Name;
                acc.Email = nAcc.Email;
                acc.Country = nAcc.Country;
                acc.Password = nAcc.Password;
                acc.Active = nAcc.Active;
                acc.Avatar = nAcc.Avatar;
                acc.Points = nAcc.Points;
                acc.Coins = nAcc.Coins;
                acc.Config = nAcc.Config;
                nAcc.Serverconfig.main = acc.Serverconfig.main;
                acc.Serverconfig = nAcc.Serverconfig;

                context.SaveChanges();
                return acc;
            }
            return null;

        }

        public Account? DeleteAccount(string id)
        {
            var acc = context.Accounts.Find(id);
            if (acc != null)
            {
                //REMOVER LOS DATOS ASOCIADOS EN OTRAS TABLAS
                context.Remove(acc);
                context.SaveChanges();
                return acc;
            }
            return null;
        }


        public Collection DeleteCard(string accountId, string cardId)
        {
            var collection = context.Collections.Find(accountId);
            if (collection == null)
            {
                return null;
            }
            else
            {
                if (collection.Collection1.Contains(cardId))
                {
                    collection.Collection1 = collection.Collection1.Where(x => x != cardId).ToArray();
                    context.SaveChanges();


                    return collection;
                }
                return null;
            }

        }

        /*
        public string GetParam(string id,string param)
        {
            Account account = context.Accounts.Find(id);
            if (account == null) 
            {
                return null;
            }
            string config = account.Config;
            if(config.Contains(param)) 
            { 
            }
        }*/


        public string[]? addCardsListToCollection(string accountId, string[] cardId)
        {
            string[] tmpresult = new List<string>().ToArray();
            foreach (var card in cardId)
            {
                tmpresult = addCardsToCollection(accountId, card);
                if (tmpresult is null) continue;
            }
            return tmpresult;
        }

        public Dictionary<string, string>? GetParameter(string id, string parameter)
        {
            Account? user = GetAccount(id);
            if (user == null) { return null; }
            if (!user.Serverconfig.ContainsKey(parameter.ToLower())) { return null; }
            return new Dictionary<string, string>
            {
                [parameter.ToLower()] = user.Serverconfig[parameter.ToLower()]
            };
        }
        public Dictionary<string, string>? GetParameters(string id)
        {
            Account? user = GetAccount(id);
            if (user == null) { return null; }
            return user.Serverconfig;
        }

        public Dictionary<string, string>? PostParameter(string id, string parameter, string value)
        {
            Account? user = GetAccount(id);
            if (user == null) { return null; }
            if (user.Serverconfig.ContainsKey(parameter.ToLower())) { return null; }
            user.Serverconfig[parameter.ToLower()] = value;
            context.SaveChanges();
            return user.Serverconfig;
        }

        public Dictionary<string, string>? PutParameter(string id, string parameter, string value)
        {
            Account? user = GetAccount(id);
            if (user == null) { return null; }
            if (!user.Serverconfig.ContainsKey(parameter.ToLower())) { return null; }
            user.Serverconfig[parameter.ToLower()] = value;
            context.SaveChanges();
            return user.Serverconfig;
        }

        public bool? SelectFavoriteDeck(string id, string idDeck)
        {
            Account? user = GetAccount(id);
            if (user == null) { return null; }
            Deck? deck = context.Decks.Find(idDeck);
            if (deck == null) { return false; }

            if (deck.IdDeck==user.Id)
            {
                FavoriteDeck actual = context.FavoriteDecks.Find(idDeck);
                if (actual==null)
                {
                    actual = new() { Accountid = user.Id, Deckid = deck.IdDeck};
                    context.FavoriteDecks.Add(actual);
                }
                else
                {
                    actual.Deckid = deck.IdDeck;
                    actual.Deck = deck;
                }
                context.SaveChanges();
            }
            else
            {
                return false;
            }

            return true;
        }

        

    }
}


