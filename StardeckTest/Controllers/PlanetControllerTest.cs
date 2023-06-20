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
    public class PlanetControllerTest
    {

        PlanetImage planet;


        [TestInitialize]
        public void poblateData()
        {
            planet = new PlanetImage()
            {
                Id = "",
                Name = "TESTING",
                Type=0,
                Active= true,
                Description="TESTPLANET",
                Ability="0",
                Image="0001"
            };
        }



        [TestMethod]
        public void PlanetCreationTest()
        {
            var controller = new PlanetController(new FakeStardeckContext());
            var response = controller.Post(planet);

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<OkObjectResult>(response.Result);
        }

        [TestMethod]
        public void GetPlanetTest()
        {
            var controller = new PlanetController(new FakeStardeckContext());
            var response = controller.Get("P-cf0522d3d98e");

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<OkObjectResult>(response.Result);
        }

        [TestMethod]
        public void GetAllPlanetsTest()
        {
            var controller = new PlanetController(new FakeStardeckContext());
            var response = controller.Get();

            //Assert 
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<OkObjectResult>(response.Result);
        }


        [TestMethod]
        public void DeletePlanetTest()
        {
            var controller = new PlanetController(new FakeStardeckContext());
            var response = controller.Delete("P-cf0522d3d98e");

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<OkObjectResult>(response.Result);
        }
    }
}
