using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
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
    public class AccountLogicTest
    {
        Account account;


        [TestInitialize]
        public void poblateData()
        {
            account = new Account("{'rol':'User'}")
            {
                Id = "",
                Name = "TESTING",
                Nickname = "TESTINGNICK",
                Active = true,
                Email = "testing@test.com",
                Password = "Testing12345",
                Country = "Costa Rica",
                Config = "{}"

            };
        }

        [TestMethod]
        public void AccountCreationTest()
        {
            
            var logic = new Stardeck.Logic.AccountLogic(new FakeStardeckContext(), new NullLogger<GameController>());
            var response = logic.NewAccount(account);
            //Assert
            Assert.AreEqual(response, 1);
        }

        [TestMethod]
        public void AccountCheckIdTest()
        {
            account.generateID();

            //Assert
            Assert.IsTrue(Regex.IsMatch(account.Id, @"^U-[a-zA-Z0-9]{12}"));
        }

        [TestMethod]
        public void AccountFoundTest()
        {
            var logic = new Stardeck.Logic.AccountLogic(new FakeStardeckContext(), new NullLogger<GameController>());

            var response = logic.GetAccount("U-RXF7RJNBWEKD");

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(response.GetType(), typeof(Account));

        } 

        [TestMethod]
        public void AllAccountFoundTest()
        {
            var logic = new Stardeck.Logic.AccountLogic(new FakeStardeckContext(), new NullLogger<GameController>());
            var response = logic.GetAll();
            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(response.GetType(), typeof(List<Account>));
        }

        [TestMethod]
        public void GetcardsTest()
        {
            var logic = new Stardeck.Logic.AccountLogic(new FakeStardeckContext(), new NullLogger<GameController>());
            var response = logic.GetCards("U-RXF7RJNBWEKD");
            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(response.GetType(), typeof(string[]));
        }

        [TestMethod]
        public void GetGeneralRankingTest()
        {
            var logic = new Stardeck.Logic.AccountLogic(new FakeStardeckContext(), new NullLogger<GameController>());
            var response = logic.GetRanking(false,"");
            //Assert
            Assert.IsNotNull(response);
            //Assert.AreEqual(response.GetType(), typeof(List<object>));
        }

        [TestMethod]
        public void GetIndividualRankingTest()
        {
            var logic = new Stardeck.Logic.AccountLogic(new FakeStardeckContext(), new NullLogger<GameController>());
            var response = logic.GetRanking(true, "U-RXF7RJNBWEKD");
            //Assert
            Assert.IsNotNull(response);
            //Assert.AreEqual(response.GetType(), typeof(string[]));
        }

    }
}
