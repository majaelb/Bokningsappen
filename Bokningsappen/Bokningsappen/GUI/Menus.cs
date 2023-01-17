using Bokningsappen.Logic;
using Bokningsappen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningsappen.GUI
{
    internal class Menus
    {
        public static void StartMenu()
        {

            bool runProgram = true;
            while (runProgram)

            {
                Console.Clear();
                Console.WriteLine("Välkommen till Omsorgsbolaget. Välj alternativ nedan");
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine("[L]ogga in");
                Console.WriteLine("[A]vsluta");
                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.L:
                        Console.Clear();
                        ActiveChoice("[L]ogga in");
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

        public static void ShowLoggedinUsersMenu(User user)
        {
            if (user.Title == "ADM")
            {
                Console.Clear();
                AdminMenu(user);
            }
            else
            {
                Console.Clear();
                StaffMenu(user);
            }
        }

        public static void AdminMenu(User user)
        {
            bool runProgram = true;
            while (runProgram)

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
                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.L:
                        Console.Clear();
                        ActiveChoice("[L]ägg till ny vikarie");
                        InsertData.AddNewEmployee();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case ConsoleKey.B:
                        Console.Clear();
                        ActiveChoice("[B]oka vikarie");
                        InsertData.BookEmployeeForShift();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case ConsoleKey.T:
                        Console.Clear();
                        ActiveChoice("[T]a bort vikarie från pass");
                        InsertData.RemoveEmployeeFromShift();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case ConsoleKey.V:
                        Console.Clear();
                        ActiveChoice("[V]isa pass per vecka");
                        ShowData.ShowBookingsPerWeek();
                        Console.Clear();
                        break;
                    case ConsoleKey.N:
                        Console.Clear();
                        ActiveChoice("A[N]ställdas lön");
                        ShowData.ShowSalaryForEmployees();
                        break;
                    case ConsoleKey.F:
                        Console.Clear();
                        ActiveChoice("[F]lest bokningar");
                        ShowData.ShowMostBookedStaff();
                        Console.WriteLine();
                        ShowData.ShowMostBookedUnit();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case ConsoleKey.A:
                        runProgram = false;
                        break;
                }
            }
        }

        public static void StaffMenu(User user)
        {
            bool runProgram = true;
            while (runProgram)

            {
                Console.WriteLine("Du är inloggad som: " + user.FirstName + " " + user.LastName + ", " + user.Title);
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("Välj funktion");
                Console.WriteLine("====");
                Console.WriteLine("[V]isa mina bokade pass");
                Console.WriteLine("[M]in lön");
                Console.WriteLine("[A]vsluta/Logga ut");
                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.V:
                        Console.Clear();
                        ActiveChoice("[V]isa mina bokade pass");
                        ShowData.ShowLoggedinUsersBookings(user);
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case ConsoleKey.M:
                        Console.Clear();
                        ActiveChoice("[M]in lön");
                        ShowData.ShowLoggedInUsersSalary(user);
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case ConsoleKey.A:
                        runProgram = false;
                        break;
                }
            }
        }

        private static void ActiveChoice(string choice)
        {
            Console.WriteLine("Aktivt val:");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(choice);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
        }
    }
}


