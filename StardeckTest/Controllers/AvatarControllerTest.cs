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
    public class AvatarControllerTest
    {

        AvatarImage avatar;


        [TestInitialize]
        public void poblateData()
        {
            avatar = new AvatarImage()
            {
                Id = 0001,
                Image="001",
                Name = "TESTING"

            };
        }

        [TestMethod]
        public void AvatarCreationTest()
        {
            var controller = new AvatarController(new FakeStardeckContext());
            var response = controller.Post(avatar);

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<OkObjectResult>(response.Result);
        }

        [TestMethod]
        public void GetAllAvatarsTest()
        {
            var controller = new AvatarController(new FakeStardeckContext());
            var response = controller.Get();

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<OkObjectResult>(response.Result);
        }



        [TestMethod]
        public void GetAvatarTest()
        {
            var controller = new AvatarController(new FakeStardeckContext());
            var response = controller.Get(1000);

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<OkObjectResult>(response.Result);
        }


        [TestMethod]
        public void DeleteAvatarTest()
        {
            var controller = new AvatarController(new FakeStardeckContext());
            var response = controller.Delete(1000);

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<OkObjectResult>(response.Result);
        }

    }
}
