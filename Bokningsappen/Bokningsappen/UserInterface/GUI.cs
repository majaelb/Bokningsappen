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
            Console.WriteLine("Tryck på valfri knapp för att gå vidare");
            Console.ReadKey(true);
        }
    }
}
