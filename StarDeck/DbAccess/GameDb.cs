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

        public List<Gameroom> GetAllGamerooms()
        {
            List<Gameroom> roomList = _context.Gamerooms.ToList();
            if (roomList is null)
            {
                return null;
            }
            return roomList;
        }

        public Gameroom GetGameroom(string id)
        {
            Gameroom room = _context.Gamerooms.Find(id);
            if (room is null)
            {
                return null;
            }
            return room;
        }
    }
}
