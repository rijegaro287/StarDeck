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
            var room = await gameLogic.IsWaiting(playerId);
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
        public async Task<IActionResult> GetPlayerData(string idRoom, string idUser)
        {
            var room = gameLogic.GetGameRoomData(idRoom)?.GetPlayerData(idUser);
            if (room != null)
            {
                return Ok(room);
            }

            return NotFound();
        }

        [HttpPost("getGameRoomData/{idRoom}/{idUser}/{idCard}/{indexTargetPlanet}")]
        public async Task<IActionResult> PlayCard(string idRoom, string idUser, string idCard, int indexTargetPlanet)
        {
            if (indexTargetPlanet<0 & indexTargetPlanet >3)
            {
                return BadRequest(KeyValuePair.Create("error", "Invalid index"));
            }
            
            var answer = await this.gameLogic.PlayCard(idRoom, idUser, idCard, indexTargetPlanet);
            var playerData = gameLogic.GetGameRoomData(idRoom)?.GetPlayerData(idUser);
            switch (answer)
            {
                case null:
                    return NotFound(KeyValuePair.Create("error", "Room Game Instance not found"));
                case 1:
                    return Ok(KeyValuePair.Create("Played", playerData));
                case 0:
                    return Ok(KeyValuePair.Create("Not Played thus lack of energy", playerData));
                case -1:
                    return Ok(KeyValuePair.Create("Invalid ID", playerData));
            }

            return NotFound(KeyValuePair.Create("", playerData));
        }

        [HttpGet("{idRoom}/{idUser}/initTurn")]
        public async Task<IActionResult> InitTurn(string idRoom, string idUser)
        {
            var turn = await gameLogic.GetGameRoomData(idRoom)?.InitTurn(idUser);
            if (turn is null)
            {
                return BadRequest(KeyValuePair.Create("error", "Player not in game"));
            }
            
            //after the turn end request the player data
            var playerData = gameLogic.GetGameRoomData(idRoom)?.GetPlayerData(idUser);
            //if the player data is null the game ended 10 min ago and need to request the game room not the player data
            if (playerData is null)
            {
                return NotFound();
            }

            return Ok(playerData);
        }

        [HttpPost("{idRoom}/{idUser}/endTurn")]
        public async Task<IActionResult> EndTurn(string idRoom, string idUser)
        {
            var task = await gameLogic.EndTurn(idRoom, idUser);
            return task switch
            {
                null => NotFound(KeyValuePair.Create("error", "Room Game Instance not found")),
                false => NotFound(KeyValuePair.Create("error", "Game finished")),
                _ => Ok(KeyValuePair.Create("Turn ended", task))
            };
        }
    }
}