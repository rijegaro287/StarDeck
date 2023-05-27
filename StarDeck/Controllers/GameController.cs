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
        private readonly StardeckContext context;
        private GameLogic gameLogic;

        public GameController(StardeckContext context)
        {
            this.context = context;
            this.gameLogic = new GameLogic(context);
        }


        // POST api/<GameController>
        [HttpPost("{playerId}")]
        public async Task<IActionResult> Post(string playerId)
        {
            var room=await gameLogic.IsWaiting(playerId);
            if (room is null)
            {
                return NotFound();
            }
            return Ok(room);
        }

        [HttpPut("{id}/{isInMatchMaking}")]
        public async Task<IActionResult> Put(string id, bool isInMatchMaking)
        {
            var act = await gameLogic.PutInMatchMaking(id, isInMatchMaking);
            if (act != null)
            {
                return Ok(new KeyValuePair<string, bool?>(id, act));
            }

            return NotFound();
        }


        [HttpGet("getGameRooms")]
        public async Task<IActionResult> Get()
        {
            var rooms = gameLogic.GetAllGamerooms();
            if (rooms != null)
            {
                return Ok(rooms);
            }

            return NotFound();
        }

        [HttpGet("getGameRoom/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var room = gameLogic.GetGameroom(id);
            if (room is not null)
            {
                return Ok(room);
            }

            return NotFound();
        }
        [HttpGet("getGameRoomData/{id}")]
        public async Task<IActionResult> GetData(string id)
        {
            var room = gameLogic.GetGameRoomData(id);
            if (room != null)
            {
                return Ok(room);
            }

            return NotFound();
        }
        [HttpGet("getGameRoomData/{idRoom}/{idUser}")]
        public async Task<IActionResult> GetPlayerData(string idRoom,string idUser)
        {
            var room = gameLogic.GetGameRoomData(idRoom)?.GetPlayerData(idUser);
            if (room != null)
            {
                return Ok(room);
            }

            return NotFound();
        }
    }
}