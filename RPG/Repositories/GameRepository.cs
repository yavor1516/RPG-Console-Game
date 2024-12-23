using RPG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Repositories
{
    public class GameRepository
    {
        private readonly RPGContext _context;

        public GameRepository(RPGContext context)
        {
            _context = context;
        }

        public void SaveGame(Game game)
        {
            _context.Games.Add(game);
            _context.SaveChanges();
        }

        public void UpdateGame(Game game)
        {
            _context.Games.Update(game);
            _context.SaveChanges();
        }
    }
}
