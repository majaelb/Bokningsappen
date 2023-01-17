using Bokningsappen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningsappen.Logic
{
    public class Helpers
    {
        public static User? Login()
        {
            using (var database = new MyDbContext())
            {
                bool failedLogin = true;
                while (failedLogin)
                {
                    Console.Write("Ange ditt användarnamn: ");
                    var username = Console.ReadLine();
                    Console.Write("Ange ditt lösenord: ");
                    var password = Console.ReadLine();



                    var users = database.Users.ToList();

                    var loggedInUser = users
                        .SingleOrDefault(u => u.UserName.Equals(username) && u.PassWord.Equals(password));
                    if (loggedInUser != null)
                    {
                        Console.WriteLine("Välkommen " + loggedInUser.FirstName + " " + loggedInUser.LastName + "!");
                        Console.ReadKey();
                        failedLogin = false;
                        return loggedInUser;
                    }
                    else
                    {
                        WrongInput("Fel användarnamn eller lösenord");
                        if (ExitChoice())
                        {
                            return null;
                        }                     
                    }
                }
            }
            return null;
        }

        internal static void WrongInput(string instruction)
        {
            Console.WriteLine(instruction + " försök igen eller tryck <TAB> för att gå tillbaka");
        }

        internal static bool ExitChoice()
        {
            var key = Console.ReadKey(true).Key;
            return key == ConsoleKey.Tab;
        }


        internal static void ShowUserMenu(User user)
        {
            if (user.Title == "ADM")
            {
                Console.Clear();
                GUI.Menus.AdminMenu(user);
            }
            else
            {
                Console.Clear();
                GUI.Menus.StaffMenu(user);
            }
        }

        internal static void ActiveChoice(string choice)
        {
            Console.WriteLine("Aktivt val:");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(choice);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
        }

        public static void CheckLogIn(bool failedLogIn, string username, string password)
        {
            using (var database = new MyDbContext())
            {
                var users = database.Users.ToList();

                var loggedInUser = users
                    .SingleOrDefault(u => u.UserName.Equals(username) && u.PassWord.Equals(password));
                if (loggedInUser != null)
                {
                    Console.WriteLine("Välkommen " + loggedInUser.FirstName);
                    failedLogIn = false;
                }
                else
                {
                    Console.WriteLine("Fel användarnamn eller lösenord, försök igen");
                }
            }

        }
    }
}
