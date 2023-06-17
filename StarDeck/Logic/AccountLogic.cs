using Microsoft.EntityFrameworkCore;
using Stardeck.Controllers;
using Stardeck.DbAccess;
using Stardeck.Models;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;

//using System.Web.Mvc;

namespace Stardeck.Logic
{
    public class AccountLogic
    {
        private readonly StardeckContext context;
        private readonly AccountDb accountDB;
        private readonly AvatarDb avatarDB;
        private readonly CardDb cardDB;
        private readonly ILogger _logger;

        public AccountLogic(StardeckContext context, ILogger logger)
        {
            _logger = logger;
            this.context = context;
            this.avatarDB = new AvatarDb(context);
            this.accountDB = new AccountDb(context);
            this.cardDB = new CardDb(context);
        }


        public List<Account>? GetAll()
        {
            List<Account>? accounts = accountDB.GetAllAccounts();
            _logger.LogInformation("Request GetAll de Accounts completada");
            return accounts;
        }

        public Account? GetAccount(string id)
        {
            var acc = accountDB.GetAccount(id);
            if (acc == null)
            {
                _logger.LogWarning("No se encontró cuenta en GetAccount para {id}", id);
                return null;
            }

            _logger.LogInformation("Request GetAccount para {id} completada", id);
            return acc;
        }

        public string[] GetCards(string accountId)
        {
            var collection = accountDB.GetAccountCards(accountId);
            if (collection == null)
            {
                _logger.LogWarning("No se encontró colección en GetCards para {id}", accountId);
                return null;
            }

            _logger.LogInformation("Request GetCards para {id} completada", accountId);
            return collection;
        }

        public Dictionary<string, string>? GetParameter(string id, string parameter)
        {
            Account? user = GetAccount(id);
            if (user == null)
            {
                _logger.LogWarning("No se encontró Usuario en GetParameters para {id}", id);
                return null;
            }

            if (!user.Serverconfig.ContainsKey(parameter.ToLower()))
            {
                _logger.LogWarning("No se encontró Parámetro {parameter} en GetParameters para usuario{id}", parameter,
                    id);
                return null;
            }

            _logger.LogInformation("Request GetParameter para {id} completada", id);
            return new Dictionary<string, string>
            {
                [parameter.ToLower()] = user.Serverconfig[parameter.ToLower()]
            };
        }

        public Dictionary<string, string>? GetParameters(string id)
        {
            Account? user = GetAccount(id);
            if (user == null)
            {
                _logger.LogWarning("No se encontró usuario {id} en GetParameters ", id);
                return null;
            }

            _logger.LogInformation("Request GetParameters para {id} completada", id);
            return user.Serverconfig;
        }

        public Account? GetAccountWithFavoriteDeck(string id)
        {
            //var card = context.Cards.Where(x=> x.Id==id).Include(x=>x.Navigator);
            var acc = context.Accounts.Include(x => x.FavoriteDeck).First(x => x.Id == id);
            _logger.LogInformation("Request GetAccountWithFavoriteDeck para {id} completada", id);
            return acc;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="acc"></param>
        /// <returns>1 if account created with cards, -1 error in creation of account, -2 error assignin cards to account, null accoutn already exist </returns>
        public int? NewAccount(Account acc)
        {
            if (avatarDB.GetAvatar(acc.Avatar) == null)
            {
                Avatar a = new()
                {
                    Id = 1111,
                    Image = Convert.FromBase64String("0001"),
                    Name = "default"
                };
                avatarDB.NewAvatar(a);
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

            accAux.generateID();

            var save = accountDB.NewAccount(accAux);
            if (save.Equals(null))
            {
                //La cuenta ya existe
                _logger.LogWarning("Ya existe una cuenta para {id}", acc.Id);
                return null;
            }

            if (save == false)
            {
                //fallo al guardar la cuenta
                _logger.LogWarning("No se pudo guardar la cuenta {id}", acc.Id);
                return -1;
            }

            //Add initial cards from basic
            //var cards = context.Cards.Where(x => x.Type == 0).ToList();
            var cards = cardDB.GetCardByType(0);
            //Cards empty
            if (cards == null)
            {
                //fallo al guardar las cartas
                _logger.LogWarning("No se guardaron las cartas de la cuenta nueva {id}", acc.Id);
                return -2;
#warning se quedaria la cuenta creada sin cartas, borrarla
            }

            var ran = new Random();
            for (int i = 0; i < 15; i++)
            {
                var index = ran.Next(cards.Count);
                AddCardsToCollection(accAux.Id, cards[index].Id);
                cards.RemoveAt(index);
            }

            _logger.LogInformation("Request NewAccount para {id} completada", acc.Id);
            return 1;
        }

        /// <summary>
        /// Ad a card to the player collection. If is a new player create it collection
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="cardId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"> when a card is already in collection</exception>
        public string[]? AddCardsToCollection(string accountId, string cardId)
        {
            var collection = context.Collections.Find(accountId);
            if (collection == null)
            {
                collection = CreateAndSaveNewCollection(accountId);
            }

            if (collection.Collectionlist.Contains(cardId))
            {
                _logger.LogWarning("El usuario {accountId} ya tiene la carta {cardId} en su colección", accountId,
                    cardId);
                return null;
            }

            collection.Collectionlist.Add(cardId);
            context.SaveChanges();
            _logger.LogInformation("Request AddCardsToCollection para {id} completada", accountId);
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
            _logger.LogInformation("Request CreateAndSaveNewCollection para {id} completada", accountId);
            return collection;
        }

        public string[]? AddCardsListToCollection(string accountId, string[] cardId)
        {
            string[] tmpresult = new List<string>().ToArray();
            foreach (var card in cardId)
            {
                tmpresult = AddCardsToCollection(accountId, card);
                if (tmpresult is null) continue;
            }

            _logger.LogInformation("Request AddCardsListToCollection para {id} completada", accountId);
            return tmpresult;
        }

        public Dictionary<string, string>? PostParameter(string id, string parameter, string value)
        {
            Account? user = GetAccount(id);
            if (user == null)
            {
                _logger.LogWarning("no se encuentra usuario {id} para request PostParameter", id);
                return null;
            }

            if (user.Serverconfig.ContainsKey(parameter.ToLower()))
            {
                return null;
            }

            user.Serverconfig[parameter.ToLower()] = value;
            context.SaveChanges();
            _logger.LogInformation("Request PostParameter para {id} completada", id);
            return user.Serverconfig;
        }


        public Account? UpdateAccount(string id, Account nAcc)
        {
            var acc = accountDB.GetAccount(id);

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
                _logger.LogInformation("Request UpdateAccount para {id} completada", id);
                return acc;
            }

            _logger.LogWarning("No se encontró cuenta con {id} en UpdateAccount", id);
            return null;
        }

        public bool? SelectFavoriteDeck(string id, string idDeck)
        {
            Account? user = GetAccount(id);
            if (user is null)
            {
                _logger.LogWarning("No se encontró cuenta con {id} en SelectFavoriteDeck", id);
                return null;
            }

            Deck? deck = context.Decks.Find(idDeck);
            if (deck is null)
            {
                _logger.LogWarning("No se encontró deck con {idDeck} para el usuario {id} en SelectFavoriteDeck",
                    idDeck, id);
                return false;
            }

            if (deck.IdAccount == user.Id)
            {
                FavoriteDeck? actual = context.FavoriteDecks.Find(id);
                if (actual is null)
                {
                    actual = new FavoriteDeck { Accountid = user.Id, Deckid = deck.IdDeck };
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
                _logger.LogWarning(
                    "El usuario asociado a {idDeck} no coincide con el usuario dado {id} en SelectFavoriteDeck", idDeck,
                    id);
                return false;
            }

            _logger.LogInformation("Request SelectFavoriteDeck para usuario {id} y deck{idDeck} completada", id,
                idDeck);
            return true;
        }

        public Dictionary<string, string>? PutParameter(string id, string parameter, string value)
        {
            Account? user = GetAccount(id);
            if (user == null)
            {
                _logger.LogWarning("No se encontró usuario {id} en PutParameter", id);
                return null;
            }

            if (!user.Serverconfig.ContainsKey(parameter.ToLower()))
            {
                _logger.LogWarning("Ya existe el parámetro {parameter} para el usuario {id} en PutParameter", parameter,
                    id);
                return null;
            }

            user.Serverconfig[parameter.ToLower()] = value;
            context.SaveChanges();
            _logger.LogInformation("Request PutParameter para usuario {id} y parámetro {parameter} completada", id,
                parameter);
            return user.Serverconfig;
        }


        public Account? DeleteAccount(string id)
        {
            var acc = accountDB.DeleteAccount(id);
            if (acc != null)
            {
                _logger.LogInformation("Request DeleteAccount para usuario {id} completada", id);
                return acc;
            }

            _logger.LogWarning("Request DeleteAccount falló para usuario {id}", id);
            return null;
        }


        public Collection DeleteCard(string accountId, string cardId)
        {
            var collection = context.Collections.Find(accountId);
            if (collection == null)
            {
                _logger.LogWarning("No se encontró colección para el usuario {accountId} en DeleteCard", accountId);
                return null;
            }
            else
            {
                if (collection.Collection1.Contains(cardId))
                {
                    collection.Collection1 = collection.Collection1.Where(x => x != cardId).ToArray();
                    context.SaveChanges();

                    _logger.LogWarning("Request DeleteCard para usuario {id} y la carta {cardId} completada", accountId,
                        cardId);
                    return collection;
                }

                _logger.LogWarning(
                    "No se encontró la carta {cardId} en colección para el usuario {accountId} en DeleteCard", cardId,
                    accountId);
                return null;
            }
        }

        public void ManageEnd(long? bet, string player1Id, string player2Id)
        {
            var winner = GetAccount(player1Id);
            var looser = GetAccount(player2Id);

            if (winner is null || looser is null)
            {
                _logger.LogWarning("No se encontró usuario {player1Id} o {player2Id} en ManageEnd", player1Id,
                    player2Id);
                return;
            }

            winner.Points += 1;
            winner.Coins += 1;
            if (bet is not null)
            {
                winner.Coins += (int)bet;
                looser.Coins -= (int)bet;
            }

            context.SaveChanges();
        }

        public object GetRanking(bool individual,string accountId)
        {
            var accounts = GetAll().Select(x => new { Nickname = x.Nickname, Points = x.Points }).ToList();
            accounts = accounts.OrderByDescending(x => x.Points).ThenBy(x => x.Nickname) .ToList();
            if (individual)
            {
                int counter=1;
                foreach(var account in accounts)
                {
                    var nick=GetAccount(accountId).Nickname;
                    if (account.Nickname == nick)
                    {
                        return new
                        {
                            Position = counter,
                            Nickname = account.Nickname,
                            Points = account.Points,
                        };
                    }
                    counter++;
                }
            }
            
            
            return accounts;

        }
    }
}