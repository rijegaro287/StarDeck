using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging.Abstractions;
using Stardeck.Controllers;
using Stardeck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StardeckTest.Controllers
{
    [TestClass()]
    public class AccountControllerTest
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
            var controller = new AccountController(new FakeStardeckContext(), new NullLogger<AccountController>());
            var response = controller.Post(account);

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<OkObjectResult>(response.Result);
        }

        [TestMethod]
        public void AccountFoundTest()
        {
            var controller = new AccountController(new FakeStardeckContext(), new NullLogger<AccountController>());

            var response = controller.Get("U-RXF7RJNBWEKD");

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<OkObjectResult>(response.Result);

        }

        [TestMethod]
        public void AllAccountFoundTest()
        {
            var controller = new AccountController(new FakeStardeckContext(), new NullLogger<AccountController>());

            var response = controller.Get();

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<OkObjectResult>(response.Result);

        }


        [TestMethod]
        public void GetcardsTest()
        {
            var controller = new AccountController(new FakeStardeckContext(), new NullLogger<AccountController>());

            var response = controller.GetCards("U-RXF7RJNBWEKD");

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<OkObjectResult>(response.Result);

        }

        [TestMethod]
        public void GetGeneralRankingTest()
        {
            var controller = new AccountController(new FakeStardeckContext(), new NullLogger<AccountController>());

            var response = controller.GetRanking(false,"");

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<OkObjectResult>(response.Result);

        }


        [TestMethod]
        public void GetIndividualRankingTest()
        {
            var controller = new AccountController(new FakeStardeckContext(), new NullLogger<AccountController>());

            var response = controller.GetRanking(true,"U-RXF7RJNBWEKD");

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<OkObjectResult>(response.Result);

        }


    }
}
