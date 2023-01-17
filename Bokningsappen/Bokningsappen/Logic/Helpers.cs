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
                    //Console.Clear();
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
                        Console.WriteLine("Fel användarnamn eller lösenord, försök igen");
                        Console.ReadKey();
                    }
                }
            }
            return null;
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
