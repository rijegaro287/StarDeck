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
        public void GetAllParametersTest()
        {
            var controller = new AccountController(new FakeStardeckContext(), new NullLogger<AccountController>());

            var response = controller.GetParameters("U-RXF7RJNBWEKD");

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<OkObjectResult>(response.Result);

        }

        [TestMethod]
        public void GetParameterTest()
        {
            var controller = new AccountController(new FakeStardeckContext(), new NullLogger<AccountController>());

            var response = controller.GetParameter("U-RXF7RJNBWEKD","");

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<NotFoundObjectResult>(response.Result);

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
        public void AddCardToAccountTest()
        {
            var controller = new AccountController(new FakeStardeckContext(), new NullLogger<AccountController>());

            var response = controller.AddCards("U-RXF7RJNBWEKD", "C-542d9cf47009");

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<OkObjectResult>(response.Result);

        }

        [TestMethod]
        public void AddCardListToAccountTest()
        {
            var controller = new AccountController(new FakeStardeckContext(), new NullLogger<AccountController>());

            var response = controller.AddCardsList("U-RXF7RJNBWEKD", new string[] { "C-542d9cf47009", "C-bc0aece48924" });

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

        [TestMethod]
        public void DeleteAccountTest()
        {
            var controller = new AccountController(new FakeStardeckContext(), new NullLogger<AccountController>());

            var response = controller.Delete("U-RXF7RJNBWEKD");

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<OkObjectResult>(response.Result);

        }


        [TestMethod]
        public void DeleteCardTest()
        {
            var controller = new AccountController(new FakeStardeckContext(), new NullLogger<AccountController>());

            var response = controller.Delete("U-RXF7RJNBWEKD", "C-4b370960de0a");

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<OkObjectResult>(response.Result);

        }


        [TestMethod]
        public void PostParameter()
        {
            var controller = new AccountController(new FakeStardeckContext(), new NullLogger<AccountController>());

            var response = controller.PostParameter("U-RXF7RJNBWEKD","Test","TRUE");

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<OkObjectResult>(response.Result);

        }


    }
}
