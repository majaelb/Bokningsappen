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
        public static void PrintOptions(User user)
        {
            Console.WriteLine("Du är inloggad som: " + user.FirstName + " " + user.LastName + ", " + user.Title);
            Console.WriteLine("-------------------------------------------------");
            List<string> menuOptions = new List<string> { "Lägg till ny vikarie", "Ändra info om vikarie", "Boka vikarie på pass", "Avboka vikarie från pass", "Visa alla pass", "Visa löner", "Flest bokningar", "Avsluta/Logga ut" };
            GUI.PrintList(menuOptions);
        }

        public static void Run(User user)
        {
            bool runProgram = true;
            while (runProgram)

            {
                PrintOptions(user);
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
                        Helper.ActiveChoice("Ändra info om vikarie");
                        InputManager.ChangeEmployeeInfo();
                        GUI.PressAnyKey();
                        Console.Clear();                     
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        Console.Clear();
                        Helper.ActiveChoice("Boka vikarie på pass");
                        InputManager.BookEmployeeForShift();                   
                        Console.Clear();
                        break;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        Console.Clear();
                        Helper.ActiveChoice("Avboka vikarie från pass");
                        InputManager.RemoveEmployeeFromShift();
                        GUI.PressAnyKey();
                        Console.Clear();
                        break;
                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        Console.Clear();
                        Helper.ActiveChoice("Visa pass per vecka");
                        ShowManager.ShowBookingsPerWeek();
                        Console.Clear();
                        break;
                    case ConsoleKey.D6:
                    case ConsoleKey.NumPad6:
                        Console.Clear();
                        Helper.ActiveChoice("Anställdas lön");
                        ShowManager.ShowSalaryForEmployees();
                        GUI.PressAnyKey();
                        Console.Clear();
                        break;
                    case ConsoleKey.D7:
                    case ConsoleKey.NumPad7:
                        Console.Clear();
                        Helper.ActiveChoice("Flest bokningar");
                        ShowManager.ShowMostBookedStaff();
                        Console.WriteLine();
                        ShowManager.ShowMostBookedUnit();
                        GUI.PressAnyKey();
                        Console.Clear();
                        break;
                    case ConsoleKey.D8:
                    case ConsoleKey.NumPad8:
                        runProgram = false;
                        break;
                }
            }
        }
    }
}
