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
            Console.WriteLine("[V]isa mina bokade pass");
            Console.WriteLine("[M]in lön");
            Console.WriteLine("[A]vsluta/Logga ut");
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
                    case ConsoleKey.V:
                        Console.Clear();
                        Helper.ActiveChoice("[V]isa mina bokade pass");
                        ShowManager.ShowLoggedinUsersBookings(user);
                        GUI.PressAnyKey();
                        Console.Clear();
                        break;
                    case ConsoleKey.M:
                        Console.Clear();
                        Helper.ActiveChoice("[M]in lön");
                        ShowManager.ShowLoggedInUsersSalary(user);
                        GUI.PressAnyKey();
                        Console.Clear();
                        break;
                    case ConsoleKey.A:
                        runProgram = false;
                        break;
                }
            }
        }
    }
}
