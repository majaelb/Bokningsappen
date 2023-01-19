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
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] " + list[i]);
            }
        }
    }
}
