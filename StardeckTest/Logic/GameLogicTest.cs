using Stardeck.GameModels;
using Stardeck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardeckTest.Logic
{
    [TestClass]
    public class GameLogicTest
    {
        [TestMethod]
        public void GameFoundTest()
        {
            var logic = new Stardeck.Logic.GameLogic(new FakeStardeckContext());

            var response = logic.GetGameroom("G-202903a7d04e");

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(response.GetType(), typeof(Gameroom));

        }

        [TestMethod]
        public void AllGamesFoundTest()
        {
            var logic = new Stardeck.Logic.GameLogic(new FakeStardeckContext());
            var response = logic.GetAllGamerooms();
            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(response.GetType(), typeof(List<Gameroom>));
        }

        [TestMethod]
        public void GetRoomDataTest()
        {
            var logic = new Stardeck.Logic.GameLogic(new FakeStardeckContext());

            var response = logic.GetGameRoomData("G-202903a7d04e");

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(response.GetType(), typeof(Gameroom));

        }
    }
}
