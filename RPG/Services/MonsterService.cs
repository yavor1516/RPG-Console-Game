using RPG.Models;
using RPG.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Services
{
    public class MonsterService
    {
        private readonly MonsterRepository _monsterRepository;

        public MonsterService(MonsterRepository monsterRepository)
        {
            _monsterRepository = monsterRepository;
        }

        public Monster SpawnMonster(int gameId, int gridSize, char[,] grid)
        {
            var random = new Random();
            int x, y;
            do
            {
                x = random.Next(0, gridSize);
                y = random.Next(0, gridSize);
            } while (grid[x, y] != '▒');

            var monster = new Monster
            {
                Strength = random.Next(1, 4),
                Agility = random.Next(1, 4),
                Intelligence = random.Next(1, 4),
                Health = random.Next(5, 16),
                Damage = random.Next(2, 6),
                Range = 1,
                X = x,
                Y = y,
                GameId = gameId
            };

            try
            {
                _monsterRepository.AddMonster(monster);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving monster to database: {ex.Message}");
            }

            grid[x, y] = 'o'; // Monster symbol on grid
            return monster;
        }

        public void MoveMonsterTowardsHero(Monster monster, Hero hero, char[,] grid)
        {
            grid[monster.X, monster.Y] = '▒';

            if (monster.X < hero.X) monster.X++;
            else if (monster.X > hero.X) monster.X--;

            if (monster.Y < hero.Y) monster.Y++;
            else if (monster.Y > hero.Y) monster.Y--;

            grid[monster.X, monster.Y] = 'o';
        }

        public List<Monster> GetMonstersInRange(List<Monster> monsters, Hero hero)
        {
            return monsters.Where(m =>
                Math.Abs(m.X - hero.X) <= hero.Range &&
                Math.Abs(m.Y - hero.Y) <= hero.Range).ToList();
        }
    }
}
