using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stardeck.GameModels;
using Stardeck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stardeck.GameModels.Tests
{
    [TestClass()]
    public class GameRoomTests
    {

        Gameroom? room;
        [TestInitialize]
        public void poblateData()
        {
            room = new Gameroom
            {
                Player1 = "U-123456789012",
                Player2 = "U-234567890123"
            };
        }
        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GameRoomCreationErrorTest()
        {
            var roomAux = new GameRoom(room);
        }


        /**
    //Esta Unit Test esta muy compleja, es posible que sea mejor dividirla en varias
        [TestMethod()]
        public void GameRoomCreationTest()
        {
            StardeckContext context = new StardeckContext();
            setDBcache(context);
            Account player1 = context.Accounts.Include(x=>x.FavoriteDeck).FirstOrDefault(x=>x.Id== "U-012345678901");
            Account player2 = context.Accounts.Include(x => x.FavoriteDeck).FirstOrDefault(x => x.Id == "U-123456789012");
            room.Player1 = player1.Id;
            room.Player1Navigation = player1;

            room.Player2 = player2.Id;
            room.Player2Navigation = player2;

            var roomAux = new GameRoom(room);

        }
    private void setDBcache(StardeckContext context)
    {
            var acc1 = new Account("{}")
            {
                Id = "U-012345678901",
                Name = "Prueba2",
                Password = "Prueba",
                Email = "b@b.com",
                Nickname = "Prueba2",
            };
            context.Accounts.Add(acc1);
            var acc2 = new Account("{}")
            {
                Id = "U-123456789012",
                Name = "Prueba3",
                Password = "Prueba",
                Email = "a@a.com",
                Nickname = "Prueba3",
            };
            context.Accounts.Add(acc2);

            for (int i = 10; i < 36; i++)
            {
                context.Cards.Add(new Card
                {
                    Id = "C-1234567890"+(i.ToString()),
                    Name = "Prueba"+ (i.ToString()),
                    Type = 0,
                    Battlecost = 1,
                    Description = "Prueba"
                });
            }
            var cards = context.Cards.ToList();
            Deck deck1 = new(new List<string>().ToArray());
            Deck deck2 = new(new List<string>().ToArray());
            foreach (var item in cards)
            {
                deck1.addCard(item.Id);
            }
            cards.Reverse();
            foreach (var item in cards)
            {
                deck1.addCard(item.Id);
            }
            var favDeck1 = new FavoriteDeck()
            {
                Deck = deck1,Account=acc1,Deckid=deck1.Id,Accountid=acc1.Id
            };
            var favDeck2 = new FavoriteDeck()
            {
                Deck = deck2,Account=acc2,Deckid=deck2.Id,Accountid=acc2.Id
            };
            acc1.FavoriteDeck = favDeck1;
            acc2.FavoriteDeck = favDeck2;
            
        }
        */

    }
}