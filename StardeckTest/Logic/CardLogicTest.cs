using Microsoft.Extensions.Logging.Abstractions;
using Stardeck.Controllers;
using Stardeck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StardeckTest.Logic
{
    [TestClass]
    public class CardLogicTest
    {

        CardImage card;


        [TestInitialize]
        public void poblateData()
        {
            card = new CardImage()
            {
                Id = "",
                Name = "TESTING CARD",
                Energy = 10,
                Battlecost = 3,
                Image = "0001",
                Active = true,
                Type = 0,
                Description = "CARTA DE TESTING",
                Race = "Rara"

            };
        }
        [TestMethod]
        public void CardCreationTest()
        {
            var logic = new Stardeck.Logic.CardLogic(new FakeStardeckContext(), new NullLogger<GameController>());
            var response = logic.NewCard(card);
            //Assert
            Assert.IsTrue(response);
            
        }

        [TestMethod]
        public void CardCheckIdTest()
        {
            card.generateID();

            //Assert
            Assert.IsTrue(Regex.IsMatch(card.Id, @"^C-[a-zA-Z0-9]{12}"));
        }

        [TestMethod]
        public void CardFoundTest()
        {
            var logic = new Stardeck.Logic.CardLogic(new FakeStardeckContext(), new NullLogger<GameController>());

            var response = logic.GetCard("C-9ae900e6feec");

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(response.GetType(), typeof(Card));

        }

        [TestMethod]
        public void AllCardsFoundTest()
        {
            var logic = new Stardeck.Logic.CardLogic(new FakeStardeckContext(), new NullLogger<GameController>());
            var response = logic.GetAll();
            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(response.GetType(), typeof(List<Card>));
        }

        [TestMethod]
        public void GetNineCardsTest()
        {
            var logic = new Stardeck.Logic.CardLogic(new FakeStardeckContext(), new NullLogger<GameController>());
            var response = logic.GetNineCards();
            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(response.GetType(), typeof(List<Card>));
            Assert.AreEqual(response.Count, 9);
        }


    }
}
