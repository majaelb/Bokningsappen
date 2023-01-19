using Bokningsappen.Logic;
using Bokningsappen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bokningsappen.Menus;
using Bokningsappen.UserInterface;

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
            Console.WriteLine("[1]Lägg till ny vikarie");
            Console.WriteLine("[2]Boka vikarie på pass");
            Console.WriteLine("[3]Avboka vikarie från pass");
            Console.WriteLine("[4]Visa alla pass");
            Console.WriteLine("[5]Anställdas lön");
            Console.WriteLine("[6]Flest bokningar");
            Console.WriteLine("[7]Avsluta/Logga ut");
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
                    case ConsoleKey.D1: 
                    case ConsoleKey.NumPad1:
                        Console.Clear();
                        Helper.ActiveChoice("Lägg till ny vikarie");
                        InputManager.AddNewEmployee();
                        GUI.PressAnyKey();
                        Console.Clear();
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Console.Clear();
                        Helper.ActiveChoice("Boka vikarie på pass");
                        InputManager.BookEmployeeForShift();
                        //GUI.PressAnyKey();
                        Console.Clear();
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        Console.Clear();
                        Helper.ActiveChoice("Avboka vikarie från pass");
                        InputManager.RemoveEmployeeFromShift();
                        //GUI.PressAnyKey();
                        Console.Clear();
                        break;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        Console.Clear();
                        Helper.ActiveChoice("Visa pass per vecka");
                        ShowManager.ShowBookingsPerWeek();
                        Console.Clear();
                        break;
                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        Console.Clear();
                        Helper.ActiveChoice("Anställdas lön");
                        ShowManager.ShowSalaryForEmployees();
                        GUI.PressAnyKey();
                        Console.Clear();
                        break;
                    case ConsoleKey.D6:
                    case ConsoleKey.NumPad6:
                        Console.Clear();
                        Helper.ActiveChoice("Flest bokningar");
                        ShowManager.ShowMostBookedStaff();
                        Console.WriteLine();
                        ShowManager.ShowMostBookedUnit();
                        GUI.PressAnyKey();
                        Console.Clear();
                        break;
                    case ConsoleKey.D7:
                    case ConsoleKey.NumPad7:
                        runProgram = false;
                        break;
                }
            }
        }
    }
}
