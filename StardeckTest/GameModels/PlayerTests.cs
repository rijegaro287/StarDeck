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
    public class PlayerTests
    {
        Models.Account? player;
        [TestInitialize]
        public void poblateData()
        {
            player = new Account("{}")
            {
                Id = ""
            };
        }

        [TestMethod()]
        public void PlayerCreationTest()
        {
            Account player=(new StardeckContext()).Accounts.Find("U-RXF7RJNBWEKD");
            Assert.IsNotNull(player, "No se encontro la cuenta de testeo");
            Player player1 = new Player(player);
            Assert.IsNotNull(player1,"No se logro transformar la cuenta de testeo a jugador");
            Assert.AreEqual(player.Id, player1.Id);
            Assert.IsNotNull(player1.Hand);
            Assert.IsNotNull(player1.Deck);
        }
        [TestMethod()]
        public void PlayerInitTest()
        {
            Account? player = (new Logic.AccountLogic(new())).GetAccountWithFavoriteDeck("U-RXF7RJNBWEKD");
            Assert.IsNotNull(player);
            Player? player1 = new Player(player);
            Assert.IsNotNull(player1);

            Assert.IsTrue(player1.Init(),"El jugador no se inicializo bien");

            Assert.IsNotNull(player1.GetDeck(),"Al jugador no se le asigno un deck");
            Assert.IsNotNull(player1.GetHand(),"Al jugador no se le asigno una mano") ;

        }

        [TestMethod()]
        [ExpectedException(typeof(System.NullReferenceException))]
        public void PlayerCreationAndInitErrorTest()
        {
            new Player(player).Init();
        }
    }
}