using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Models
{
    public class Game
    {
        public int Id { get; set; }
        public int HeroId { get; set; }
        public Hero Hero { get; set; }
        public int MonstersKilled { get; set; }
        public List<Monster> Monsters { get; set; } = new List<Monster>();
    }
}
