using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Stardeck.Controllers;
using Stardeck.Engine;
using Stardeck.Logic;
using Stardeck.Models;

namespace Stardeck.GameModels.Tests
{
    [TestClass()]
    public class GameRoomTests
    {
        static StardeckContext context = new StardeckContext(new DbContextOptions<StardeckContext>());

        Gameroom? room;

        [TestInitialize]
        public void poblateData()
        {
            room = new Gameroom
            {
                Id = "",
                Player1 = "U-RXF7RJNBWEKD",
                Player2 = "U-37WTJPRJSGHH"
            };
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GameRoomCreationErrorTest()
        {
            var roomAux = GameRoomBuilder.CreateInstance(room);
        }


        //Esta Unit Test esta muy compleja, es posible que sea mejor dividirla en varias
        [TestMethod()]
        public void GameRoomCreationTest()
        {
            var logic = new Logic.AccountLogic(new StardeckContext(), new NullLogger<GameController>());

            Account? player1 = context.Accounts.Include(x => x.FavoriteDeck)
                .FirstOrDefault(x => x.Id == "U-RXF7RJNBWEKD");
            Account? player2 = context.Accounts.Include(x => x.FavoriteDeck)
                .FirstOrDefault(x => x.Id == "U-37WTJPRJSGHH");
            room.Player1 = player1.Id;
            room.Player1Navigation = player1;

            room.Player2 = player2.Id;
            room.Player2Navigation = player2;

            var roomAux = GameRoomBuilder.CreateInstance(room);
        }


        [TestMethod()]
        public void GameInitTest()
        {
            var logic = new Logic.GameLogic(new(), new NullLogger<GameLogic>());
            var a = logic.PutInMatchMaking("U-RXF7RJNBWEKD", true).Result;
            Assert.IsTrue(a, "No se logro poner al jugador en MatchMaking");
            var room3 = logic.IsWaiting("U-37WTJPRJSGHH").Result;
            Assert.IsNotNull(room3, "La sala no se creo correctamente");
        }

        [TestMethod()]
        public void GameInitMaxTimeTest()
        {
            var logic = new Logic.FakeGameLogic(new(), new NullLogger<GameLogic>());


            var room2 = logic.IsWaiting("U-KIX8NYN0TY5T").Result;
            Assert.IsNull(room2, "La sala se creo correctamente, cuando no deberia");
        }
    }
}