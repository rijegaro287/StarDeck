using Microsoft.EntityFrameworkCore;
using Stardeck.Models;

namespace Stardeck.DbAccess
{
    public class GameDb
    {
        private readonly StardeckContext _context;

        public GameDb(StardeckContext _context)
        {
            this._context = _context;
        }

        public async Task<List<Gameroom?>?>? GetAllGamerooms()
        {
            List<Gameroom?>? roomList = await _context.Gamerooms.ToListAsync();
            return roomList.Count == 0 ? null : roomList;
        }

        public async Task<Gameroom?> GetGameroom(string id)
        {
            Gameroom? room = await _context.Gamerooms.FindAsync(id);

            return room;
        }
    }
}