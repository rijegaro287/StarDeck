using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Stardeck.Controllers;
using Stardeck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardeckTest.Controllers
{
    [TestClass]
    public class ParametersControllertest
    {
        Parameter parameter;


        [TestInitialize]
        public void poblateData()
        {
            parameter = new Parameter()
            {
                Key = "key",
                Value = "value"

            };
        }

        [TestMethod]
        public void ParameterCreationTest()
        {
            var controller = new ParametersController(new FakeStardeckContext());
            var response = controller.Post(parameter);

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<OkObjectResult>(response.Result);
        }

        [TestMethod]
        public void GetParameterTest()
        {
            var controller = new ParametersController(new FakeStardeckContext());
            var response = controller.Get("maxTurn");

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<OkObjectResult>(response.Result);
        }


        [TestMethod]
        public void GetAllParametersTest()
        {
            var controller = new ParametersController(new FakeStardeckContext());
            var response = controller.Get();

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<OkObjectResult>(response.Result);
        }


        [TestMethod]
        public void GetCardTypeTest()
        {
            var controller = new ParametersController(new FakeStardeckContext());
            var response = controller.GetCardType();

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<OkObjectResult>(response.Result);
        }

        [TestMethod]
        public void UpdateParameterTest()
        {
            var controller = new ParametersController(new FakeStardeckContext());
            var response = controller.Put("maxTurn","8");

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<OkObjectResult>(response.Result);
        }


        [TestMethod]
        public void DeleteParameterTest()
        {
            var controller = new ParametersController(new FakeStardeckContext());
            var response = controller.Delete("maxTurn");

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<OkObjectResult>(response.Result);
        }



    }
}
