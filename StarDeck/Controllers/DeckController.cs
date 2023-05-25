using Microsoft.AspNetCore.Mvc;
using Stardeck.DbAccess;
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
            var decks = deckLogic.GetAll();
            if ( decks== null)
            {
                return NotFound("No se encontraron decks");
            }
            return Ok(decks);
        }


        // GET api/<DeckController>/Names
        [HttpGet("Names/{userId}")]
        public async Task<Object> GetNames(string userId)
        {
            var decks = deckLogic.GetNames(userId);
            if( decks.Equals(0))
            {
                return NotFound("No se encontró usuario");
            }
            if (decks== null)
            {
                return NotFound("No se encontraron decks");
            }
            return Ok(decks);
        }


        // GET api/<DeckController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var decks = deckLogic.GetDeck(id);
            if (decks== null)
            {
                return NotFound("No se encontraron decks");
            }
            return Ok(decks);
        }

        // GET api/<DeckController>/5
        [HttpGet("User/{id}")]
        public async Task<IActionResult> GetAllDecksByUser(string UserId)
        {
            var decks = deckLogic.GetDecksByUser(UserId);
            if ( decks== null)
            {
                return NotFound("No se encontraron decks");
            }
            return Ok(decks);
        }

        // POST api/<DeckController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Deck deck)
        {
            Deck deckAux = deckLogic.NewDeck(deck);
            if (deckAux == null)
            {
                return BadRequest("Algo salió mal, inténtalo más tarde");
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
                return BadRequest("Algo salió mal, inténtalo más tarde");
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
            return NotFound("No se encontró el deck");
        }
    }
}
