using Stardeck.Models;

namespace Stardeck.DbAccess
{
    public class AccountDb
    {
        private readonly StardeckContext context;

        public AccountDb(StardeckContext context)
        {
            this.context = context;
        }


        public List<Account> GetAllAccounts()
        {
            List<Account> accounts = context.Accounts.ToList();
            if (accounts.Count == 0)
            {
                return null;
            }
            return accounts;
        }

        public Account? GetAccount(string id)
        {
            var acc = context.Accounts.Find(id);
            if(acc == null)
            {
                return null;
            }
            return acc;
        }

        public string[] GetAccountCards(string accountId)
        {
            var collection = context.Collections.Find(accountId);
            if (collection?.Collection1 == null)
            {
                return null;
            }
            return collection.Collection1;
        }

        public Account NewAccount(Account acc)
        {
            context.Accounts.Add(acc);
            context.SaveChanges();

            if (GetAccount(acc.Id) == null)
            {
                return null;
            }

            return acc;

        }

        public string[]? AddCardsToCollection(string accountId, string cardId)
        {
            var collection = context.Collections.Find(accountId);
            if (collection == null)
            {
                collection = CreateAndSaveNewCollection(accountId);
            }

            if (collection.Collectionlist.Contains(cardId))
            {
                return null;
            }
            collection.Collectionlist.Add(cardId);
            context.SaveChanges();
            return collection.Collection1;


        }


        private Collection CreateAndSaveNewCollection(string accountId)
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
            var acc = GetAccount(id);
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
            var acc = GetAccount(id);
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


        public string[]? AddCardsListToCollection(string accountId, string[] cardId)
        {
            string[] tmpresult = new List<string>().ToArray();
            foreach (var card in cardId)
            {
                tmpresult = AddCardsToCollection(accountId, card);
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

            if (deck.IdAccount == user.Id)
            {
                FavoriteDeck actual = context.FavoriteDecks.Find(idDeck);
                if (actual == null)
                {
                    actual = new() { Accountid = user.Id, Deckid = deck.IdDeck };
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
