using Microsoft.AspNetCore.Mvc;
using Stardeck.Logic;
using Stardeck.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Stardeck.Controllers
{
    [Route("cards")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private CardLogic cardLogic;

        public CardController(StardeckContext context)
        {
            this.cardLogic = new CardLogic(context);
        }


        // GET: api/<CardController>
        [HttpGet]
        [Route("get_all")]
        public async Task<IActionResult> Get()
        {
            var cards = cardLogic.GetAll();
            if (cards == null)
            {
                return BadRequest("Algo sali� mal, int�ntalo m�s tarde");
            }
            if (cards.Equals(0))
            {
                return NotFound("No se encontraron cartas");
            }
            return Ok(cards);
        }

        // GET api/<CardController>/5
        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var card = cardLogic.GetCard(id);
            if (card== null)
            {
                return NotFound("No se encontraron cartas");
            }
            return Ok(card);
        }

        // POST api/<CardController>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Post([FromBody] CardImage card)
        {
            var cardAux = cardLogic.NewCard(card);
            if (cardAux.Equals(0))
            {
                return BadRequest("Ya existe una carta con estos datos");
            }
            if (cardAux == null)
            {
                return BadRequest("Algo sali� mal, int�ntalo m�s tarde");
            }
            return Ok(cardAux);


        }

        // PUT api/<CardController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Card nCard)
        {
            var acc = cardLogic.UpdateCard(id, nCard);
            if (acc != null)
            {
                return Ok(acc);
            }
            return NotFound("No se encontr� la carta");

        }

        // DELETE api/<CardController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var card = cardLogic.DeleteCard(id);
            if (card != null)
            {
                return Ok(card);
            }
            return NotFound("No se encontr� la carta");
        }

        [HttpGet]
        [Route("get/nineCards")]
        public async Task<IActionResult> GetNineCards()
        {
            var cards = cardLogic.GetNineCards();
            if (cards== null)
            {
                return NotFound("No se encontraron cartas");
            }

            return Ok(cards);
        }
    }
}
