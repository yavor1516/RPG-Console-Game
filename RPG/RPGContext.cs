using Microsoft.EntityFrameworkCore;
using RPG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RPG
{
    public class RPGContext:DbContext
    {
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Monster> Monsters { get; set; }
        public DbSet<Game> Games { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=EXAMPLE;Database=RPG_Db;Trusted_Connection=True;"); 
            // Replace EXAMPLE with your Server name.
        }
    }
}
