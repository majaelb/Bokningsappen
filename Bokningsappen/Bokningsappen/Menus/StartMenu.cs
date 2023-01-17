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
        public void Options()
        {
            Console.Clear();
            Console.WriteLine("Välkommen till Omsorgsbolaget. Välj alternativ nedan");
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("[L]ogga in");
            Console.WriteLine("[A]vsluta");
        }

        public void Run()
        {
            bool runProgram = true;
            while (runProgram)
            {
                Options();
                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.L:
                        Console.Clear();
                        Helpers.ActiveChoice("[L]ogga in");
                        User user = Helpers.Login();
                        if (user != null)
                        {
                            Helpers.ShowUserMenu(user);
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
