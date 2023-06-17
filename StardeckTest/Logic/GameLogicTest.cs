using Microsoft.Extensions.Logging.Abstractions;
using Stardeck.Controllers;
using Stardeck.GameModels;
using Stardeck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stardeck.Logic;

namespace StardeckTest.Logic
{
    [TestClass]
    public class GameLogicTest
    {
        [TestMethod]
        public void GameFoundTest()
        {
            var logic = new Stardeck.Logic.GameLogic(new FakeStardeckContext(), new NullLogger<GameLogic>());

            var response = logic.GetGameroom("G-202903a7d04e").Result;

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(typeof(Gameroom),response.GetType() );

        }

        [TestMethod]
        public void AllGamesFoundTest()
        {
            var logic = new Stardeck.Logic.GameLogic(new FakeStardeckContext(), new NullLogger<GameLogic>());
            var response = logic.GetAllGamerooms().Result;
            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual( typeof(List<Gameroom>),response.GetType());
        }

        [TestMethod]
        [Ignore]
        public void GetRoomDataTest()
        {
            var logic = new Stardeck.Logic.GameLogic(new FakeStardeckContext(), new NullLogger<GameLogic>());

            var response = logic.GetGameRoomData("G-202903a7d04e");

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(response.GetType(), typeof(Gameroom));

        }
    }
}
