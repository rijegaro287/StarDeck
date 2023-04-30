using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Stardeck.Models;
using System.Text.RegularExpressions;
using System;
using Stardeck.Logic;

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
      this.accountLogic= new AccountLogic(context);
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
      if (accountLogic.GetAccount(id) == null)
      {
        return NotFound();
      }
      return Ok(accountLogic.GetAccount(id));
    }

    // POST api/<AccountController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Account acc)
    {
            Account accAux = accountLogic.newAccount(acc);
            if (accAux == null)
            {
                return BadRequest();
            }
            return Ok(accAux);
    }

    [HttpPost("addCards/{accountId}/{cardId}")]

    public async Task<IActionResult> addCards(string accountId, string cardId)
    {
        string[] aux= accountLogic.addCardsLogic(accountId,cardId);
      
        {
          if (aux.Length==1)
          {
            return BadRequest("Ya en coleccion " + cardId);
          }
          else
          {
            return Ok(aux);
          }
        }

        //return Ok(dic);
      
    }


    // PUT api/<AccountController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, Account nAcc)
    {
      var acc = accountLogic.updateAccount(id,nAcc);
      if (acc != null)
      {
        return Ok(acc);
      }
      return NotFound();
    }

    // DELETE api/<AccountController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
      var acc = accountLogic.deleteAccount(id);
      if (acc != null)
      {
        return Ok(acc);
      }
      return NotFound();
    }


    [HttpDelete("deleteCards")]
    public async Task<IActionResult> Delete(string accountId, string cardId)
    {
      var collection =accountLogic.deleteCard(accountId,cardId);
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
