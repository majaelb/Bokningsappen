using Bokningsappen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bokningsappen.Menus;
using Bokningsappen.UserInterface;

namespace Bokningsappen.Logic
{
    public class Helper
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
                        GUI.PressAnyKey();
                        failedLogin = false;
                        return loggedInUser;
                    }
                    else
                    {
                        Validator.WrongInput("Fel användarnamn eller lösenord");
                        if (Validator.ExitChoice())
                        {
                            return null;
                        }
                    }
                }
            }
            return null;
        }

        internal static void ShowUserMenu(User user)
        {
            if (user.Title == "ADM")
            {
                Console.Clear();
                AdminMenu.Run(user);
            }
            else
            {
                Console.Clear();
                StaffMenu.Run(user);
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
    }
}
