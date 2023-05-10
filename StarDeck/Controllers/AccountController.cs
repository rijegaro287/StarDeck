using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stardeck.Logic;
using Stardeck.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Stardeck.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly StardeckContext context;
        private AccountLogic accountLogic;
        public AccountController(StardeckContext context)
        {
            this.context = context;
            this.accountLogic = new AccountLogic(context);
        }

        // GET: api/<AccountController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (accountLogic.GetAll() == null)
            {
                return NotFound();
            }
            return Ok(accountLogic.GetAll());

        }
        [HttpGet("{accountId}/cards")]
        public async Task<IActionResult> GetCards(string accountId)
        {
            if (accountLogic.GetCards(accountId) == null)
            {
                return NotFound();
            }
            return Ok(accountLogic.GetCards(accountId));
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var temp = accountLogic.GetAccount(id);
            if (temp == null)
            {
                return NotFound();
            }
            return Ok(temp);
        }


        [HttpGet("{id}/Parameters/{parameter}")]
        public async Task<IActionResult> GetParameter(string id, string parameter)
        {
            var temp = accountLogic.GetParameter(id, parameter);
            if (temp == null)
            {
                return NotFound();
            }
            return Ok(temp);
        }
        [HttpGet("{id}/Parameters")]
        public async Task<IActionResult> GetParameters(string id)
        {
            var temp = accountLogic.GetParameters(id);
            if (temp == null)
            {
                return NotFound();
            }
            return Ok(temp);
        }




        // POST api/<AccountController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Account acc)
        {
            Account accAux = accountLogic.NewAccount(acc);
            if (accAux == null)
            {
                return BadRequest();
            }
            return Ok(accAux);
        }

        [HttpPost("addCards/{accountId}/{cardId}")]
        public async Task<IActionResult> addCards(string accountId, string cardId)
        {
            string[]? aux = accountLogic.addCardsToCollection(accountId, cardId);
            if (aux is null)
            {
                return BadRequest("Ya en coleccion " + cardId);
            }
            return Ok(aux);

            //return Ok(dic);

        }

        [HttpPost("addCards/{accountId}")]
        public async Task<IActionResult> addCardsList(string accountId, [FromBody] string[] cardId)
        {

            string[]? aux = accountLogic.addCardsListToCollection(accountId, cardId);
            if (aux is null)
            {
                return BadRequest("Ya en coleccion " + cardId);
            }
            return Ok(aux);

            //return Ok(dic);

        }

        [HttpPost("{id}/Parameters/{parameter}")]
        public async Task<IActionResult> PostParameter(string id, string parameter, [FromBody] string value)
        {
            var temp = accountLogic.PostParameter(id, parameter, value);
            if (temp == null)
            {
                return NotFound("Not found account or parameter already have a value");
            }
            return Ok(temp);
        }



        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Account nAcc)
        {
            var acc = accountLogic.UpdateAccount(id, nAcc);
            if (acc != null)
            {
                return Ok(acc);
            }
            return NotFound();
        }
        // PUT api/<AccountController>/5

        [HttpPut("{id}/favorite/{deck}")]
        public async Task<IActionResult> SelectFavorite(string id, string deck)
        {
            var selected = accountLogic.SelectFavoriteDeck(id, deck);
            if (!(selected == null ||selected!=true))
            {
                return NotFound();
            }
            return Ok(deck);
        }


        [HttpPut("{id}/Parameters/{parameter}")]
        public async Task<IActionResult> PutParameter(string id, string parameter, [FromBody] string value)
        {
            var temp = accountLogic.PutParameter(id, parameter, value);
            if (temp == null)
            {
                return NotFound("Not found account or parameter do not exist");
            }
            return Ok(temp);
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var acc = accountLogic.DeleteAccount(id);
            if (acc != null)
            {
                return Ok(acc);
            }
            return NotFound();
        }


        [HttpDelete("deleteCards")]
        public async Task<IActionResult> Delete(string accountId, string cardId)
        {
            var collection = accountLogic.DeleteCard(accountId, cardId);
            if (collection == null)
            {
                return NotFound("Coleccion no encontrada");
            }
            else
            {
                if (!collection.Collection1.Contains(cardId))
                {
                    return Ok(collection.Collection1);
                }
                return NotFound("No se pudo eliminar");
            }


        }

    }
}
