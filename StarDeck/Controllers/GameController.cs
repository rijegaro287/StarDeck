using Microsoft.AspNetCore.Mvc;
using Stardeck.Logic;
using Stardeck.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Stardeck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private static readonly StardeckContext Maincontext = new();
        private readonly StardeckContext context;
        private GameLogic gameLogic;

        public GameController(StardeckContext context)
        {
            this.context = Maincontext;
            this.gameLogic = new GameLogic(Maincontext);
        }


        // POST api/<GameController>
        [HttpPost("{playerId}")]
        public async Task<IActionResult> Post(string playerId)
        {
            var room=await GameLogic.IsWaiting(playerId);
            if (room is null)
            {
                return NotFound();
            }
            return Ok(room);
        }

        [HttpPut("{id}/{isInMatchMaking}")]
        public async Task<IActionResult> Put(string id, bool isInMatchMaking)
        {
            var act = gameLogic.PutInMatchMaking(id, isInMatchMaking);
            if (act != null)
            {
                return Ok(act);
            }

            return NotFound();
        }
    }
}