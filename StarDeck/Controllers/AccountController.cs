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
        
        private readonly AccountLogic accountLogic;
        public AccountController(StardeckContext context)
        {
            this.accountLogic = new AccountLogic(context);
        }

        // GET: api/<AccountController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var acc = accountLogic.GetAll();
            if ( acc== null)
            {
                return NotFound("No se encontraron cuentas");
            }
            return Ok(acc);

        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var temp = accountLogic.GetAccount(id);
            if (temp == null)
            {
                return NotFound("No se encontró la cuenta");
            }
            return Ok(temp);
        }

        [HttpGet("{accountId}/cards")]
        public async Task<IActionResult> GetCards(string accountId)
        {
            var cards = accountLogic.GetCards(accountId);
            if (cards== null)
            {
                return NotFound("No se encontraron cartas");
            }
            return Ok(cards);
        }


        [HttpGet("{id}/Parameters/{parameter}")]
        public async Task<IActionResult> GetParameter(string id, string parameter)
        {
            var temp = accountLogic.GetParameter(id, parameter);
            if (temp == null)
            {
                return NotFound("No se encontraron cuentas o parámetros");
            }
            return Ok(temp);
        }
        [HttpGet("{id}/Parameters")]
        public async Task<IActionResult> GetParameters(string id)
        {
            var temp = accountLogic.GetParameters(id);
            if (temp == null)
            {
                return NotFound("No se encontraron cuentas");
            }
            return Ok(temp);
        }




        // POST api/<AccountController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Account acc)
        {
            var accAux = accountLogic.NewAccount(acc);
            if(accAux.Equals(0))
            {
                return BadRequest("Ya existe una cuenta con estos datos ");
            }
            if (accAux == null)
            {
                return BadRequest("Algo salió mal guardando la cuenta, inténtalo más tarde");
            }
            if (accAux.Equals(-1))
            {
                return BadRequest("No hay cartas para asignar");
            }
            if (accAux.Equals(-2))
            {
                return BadRequest("Algo salió mal, inténtalo más tarde");
            }

            return Ok(accAux);
        }

        [HttpPost("addCards/{accountId}/{cardId}")]
        public async Task<IActionResult> AddCards(string accountId, string cardId)
        {
            string[]? aux = accountLogic.AddCardsToCollection(accountId, cardId);
            if (aux is null)
            {
                return BadRequest("Ya en coleccion " + cardId);
            }
            return Ok(aux);

            //return Ok(dic);

        }

        [HttpPost("addCards/{accountId}")]
        public async Task<IActionResult> AddCardsList(string accountId, [FromBody] string[] cardId)
        {

            string[]? aux = accountLogic.AddCardsListToCollection(accountId, cardId);
            if (aux is null)
            {
                return BadRequest("Ya en colección " + cardId);
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
                return NotFound("No se encontró cuenta o los parámetros ya tienen valor");
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
            return NotFound("No se encontró la cuenta");
        }
        // PUT api/<AccountController>/5

        [HttpPut("{id}/favorite/{deck}")]
        public async Task<IActionResult> SelectFavorite(string id, string deck)
        {
            var selected = accountLogic.SelectFavoriteDeck(id, deck);
            if (selected is not true)
            {
                return NotFound("No se encontró el deck");
            }
            if( selected is null)
            {
                return NotFound("No se encontró la cuenta");
            }
            return Ok(deck);
        }


        [HttpPut("{id}/Parameters/{parameter}")]
        public async Task<IActionResult> PutParameter(string id, string parameter, [FromBody] string value)
        {
            var temp = accountLogic.PutParameter(id, parameter, value);
            if (temp == null)
            {
                return NotFound("No se encontró la cuneta o el parámetro no existe");
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
            return NotFound("No se encontró la cuenta");
        }


        [HttpDelete("deleteCards")]
        public async Task<IActionResult> Delete(string accountId, string cardId)
        {
            var collection = accountLogic.DeleteCard(accountId, cardId);
            if (collection == null)
            {
                return NotFound("No se encontró la colección");
            }
            else
            {
                if (!collection.Collection1.Contains(cardId))
                {
                    return Ok(collection.Collection1);
                }
                return NotFound("Algo salió mal, inténtalo más tarde");
            }


        }

        

    }
}
