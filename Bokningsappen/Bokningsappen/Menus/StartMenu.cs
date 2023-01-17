using Bokningsappen.Logic;
using Bokningsappen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningsappen.Menus
{
    internal class StartMenu
    {
        public static void Option()
        {
            Console.Clear();
            Console.WriteLine("Välkommen till Omsorgsbolaget. Välj alternativ nedan");
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("[L]ogga in");
            Console.WriteLine("[A]vsluta");
        }

        public static void Run()
        {
            bool runProgram = true;
            while (runProgram)
            {
                Option();
                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.L:
                        Console.Clear();
                        Helper.ActiveChoice("[L]ogga in");
                        User user = Helper.Login();
                        if (user != null)
                        {
                            Helper.ShowUserMenu(user);
                        }
                        break;
                    case ConsoleKey.A:
                        runProgram = false;
                        break;
                }
            }
        }
    }
}
