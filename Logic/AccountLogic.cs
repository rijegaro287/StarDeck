using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stardeck.Models;
using System.Text.RegularExpressions;
using System;
using System.Net;
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
            List<Account> accounts= context.Accounts.ToList();
            if(accounts.Count == 0)
            {
                return null;
            }
            return accounts;
        }

        public string[] GetCards(string accountId)
        {
            var collection = context.Collections.Find(accountId);
            if (collection?.Collection1 == null) { 
                return null; 
            }
            return collection.Collection1;

        }

        public Account GetAccount(string id)
        {
            //var card = context.Cards.Where(x=> x.Id==id).Include(x=>x.Navigator);
            var acc = context.Accounts.Find(id);

            if (acc == null)
            {
                return null;
            }
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
                AddCardsLogic(accAux.Id, cards[index].Id);
                cards.RemoveAt(index);
            }


            return accAux;

        }

        public string[] AddCardsLogic(string accountId, string cardId)
        {
            var collection = context.Collections.Find(accountId);
            if (collection == null)
            {
                collection = new(new List<string>().ToArray())
                {
                    IdAccount = accountId
                };
                collection.Collectionlist.Add(cardId);
                context.Collections.Add(collection);
                context.SaveChanges();
                return collection.Collection1;
            }
            else
            {
                {
                    if (collection.Collectionlist.Contains(cardId))
                    {
                        string[] r = new string[1];
                        r[0] ="Ya en coleccion";
                        return r;
                    }
                    else
                    {
                        collection.Collectionlist.Add(cardId);
                        context.SaveChanges();
                        return collection.Collection1;
                    }
                }
            }
        }

        public Account UpdateAccount(string id, Account nAcc)
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

        public Account DeleteAccount(string id)
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



    }
}
