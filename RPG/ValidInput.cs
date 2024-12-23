using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class ValidInput
    {
        public static int GetValidInput(int min, int max)
        {
            int input;
            while (!int.TryParse(Console.ReadLine(), out input) || input < min || input > max)
            {
                Console.WriteLine($"Invalid input! Please enter a number between {min} and {max}: ");
            }
            return input;
        }
    }
}
