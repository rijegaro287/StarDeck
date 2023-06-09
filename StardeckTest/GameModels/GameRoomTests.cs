﻿using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stardeck.GameModels;
using Stardeck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;

namespace Stardeck.GameModels.Tests
{
    [TestClass()]
    public class GameRoomTests
    {
        static StardeckContext context = new StardeckContext(new());

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
            var roomAux = new GameRoom(room);
        }



        //Esta Unit Test esta muy compleja, es posible que sea mejor dividirla en varias
        [TestMethod()]
        public void GameRoomCreationTest()
        {
            Account player1 = context.Accounts.Include(x => x.FavoriteDeck).FirstOrDefault(x => x.Id == "U-RXF7RJNBWEKD");
            Account player2 = context.Accounts.Include(x => x.FavoriteDeck).FirstOrDefault(x => x.Id == "U-37WTJPRJSGHH");
            room.Player1 = player1.Id;
            room.Player1Navigation = player1;

            room.Player2 = player2.Id;
            room.Player2Navigation = player2;

            var roomAux = new GameRoom(room);

        }
        
        
        [TestMethod()]
        public  void GameInitTest()
        {
            var logic = new  Logic.GameLogic(new());
            var a= logic.PutInMatchMaking("U-RXF7RJNBWEKD",true).Result;
            Assert.IsTrue(a,"No se logro poner al jugador en MatchMaking");
            var room =  logic.IsWaiting("U-37WTJPRJSGHH").Result;
            Assert.IsNotNull(room, "La sala no se creo correctamente");

        }
        [TestMethod()]
        public  void GameInitMaxTimeTest()
        {
            var logic = new  Logic.GameLogic(new());
            var room =  logic.IsWaiting("U-37WTJPRJSGHH").Result;
            Assert.IsNull(room, "La sala se creo correctamente, cuando no deberia");

        }
    }

}