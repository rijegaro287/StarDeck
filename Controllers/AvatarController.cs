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
            Avatar avatarAux = avatarLogic.NewAvatar(avatar);
            if (avatarAux == null)
            {
                return BadRequest();
            }
            return Ok(avatarAux);


        }

        // PUT api/<AvatarController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Avatar nAvatar)
        {
            var avatar = avatarLogic.UpdateAvatar(id, nAvatar);
            if (avatar != null)
            {
                return Ok(avatar);
            }
            return NotFound();

        }

        // DELETE api/<AvatarController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var avatar = avatarLogic.DeleteAvatar(id);
            if (avatar != null)
            {
                return Ok(avatar);
            }
            return NotFound();
        }
    }
}
