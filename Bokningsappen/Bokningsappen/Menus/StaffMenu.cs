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
    internal class StaffMenu
    {
        public static void Option(User user)
        {
            Console.WriteLine("Du är inloggad som: " + user.FirstName + " " + user.LastName + ", " + user.Title);
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Välj funktion");
            Console.WriteLine("====");
            Console.WriteLine("[1]Mina bokade pass");
            Console.WriteLine("[2]Min lön");
            Console.WriteLine("[3]Avsluta/Logga ut");
        }

        public static void Run(User user)
        {
            bool runProgram = true;
            while (runProgram)

            {
                Option(user);
                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.Clear();
                        Helper.ActiveChoice("Mina bokade pass");
                        ShowManager.ShowLoggedinUsersBookings(user);
                        GUI.PressAnyKey();
                        Console.Clear();
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Console.Clear();
                        Helper.ActiveChoice("Min lön");
                        ShowManager.ShowLoggedInUsersSalary(user);
                        GUI.PressAnyKey();
                        Console.Clear();
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        runProgram = false;
                        break;
                }
            }
        }
    }
}
