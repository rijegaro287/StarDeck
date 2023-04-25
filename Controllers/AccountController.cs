using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Stardeck.Models;
using System.Text.RegularExpressions;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Stardeck.Controllers
{
  [Route("[controller]")]
  [ApiController]
  [AllowAnonymous]
  public class AccountController : ControllerBase
  {
    private readonly StardeckContext context;


    public AccountController(StardeckContext context)
    {
      this.context = context;
    }

    // GET: api/<AccountController>
    [HttpGet]
    public async Task<IActionResult> Get()
    {

      return Ok(await context.Accounts.ToListAsync());
    }
    [HttpGet("{accountId}/cards")]
    public async Task<IActionResult> GetCards(string accountId)
    {
      var collection = context.Collections.Find(accountId);
      if (collection?.Collection1 == null) { return NotFound(); }
      return Ok(collection.Collection1);

    }

    // GET api/<AccountController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
      //var card = context.Cards.Where(x=> x.Id==id).Include(x=>x.Navigator);
      var acc = context.Accounts.Find(id);

      if (acc == null)
      {
        return NotFound();
      }
      return Ok(acc);
    }

    // POST api/<AccountController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Account acc)
    {
      if (context.Avatars.Find(acc.Avatar) == null)
      {
        Avatar a = new()
        {
          Id = 9998,
          Image = Convert.FromBase64String("0001"),
          Name = "joli"
        };
        context.Avatars.Add(a);
        acc.Avatar = a.Id;
      }

      await context.SaveChangesAsync();
      var accAux = new Account("{'rol':'User'}")
      {
        Id = acc.Id,
        Name = acc.Name,
        Nickname = acc.Nickname,
        Email = acc.Email,
        Country = acc.Country,
        Password = acc.Password,
        Avatar = acc.Avatar,
        Config = acc.Config,


      };
      while (!Regex.IsMatch(accAux.Id, @"^U-[a-zA-Z0-9]{12}"))
      {
        accAux.Id = string.Concat("U-", System.Guid.NewGuid().ToString().Replace("-", "").AsSpan(0, 12));
      }

      await context.Accounts.AddAsync(accAux);
      await context.SaveChangesAsync();

      //Add initial cards
      var cards = context.Cards.Where(x => x.Type == 0).ToList();
      var ran = new Random();
      for (int i = 0; i < 15; i++)
      {
        var index = ran.Next(cards.Count);
        await this.addCards(accAux.Id, cards[index].Id);
        cards.RemoveAt(index);
      }

      return Ok(accAux);

    }

    [HttpPost("addCards/{accountId}/{cardId}")]

    public async Task<IActionResult> addCards(string accountId, string cardId)
    {
      var collection = context.Collections.Find(accountId);
      if (collection == null)
      {
        collection = new(new List<string>().ToArray())
        {
          IdAccount = accountId
        };
        collection.Collectionlist.Add(cardId);
        context.Collections.Add(collection);
        context.SaveChanges();
        return Ok(collection.Collection1);
      }
      else
      {
        {
          if (collection.Collectionlist.Contains(cardId))
          {
            return BadRequest("Ya en coleccion " + cardId);
          }
          else
          {
            collection.Collectionlist.Add(cardId);
            context.SaveChanges();
            return Ok("AGREGA " + collection.Collection1);
          }
        }

        //return Ok(dic);
      }
    }


    // PUT api/<AccountController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, Account nAcc)
    {
      var acc = await context.Accounts.FindAsync(id);
      if (acc != null)
      {
        acc.Id = nAcc.Id;
        acc.Name = nAcc.Name;
        acc.Email = nAcc.Email;
        acc.Country = nAcc.Country;
        acc.Password = nAcc.Password;
        acc.Active = nAcc.Active;
        acc.Avatar = nAcc.Avatar;
        acc.Points = nAcc.Points;
        acc.Coins = nAcc.Coins;
        acc.Config = nAcc.Config;
        nAcc.Serverconfig.main = acc.Serverconfig.main;
        acc.Serverconfig = nAcc.Serverconfig;

        await context.SaveChangesAsync();
        return Ok(acc);
      }
      return NotFound();

    }

    // DELETE api/<AccountController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
      var acc = await context.Accounts.FindAsync(id);
      if (acc != null)
      {
        context.Remove(acc);
        context.SaveChanges();
        return Ok(acc);
      }
      return NotFound();
    }


    [HttpDelete("deleteCards")]
    public async Task<IActionResult> Delete(string accountId, string cardId)
    {
      var collection = context.Collections.Find(accountId);
      if (collection == null)
      {
        return NotFound("Coleccion no encontrada");
      }
      else
      {
        if (collection.Collection1.Contains(cardId))
        {
          collection.Collection1 = collection.Collection1.Where(x => x != cardId).ToArray();
          context.SaveChanges();


          return Ok(collection.Collection1);
        }
        return NotFound("Coleccion no encontrada");
      }

    }
  }
}
