using Bokningsappen.Logic;
using Bokningsappen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bokningsappen.Menus;

namespace Bokningsappen.Menus
{
    internal class AdminMenu
    {
        public static void Options(User user)
        {
            Console.WriteLine("Du är inloggad som: " + user.FirstName + " " + user.LastName + ", " + user.Title);
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Välj funktion");
            Console.WriteLine("====");
            Console.WriteLine("[L]ägg till ny vikarie");
            Console.WriteLine("[B]oka vikarie på pass");
            Console.WriteLine("[T]a bort vikarie från pass");
            Console.WriteLine("[V]isa alla pass");
            Console.WriteLine("A[N]ställdas lön");
            Console.WriteLine("[F]lest bokningar");
            Console.WriteLine("[A]vsluta/Logga ut");
        }

        public static void Run(User user)
        {
            bool runProgram = true;
            while (runProgram)

            {
                Options(user);
                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.L:
                        Console.Clear();
                        Helper.ActiveChoice("[L]ägg till ny vikarie");
                        InputManager.AddNewEmployee();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case ConsoleKey.B:
                        Console.Clear();
                        Helper.ActiveChoice("[B]oka vikarie");
                        InputManager.BookEmployeeForShift();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case ConsoleKey.T:
                        Console.Clear();
                        Helper.ActiveChoice("[T]a bort vikarie från pass");
                        InputManager.RemoveEmployeeFromShift();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case ConsoleKey.V:
                        Console.Clear();
                        Helper.ActiveChoice("[V]isa pass per vecka");
                        ShowManager.ShowBookingsPerWeek();
                        Console.Clear();
                        break;
                    case ConsoleKey.N:
                        Console.Clear();
                        Helper.ActiveChoice("A[N]ställdas lön");
                        ShowManager.ShowSalaryForEmployees();
                        break;
                    case ConsoleKey.F:
                        Console.Clear();
                        Helper.ActiveChoice("[F]lest bokningar");
                        ShowManager.ShowMostBookedStaff();
                        Console.WriteLine();
                        ShowManager.ShowMostBookedUnit();
                        Console.ReadKey();
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
