using RPG.Models;
using RPG.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Screens
{
    public class InGame
    {
        private const int GridSize = 10;
        private char[,] grid = new char[GridSize, GridSize];

        private readonly HeroService heroService;
        private readonly MonsterService monsterService;
        private readonly GameService gameService;

        private Hero hero;
        private List<Monster> monsters = new List<Monster>();
        private Game currentGame;
        private int monstersKilled = 0;

        public InGame(HeroService heroService, MonsterService monsterService, GameService gameService)
        {
            this.heroService = heroService;
            this.monsterService = monsterService;
            this.gameService = gameService;
        }

        public void Show(Hero selectedHero)
        {
            hero = selectedHero;
            currentGame = gameService.StartGame(hero);

            InitializeGrid();
            PlaceHero(1, 1);

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Health: {hero.Health} | Mana: {hero.Mana} | Damage: {hero.Damage} | Monsters Killed: {monstersKilled}");
                RenderGrid();
                Console.WriteLine("Options: Move (W/A/S/D/Q/E/Z/X) | Attack (T) ");
                Console.Write("Your choice: ");
                var input = Console.ReadKey().Key;

                if (input == ConsoleKey.T)
                {
                    AttackMonster();
                }
                else
                {
                    MovementInput(input);
                }
                HandleMonsterActions();

                if (hero.Health <= 0)
                {
                    Console.WriteLine("\nYou died! Game over.");
                    break;
                }
            }

            Console.WriteLine($"Game Over! Total Monsters Killed: {monstersKilled}");
        }

        private void InitializeGrid()
        {
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    grid[i, j] = '▒';
                }
            }
        }

        private void RenderGrid()
        {
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    Console.Write(grid[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        private void PlaceHero(int x, int y)
        {
            hero.X = x;
            hero.Y = y;
            grid[x, y] = hero.Race switch
            {
                "Warrior" => '@',
                "Archer" => '#',
                "Mage" => '*',
                _ => '@',
            };
        }

        private void MovementInput(ConsoleKey key)
        {
            int newX = hero.X, newY = hero.Y;
            switch (key)
            {
                case ConsoleKey.W: newX--; break;
                case ConsoleKey.S: newX++; break;
                case ConsoleKey.A: newY--; break;
                case ConsoleKey.D: newY++; break;
                case ConsoleKey.Q: newX--; newY--; break;
                case ConsoleKey.E: newX--; newY++; break;
                case ConsoleKey.Z: newX++; newY--; break;
                case ConsoleKey.X: newX++; newY++; break;
                default:
                    Console.WriteLine("Invalid move key!");
                    return;
            }

            if (newX >= 0 && newX < GridSize && newY >= 0 && newY < GridSize)
            {
                grid[hero.X, hero.Y] = '▒';
                PlaceHero(newX, newY);
            }
        }

        private void HandleMonsterActions()
        {
            foreach (var monster in monsters)
            {
                if (Math.Abs(monster.X - hero.X) <= 1 && Math.Abs(monster.Y - hero.Y) <= 1)
                {
                    hero.Health -= monster.Damage;
                }
                else
                {
                    monsterService.MoveMonsterTowardsHero(monster, hero, grid);
                }
            }

            var newMonster = monsterService.SpawnMonster(currentGame.Id, GridSize, grid);
            monsters.Add(newMonster);
        }
        private void AttackMonster()
        {
            var targets = monsterService.GetMonstersInRange(monsters, hero);

            if (targets.Count == 0)
            {
                Console.WriteLine("\nNo available targets in your range.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nAvailable targets:");
            for (int i = 0; i < targets.Count; i++)
            {
                Console.WriteLine($"{i + 1}) Monster at ({targets[i].X}, {targets[i].Y}) - Health: {targets[i].Health}");
            }

            Console.Write("Choose a target to attack: ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= targets.Count)
            {
                var target = targets[choice - 1];
                target.Health -= hero.Damage;

                Console.WriteLine($"You attacked the monster! Remaining health: {target.Health}");

                if (target.Health <= 0)
                {
                    Console.WriteLine("Monster killed!");
                    grid[target.X, target.Y] = '▒';
                    monsters.Remove(target);
                    monstersKilled++;
                    gameService.UpdateMonstersKilled(currentGame);
                }
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }

            Console.ReadKey();
        }
    }
}
