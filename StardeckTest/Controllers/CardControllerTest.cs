using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Stardeck.Controllers;
using Stardeck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardeckTest.Controllers
{
    [TestClass()]
    public class CardControllerTest
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
            var controller = new CardController(new FakeStardeckContext(), new NullLogger<CardController>());
            var response = controller.Post(card);
            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<OkObjectResult>(response.Result);
        }


        [TestMethod]
        public void CardFoundTest()
        {
            var controller = new CardController(new FakeStardeckContext(), new NullLogger<CardController>());
            var response = controller.Get("C-9ae900e6feec");
            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<OkObjectResult>(response.Result);
        }


        [TestMethod]
        public void AllCardsFoundTest()
        {
            var controller = new CardController(new FakeStardeckContext(), new NullLogger<CardController>());
            var response = controller.Get();
            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<OkObjectResult>(response.Result);
        }

        [TestMethod]
        public void GetNineCardsTest()
        {
            var controller = new CardController(new FakeStardeckContext(), new NullLogger<CardController>());
            var response = controller.GetNineCards();
            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<OkObjectResult>(response.Result);
        }



    }
}
