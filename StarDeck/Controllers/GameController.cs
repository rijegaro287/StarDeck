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
        private static StardeckContext Maincontext = new();
        private readonly StardeckContext context;
        private GameLogic gameLogic;
        public GameController(StardeckContext context)
        {
            this.context = GameController.Maincontext;
            this.gameLogic = new GameLogic(context);
        }



        // POST api/<GameController>
        [HttpPost("{playerId}")]
        public async Task<IActionResult> Post(string playerId)
        {
            return Ok(gameLogic.IsWaitng(playerId));

        }

        [HttpPut("{id}/{isInMatchMaking}")]
        public async Task<IActionResult> Put(string id, bool isInMatchMaking)
        {
            List<Account> act = gameLogic.SetActive(id, isInMatchMaking);
            if (act != null)
            {
                return Ok(act);
            }
            return NotFound();
        }


    }
}
