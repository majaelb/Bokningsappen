using Bokningsappen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningsappen.Menus
{
    internal class AdminMenu
    {
        public void Options(User user)
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

        public void Start()
        {
            throw new NotImplementedException();
        }
    }
}
