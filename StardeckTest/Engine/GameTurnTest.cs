using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Stardeck.Controllers;
using Stardeck.Engine;
using Stardeck.Models;
using Stardeck.Models.Tests;

namespace Stardeck.GameModels.Tests;

public class GameTurnTest
{
    [TestClass()]
    public class GameRoomTurnTests //All test in this class run in parralel at the same time, mostly ended al 35s or 40s
    {
        static StardeckContext context = new StardeckContext(new DbContextOptions<StardeckContext>());

        Gameroom? room;
        GameRoom? roomAux;

        [TestInitialize]
        public void GameRoomCreation()
        {
            var logic = new Logic.AccountLogic(new StardeckContext(), new NullLogger<GameController>());
            room = new Gameroom
            {
                Id = "",
                Player1 = "U-9T9A926O2U02",
                Player2 = "U-37WTJPRJSGHH"
            };
            Account? player1 = context.Accounts.Include(x => x.FavoriteDeck)
                .FirstOrDefault(x => x.Id == "U-9T9A926O2U02");
            Account? player2 = context.Accounts.Include(x => x.FavoriteDeck)
                .FirstOrDefault(x => x.Id == "U-37WTJPRJSGHH");
            room.Player1 = player1.Id;
            room.Player1Navigation = player1;

            room.Player2 = player2.Id;
            room.Player2Navigation = player2;

            roomAux = GameRoomBuilder.CreateInstance(room);
            GameRoomBuilder.Init(new FakeDb(), roomAux);
        }

        [TestMethod()]
        public void TurntimerFinish() //
        {
            var actualTurn = roomAux.Turn;
            var timer = roomAux.PublicawaitTurnToEnd();
            timer.Wait(); //wait for the turn to end
            Task.Delay(1000).Wait(); //wait for the turn to change
            Assert.IsTrue(actualTurn != roomAux.Turn);
        }

        [TestMethod()]
        public void FinishGame() //
        {
            var actualTurn = roomAux.Turn;
            var timer = roomAux.GetTurnTimer();
            timer.Wait(); //wait for the turn to end
            Task.Delay(1000).Wait(); //wait for the turn to change
            Assert.IsTrue(actualTurn != roomAux.Turn);
        }

        [TestMethod()]
        public void PlayCards()
        {
            var player1 = roomAux.Player1;
            player1.Energy = 10;
            var handcount = player1.Hand.Count;
            var card = player1.Hand.First(x => x.Energy <= player1.Energy);
            var actualCards = roomAux.Territories[0].player1Cards.Count;

            roomAux.PlayCard(player1.Id, card.Id, 1);
            Assert.IsTrue(player1.TmpTerritories[0].Count == 1,
                "no se jugo la carta en el territorio privado"); //played in private terrirories
            var timer = roomAux.PublicawaitTurnToEnd();
            timer.Wait(); //wait for the turn to end
            Task.Delay(1500).Wait(); //wait for the turn to change
            Assert.IsTrue(player1.TmpTerritories[0].Count == 0,
                "no se elimino la carta del territorio privado al terminar el turno"); //played in private terrirories
            Assert.IsTrue(roomAux.Territories[0].player1Cards.Count == actualCards + 1,
                "No se jugo la carta en el territorio publico al terminar el turno"); //played in public terrirories
            Assert.IsTrue(player1.Hand.Count == handcount,
                "No se robo carta al terminar el turno"); //played in public terrirories
        }

        [TestMethod()]
        public void PlayCardsWithOutEnergy()
        {
            var player2 = roomAux.Player2;
            var card = player2.Hand.First(x => x.Energy > player2.Energy);
            roomAux.PlayCard(player2.Id, card.Id, 2);
            Assert.IsTrue(player2.TmpTerritories[1].Count == 0,
                "Se jugo la carta en el territorio privado aun cuando no tenia energia"); //played in private terrirories
        }

        [TestMethod()]
        public void endTurn()
        {
            var player1 = roomAux.Player1;
            var player2 = roomAux.Player2;
            while (roomAux.Turn <= 1) //Espere al segundo turno para probar el cambio de turno
            {
                var timer = roomAux.PublicawaitTurnToEnd();
                timer.Wait(); //wait for the turn to end
                Task.Delay(1000).Wait(); //wait for the turn to change
            }

            var actualTurn = roomAux.Turn;
            roomAux.EndTurn(player1.Id);
            roomAux.EndTurn(player2.Id);
            Task.Delay(1000).Wait(); //wait for the turn to change
            Assert.IsTrue(actualTurn != roomAux.Turn);
        }

        [TestMethod()]
        public void surrender()
        {
            var player1 = roomAux.Player1;
            var player2 = roomAux.Player2;
            while (roomAux.Turn < 2) //Espere al segundo turno para probar el cambio de turno
            {
                var timer = roomAux.PublicawaitTurnToEnd();
                timer.Wait(); //wait for the turn to end
                Task.Delay(1000).Wait(); //wait for the turn to change
            }

            var actualTurn = roomAux.Turn;
            roomAux.Surrender(player1.Id);
            Task.Delay(1500).Wait(); //wait for the turn to change
            Assert.IsTrue(actualTurn != roomAux.Turn, "el turno no cambio al rendirse");
            Assert.IsTrue(roomAux.Turn == 9, "no se termino el juego al rendirse");
            Assert.IsTrue(roomAux.Winner == player2.Id, "el ganador no fue el jugador 2");
        }
    }
}