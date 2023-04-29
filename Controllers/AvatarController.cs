using Microsoft.AspNetCore.Mvc;
using Stardeck.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Stardeck.Logic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Stardeck.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AvatarController : ControllerBase
    {
        private readonly StardeckContext context;
        private AvatarLogic avatarLogic;


        public AvatarController(StardeckContext context)
        {
            this.context = context;
            this.avatarLogic = new AvatarLogic(context);
        }


        // GET: api/<CardController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (avatarLogic.GetAll() == null)
            {
                return NotFound();
            }
            return Ok(avatarLogic.GetAll());
        }

        // GET api/<AvatarController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (avatarLogic.GetAvatar(id) == null)
            {
                return NotFound();
            }
            return Ok(avatarLogic.GetAvatar(id));
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
            Avatar avatarAux = avatarLogic.newAvatar(id, nAvatar);
            if (avatarAux == null)
            {
                return BadRequest();
            }
            return Ok(avatarAux);

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
