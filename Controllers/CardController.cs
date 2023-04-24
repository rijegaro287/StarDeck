﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stardeck.Models;
using System.Text.Json;
using System.Xml.Linq;
using System;
using System.Text.RegularExpressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Stardeck.Controllers
{
    [Route("cards")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly StardeckContext context;


        public CardController(StardeckContext context)
        {
            this.context = context;
        }


        // GET: api/<CardController>
        [HttpGet]
        [Route("get_all")]
        public async Task<IActionResult> Get()
        {
            
            return Ok(await context.Cards.ToListAsync());
        }

        // GET api/<CardController>/5
        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            //var card = context.Cards.Where(x=> x.Id==id).Include(x=>x.Navigator);
            var card = context.Cards.Find(id);
            
            if (card == null)
            {
                return NotFound();
            }
            return Ok(card);
        }

        // POST api/<CardController>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult>Post([FromBody] CardImage card)
        {
            var cardAux = new Card()
            {
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

            while (!Regex.IsMatch(cardAux.Id, @"^C-[a-zA-Z0-9]{12}"))
            {
                cardAux.Id = string.Concat("C-", System.Guid.NewGuid().ToString().Replace("-", "").AsSpan(0, 12));
            }
            await context.Cards.AddAsync(cardAux);
            
            await context.SaveChangesAsync();
            return Ok(card);

        }

        // PUT api/<CardController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Card nCard)
        {
            var card = await context.Cards.FindAsync(id);
            if (card != null)
            {
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
            }
            return NotFound();

        }

        // DELETE api/<CardController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var card = await context.Cards.FindAsync(id);
            if (card != null)
            {
                context.Remove(card);
                context.SaveChanges();
                return Ok(card);
            }
            return NotFound();
        }
    }
}
