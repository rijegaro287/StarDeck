using Microsoft.AspNetCore.Mvc;
using Stardeck.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Stardeck.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AvatarController : ControllerBase
    {
        private readonly StardeckContext context;


        public AvatarController(StardeckContext context)
        {
            this.context = context;
        }


        // GET: api/<CardController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await context.Avatars.ToListAsync());
        }

        // GET api/<AvatarController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            //var card = context.Cards.Where(x=> x.Id==id).Include(x=>x.Navigator);
            var avatar = context.Avatars.Find(id);

            if (avatar == null)
            {
                return NotFound();
            }
            return Ok(avatar);
        }

        // POST api/<AvatarController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AvatarImage avatar)
        {
            Avatar avatarAux=new Avatar()
            {
                Id = avatar.Id,
                Image = Convert.FromBase64String(avatar.Image),
                Name = avatar.Name

            };

            await context.Avatars.AddAsync(avatarAux);

            await context.SaveChangesAsync();
            return Ok(avatarAux);


        }

        // PUT api/<AvatarController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Avatar nAvatar)
        {
            var av = await context.Avatars.FindAsync(id);
            if(av!=null)
            {
                av.Id = nAvatar.Id;
                av.Image=nAvatar.Image;
                av.Name = nAvatar.Name;
                await context.SaveChangesAsync();
                return Ok(av);
            }
            return NotFound();

        }

        // DELETE api/<AvatarController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var avatar = await context.Avatars.FindAsync(id);
            if (avatar != null)
            {
                context.Remove(avatar);
                context.SaveChanges();
                return Ok(avatar);
            }
            return NotFound();
        }
    }
}
