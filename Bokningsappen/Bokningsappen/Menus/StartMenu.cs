using Bokningsappen.Logic;
using Bokningsappen.Models;
using Bokningsappen.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningsappen.Menus
{
    internal class StartMenu
    {
        public static void PrintOptions()
        {
            Console.Clear();
            Console.WriteLine("Välkommen till Omsorgsbolaget");
            Console.WriteLine("-----------------------------");
            List<string> menuOptions = new List<string> { "Logga in", "Avsluta" };
            GUI.PrintList(menuOptions);
           
        }

        public static void Run()
        {
            bool runProgram = true;
            while (runProgram)
            {
                PrintOptions();
                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.Clear();
                        Helper.ActiveChoice("Logga in");
                        User? user = Helper.Login();
                        if (user != null)
                        {
                            Helper.ShowUserMenu(user); //Om användaren från loginmetoden inte är null skickas man vidare till rätt sida
                        }                       
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        runProgram = false;
                        break;
                }
            }
        }
    }
}
