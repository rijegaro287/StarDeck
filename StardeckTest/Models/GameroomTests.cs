using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stardeck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Stardeck.Models.Tests
{
    [TestClass()]
    public class GameroomTests
    {
        Gameroom room;
        [TestInitialize]
        public void poblateData()
        {
            room = new Gameroom
            {
                Id = "",
                Player1 = "U-123456789012",
                Player2 = "U-234567890123"
            };
        }

        [TestMethod()]
        public void generateIDTest()
        {


            room.generateID();

            Assert.IsTrue(Regex.IsMatch(room.Id, @"^G-[a-zA-Z0-9]{12}"));
        }


        [TestMethod()]
        public void CheckIDTest()
        {

            room.generateID();
            var roomid= room.Id;


            room.generateID();

            Assert.IsTrue(Regex.IsMatch(room.Id, @"^G-[a-zA-Z0-9]{12}"));
            Assert.IsTrue((room.Id == room.Id));
        }
    }
}