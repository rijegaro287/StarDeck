using Microsoft.AspNetCore.Mvc;
using Stardeck.Logic;
using Stardeck.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Stardeck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanetController : ControllerBase
    {

        private PlanetLogic planetLogic;

        public PlanetController(StardeckContext context)
        {
            this.planetLogic = new PlanetLogic(context);
        }


        // GET: api/<PlanetController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (planetLogic.GetAll() == null)
            {
                return NotFound();
            }
            return Ok(planetLogic.GetAll());
        }

        // GET api/<PlanetController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (planetLogic.GetPlanet(id) == null)
            {
                return NotFound();
            }
            return Ok(planetLogic.GetPlanet(id));
        }

        // POST api/<PlanetController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PlanetImage planet)
        {
            Planet planetAux = planetLogic.NewPlanet(planet);
            if (planetLogic == null)
            {
                return BadRequest();
            }
            return Ok(planetAux);


        }

        // PUT api/<PlanetController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Planet nPlanet)
        {
            Planet planetAux = planetLogic.UpdatePlanet(id, nPlanet);
            if (planetAux == null)
            {
                return BadRequest();
            }
            return Ok(planetAux);

        }

        // DELETE api/<PlanetController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var planet = planetLogic.DeletePlanet(id);
            if (planet != null)
            {
                return Ok(planet);
            }
            return NotFound();
        }
    }
}
