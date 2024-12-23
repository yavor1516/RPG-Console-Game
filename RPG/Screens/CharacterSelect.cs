using RPG.Models;
using RPG.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Screens
{
    public static class CharacterSelect
    {
        public static Hero Show(HeroService heroService)
        {
            Console.Clear();
            Console.WriteLine("Choose your character type:");
            Console.WriteLine("1) Warrior\n2) Archer\n3) Mage");
            Console.Write("Your pick: ");
            int choice = ValidInput.GetValidInput(1, 3);

            Hero hero = choice switch
            {
                1 => new Hero { Race = "Warrior", Strength = 3, Agility = 3, Intelligence = 0 },
                2 => new Hero { Race = "Archer", Strength = 2, Agility = 4, Intelligence = 0 },
                3 => new Hero { Race = "Mage", Strength = 2, Agility = 1, Intelligence = 3 },
                _ => throw new InvalidOperationException("Invalid choice!")
            };

            Console.Write("Enter your hero's name: ");
            hero.Name = Console.ReadLine() ?? "Hero";

            // Buffing stats
            Console.WriteLine("Would you like to buff up your stats before starting? (Limit: 3 points total)");
            Console.Write("Response (Y/N): ");
            if (Console.ReadLine()?.ToUpper() == "Y")
            {
                int remainingPoints = 3;
                while (remainingPoints > 0)
                {
                    Console.WriteLine($"Remaining points: {remainingPoints}");
                    Console.WriteLine($"Current stats: Strength = {hero.Strength}, Agility = {hero.Agility}, Intelligence = {hero.Intelligence}");

                    Console.Write("Add to Strength: ");
                    int strengthPoints = ValidInput.GetValidInput(0, remainingPoints);
                    hero.Strength += strengthPoints;
                    remainingPoints -= strengthPoints;

                    if (remainingPoints > 0)
                    {
                        Console.WriteLine($"Remaining points: {remainingPoints}");
                        Console.WriteLine($"Current stats: Strength = {hero.Strength}, Agility = {hero.Agility}, Intelligence = {hero.Intelligence}");
                        Console.Write("Add to Agility: ");
                        int agilityPoints = ValidInput.GetValidInput(0, remainingPoints);
                        hero.Agility += agilityPoints;
                        remainingPoints -= agilityPoints;
                    }

                    if (remainingPoints > 0)
                    {
                        Console.WriteLine($"Remaining points: {remainingPoints}");
                        Console.WriteLine($"Current stats: Strength = {hero.Strength}, Agility = {hero.Agility}, Intelligence = {hero.Intelligence}");
                        Console.Write("Add to Intelligence: ");
                        int intelligencePoints = ValidInput.GetValidInput(0, remainingPoints);
                        hero.Intelligence += intelligencePoints;
                        remainingPoints -= intelligencePoints;
                    }
                }
            }

            heroService.CreateHero(hero);
            return hero;
        }
       

    }
}
