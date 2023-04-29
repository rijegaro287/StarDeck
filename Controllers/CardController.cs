using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stardeck.Models;
using System.Text.Json;
using System.Xml.Linq;
using System;
<<<<<<< Updated upstream
=======
using System.Text.RegularExpressions;
using Stardeck.Logic;
>>>>>>> Stashed changes

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Stardeck.Controllers
{
    [Route("[controller]")]
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
        public async Task<IActionResult> Get()
        {
            if(cardLogic.GetAll() == null)
            {
                return NotFound();
            }
            return Ok(cardLogic.GetAll());
        }

        // GET api/<CardController>/5
        [HttpGet("{id}")]
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
        public async Task<IActionResult>Post([FromBody] CardImage card)
        {
            Card cardAux = cardLogic.newCard(card);
            if (cardAux == null)
            {
<<<<<<< Updated upstream
                Id = card.Id,
                Name = card.Name,
                Energy = card.Energy,
                Battlecost = card.Battlecost,
                Image = Convert.FromBase64String(card.Image), 
                Active = card.Active,
                Type = card.Type,
                Ability = card.Ability,
                Description= card.Description

    };
            await context.Cards.AddAsync(cardAux);
            
            await context.SaveChangesAsync();
            return Ok(card);
=======
                return BadRequest();
            }
            return Ok(cardAux);
>>>>>>> Stashed changes

        }

        // PUT api/<CardController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Card nCard)
        {
            var acc = cardLogic.updateCard(id, nCard);
            if (acc != null)
            {
<<<<<<< Updated upstream
                card.Id = nCard.Id;
                card.Name = nCard.Name;
                card.Energy = nCard.Energy;
                card.Battlecost = nCard.Battlecost;
                card.Image = nCard.Image;
                card.Active = nCard.Active;
                card.Type = nCard.Type;
                card.Ability = nCard.Ability;
                card.Description = nCard.Description;

                await context.SaveChangesAsync();
                return Ok(card);
=======
                return Ok(acc);
>>>>>>> Stashed changes
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
    }
}
