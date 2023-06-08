using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stardeck.GameModels;
using Stardeck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stardeck.Engine;
using Microsoft.Extensions.Logging.Abstractions;
using Stardeck.Controllers;

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
            PlayerModel player1 = new PlayerLogic(player);
            Assert.IsNotNull(player1,"No se logro transformar la cuenta de testeo a jugador");
            Assert.AreEqual(player.Id, player1.Id);
            Assert.IsNotNull(player1.Hand);
            Assert.IsNotNull(player1.Deck);
        }
        [TestMethod()]
        public void PlayerInitTest()
        {
            var context = new StardeckContext();
            Account? player = (new Logic.AccountLogic(context, new NullLogger<GameController>())).GetAccountWithFavoriteDeck("U-RXF7RJNBWEKD");
            Assert.IsNotNull(player);
            PlayerModel? player1 = new PlayerLogic(player);
            Assert.IsNotNull(player1);

            Assert.IsTrue(GameRoomBuilder.InitPlayer(context, player1),"El jugador no se inicializo bien");

            Assert.IsNotNull(player1.Deck,"Al jugador no se le asigno un deck");
            Assert.IsNotNull(player1.Hand,"Al jugador no se le asigno una mano") ;

        }

        [TestMethod()]
        [ExpectedException(typeof(System.NullReferenceException))]
        public void PlayerCreationAndInitErrorTest()
        {
            GameRoomBuilder.InitPlayer(new StardeckContext(), new PlayerLogic(player));
        }
    }
}