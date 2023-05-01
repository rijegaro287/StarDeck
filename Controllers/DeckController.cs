using Microsoft.AspNetCore.Mvc;
using Stardeck.Logic;
using Stardeck.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Stardeck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeckController : ControllerBase
    {

        private DeckLogic deckLogic;

        public DeckController(StardeckContext context)
        {
            this.deckLogic = new DeckLogic(context);
        }


        // GET: api/<DeckController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (deckLogic.GetAll() == null)
            {
                return NotFound();
            }
            return Ok(deckLogic.GetAll());
        }

        // GET api/<DeckController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (deckLogic.GetDeck(id) == null)
            {
                return NotFound();
            }
            return Ok(deckLogic.GetDeck(id));
        }

        // POST api/<DeckController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Deck deck)
        {
            Deck deckAux = deckLogic.NewDeck(deck);
            if (deckAux == null)
            {
                return BadRequest();
            }
            return Ok(deckAux);


        }

        // PUT api/<DeckController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Deck nDeck)
        {
            Deck deckAux = deckLogic.UpdateDeck(id, nDeck);
            if (deckAux == null)
            {
                return BadRequest();
            }
            return Ok(deckAux);

        }

        // DELETE api/<DeckController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var deck = deckLogic.DeleteDeck(id);
            if (deck != null)
            {
                return Ok(deck);
            }
            return NotFound();
        }
    }
}
