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
        private readonly StardeckContext context;
        private CardLogic cardLogic;

        public CardController(StardeckContext context)
        {
            this.context = context;
            this.cardLogic = new CardLogic(context);
        }


        // GET: api/<CardController>
        [HttpGet]
        [Route("get_all")]
        public async Task<IActionResult> Get()
        {
            if (cardLogic.GetAll() == null)
            {
                return NotFound();
            }
            return Ok(cardLogic.GetAll());
        }

        // GET api/<CardController>/5
        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (cardLogic.GetCard(id) == null)
            {
                return NotFound();
            }
            return Ok(cardLogic.GetCard(id));
        }

        // POST api/<CardController>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Post([FromBody] CardImage card)
        {
            Card cardAux = cardLogic.NewCard(card);
            if (cardAux == null)
            {
                return BadRequest();
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
            return NotFound();

        }

        // DELETE api/<CardController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var card = cardLogic.deleteCard(id);
            if (card != null)
            {
                return Ok(card);
            }
            return NotFound();
        }


        [HttpGet]
        [Route("get/nineCards")]
        public async Task<IActionResult> getNineCards()
        {
            if (cardLogic.getNineCards() == null)
            {
                return NotFound();
            }

            return Ok(cardLogic.getNineCards());
        }
    }
}
