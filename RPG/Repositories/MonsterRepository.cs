using RPG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Repositories
{
    public class MonsterRepository
    {
        private readonly RPGContext _context;

        public MonsterRepository(RPGContext context)
        {
            _context = context;
        }

        public void AddMonster(Monster monster)
        {
            _context.Monsters.Add(monster);
            _context.SaveChanges();
        }
    }
}
