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
    [TestClass]
    public class DeckControllerTest
    {

        Deck deck;


        [TestInitialize]
        public void poblateData()
        {
            deck = new Deck(new string[] { "C-9ae900e6feec" })
            {
                Id = "",
                IdAccount = "U-RXF7RJNBWEKD",
                Cardlist = new string[] { "C-9ae900e6feec" }

            };
        }
        [TestMethod]
        public void DeckCreationTest()
        {
            var controller = new DeckController(new FakeStardeckContext(), new NullLogger<DeckController>());
            var response = controller.Post(deck);
            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<OkObjectResult>(response.Result);
        }

        [TestMethod]
        public void DeckFoundTest()
        {
            var controller = new DeckController(new FakeStardeckContext(), new NullLogger<DeckController>());
            var response = controller.Get("D-100bfff317c4");
            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<OkObjectResult>(response.Result);
        }


        [TestMethod]
        public void AllDecksFoundTest()
        {
            var controller = new DeckController(new FakeStardeckContext(), new NullLogger<DeckController>());
            var response = controller.Get();
            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<OkObjectResult>(response.Result);
        }

        [TestMethod]
        public void GetNamesTest()
        {
            var controller = new DeckController(new FakeStardeckContext(), new NullLogger<DeckController>());
            var response = controller.GetNames("U-XA8G5I3ZEZJV");
            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<OkObjectResult>(response.Result);
        }

        [TestMethod]
        public void GetDecksByUserTest()
        {
            var controller = new DeckController(new FakeStardeckContext(), new NullLogger<DeckController>());
            var response = controller.GetAllDecksByUser("U-XA8G5I3ZEZJV");
            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<OkObjectResult>(response.Result);
        }



    }
}
