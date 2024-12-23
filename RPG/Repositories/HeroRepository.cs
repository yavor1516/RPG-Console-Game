using RPG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Repositories
{
    public class HeroRepository
    {
        private readonly RPGContext _context;

        public HeroRepository(RPGContext context)
        {
            _context = context;
        }

        public void AddHero(Hero hero)
        {
            _context.Heroes.Add(hero);
            _context.SaveChanges();
        }
    }
}
