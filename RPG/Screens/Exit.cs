using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Screens
{
    public class Exit
    {
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("Thanks for playing!");
            Console.ReadKey();
        }
    }
}
