using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Models
{
    public class Monster
    {
        public int Id { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        public int Range { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
