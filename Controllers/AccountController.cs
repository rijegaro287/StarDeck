using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stardeck.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Stardeck.Controllers
{
    [Route("[controller]")]
    [ApiController]
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
                Avatar a = new() {
                    Id = 9998,
                    Image = Convert.FromBase64String("0001"),
                    Name = "joli"
                };
                context.Avatars.Add(a);
                acc.Avatar = a.Id;
            }

            await context.SaveChangesAsync();
            var accAux = new Account()
            {
                Id = acc.Id,
                Name = acc.Name,
                Nickname= acc.Nickname,
                Email= acc.Email,
                Country= acc.Country,
                Password= acc.Password,
                Avatar= acc.Avatar

            };
            await context.Accounts.AddAsync(accAux);

            await context.SaveChangesAsync();
            return Ok(acc);

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
                acc.Avatar= nAcc.Avatar;
                acc.Config = nAcc.Config;
                acc.Points = nAcc.Points;
                acc.Coins = nAcc.Coins;

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
    }
}
