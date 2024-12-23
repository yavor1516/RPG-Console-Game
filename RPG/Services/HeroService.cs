using RPG.Models;
using RPG.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Services
{
    public class HeroService
    {
        private readonly HeroRepository _heroRepository;

        public HeroService(HeroRepository heroRepository)
        {
            _heroRepository = heroRepository;
        }

        public void CreateHero(Hero hero)
        {
            hero.Health = hero.Strength * 5;
            hero.Mana = hero.Intelligence * 3;
            hero.Damage = hero.Agility * 2;

            hero.Range = hero.Race switch
            {
                "Warrior" => 1,
                "Archer" => 2,
                "Mage" => 3,
                _ => 1 // Default range
            };

            try
            {
                _heroRepository.AddHero(hero);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving hero to database: {ex.Message}");
            }
        }
    }
}
