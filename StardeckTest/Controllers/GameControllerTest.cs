using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Stardeck.Controllers;
using Stardeck.Logic;
using Stardeck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardeckTest.Controllers
{
    [TestClass]
    public class GameControllerTest
    {

        [TestMethod]
        public void GameFoundTest()
        {
            var controller = new GameController(new FakeStardeckContext(), new NullLogger<GameController>());

            var response = controller.Get("G-202903a7d04e");

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<OkObjectResult>(response.Result);

        }

        [TestMethod]
        public void AllGamesFoundTest()
        {
            var controller = new GameController(new FakeStardeckContext(), new NullLogger<GameController>());

            var response = controller.Get();

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<OkObjectResult>(response.Result);
        }

        [TestMethod]
        public void IsInGameTest()
        {
            var controller = new GameController(new FakeStardeckContext(), new NullLogger<GameController>());

            var response = controller.IsInGame("U-RXF7RJNBWEKD");

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<NotFoundObjectResult>(response.Result);
        }

        [TestMethod]
        public void PutInMatchMakingTest()
        {
            var controller = new GameController(new FakeStardeckContext(), new NullLogger<GameController>());

            var response = controller.Put("U-RXF7RJNBWEKD",true);

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<OkObjectResult>(response.Result);
        }

    }
}
