using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningsappen.UserInterface
{
    internal class GUI
    {
        internal static void PressAnyKey()
        {
            Console.WriteLine();
            Console.WriteLine("Tryck på valfri knapp för att gå vidare");
            Console.ReadKey(true);
        }

        internal static void PrintList(List<string> list)
        {
            Console.WriteLine("Välj funktion");
            Console.WriteLine("=====");
            for (int row = 0; row < list.Count; row++)
            {
                Console.WriteLine($"[{row + 1}] " + list[row]);
            }
        }
    }
}
