using Bokningsappen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningsappen.Logic
{
    internal class Validator
    {
        internal static string GetValidatedString(string instruction)
        {
            while (true)
            {
                Console.Write(instruction);
                string? input = Console.ReadLine();
                if (input.Length > 0)
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("Felaktig inmatning, försök igen!");
                }
            }
        }

        internal static string GetFixedStringLength(string instruction, int length)
        {
            while (true)
            {
                string? input = GetValidatedString(instruction);
                string message;
                if (input.Length == length)
                {
                    return input;
                }
                else
                {
                    message = "Felaktig inmatning, försök igen! ";
                }

                Console.WriteLine(message);
            }
        }
        //internal static string GetValidatedStringLength(string length, string instruction)
        //{
        //    while (true)
        //    {
        //        string? input = GetValidatedString(instruction);
        //        string message;
        //        if (input.Length == length.Length)
        //        {
        //            return input;
        //        }
        //        else
        //        {
        //            message = "Felaktig inmatning, försök igen! ";
        //        }

        //        Console.WriteLine(message);
        //    }
        //}

        internal static string ValidateTitle()
        {
            while (true)
            {
                string title = GetValidatedString("Titel(USK/ADM) : ");
                string message;
                if (title == "USK" || title == "ADM")
                {
                    return title;
                }
                else
                {
                    message = "Ogiltig titel, försök igen! ";
                }

                Console.WriteLine(message);
            }
        }

        internal static string ValidateUnit()
        {
            using (var db = new MyDbContext())
            {
                while (true)
                {

                    string unitName = GetValidatedString("Vilken avdelning vill du visa? ");
                    var unitNames = db.Units.ToList();
                    string message = "";
                    foreach (var unit in unitNames)
                    {
                        if (unit.Name == unitName)
                        {
                            return unitName;
                        }
                        else
                        {
                            message = "Fel namn på avdelning, försök igen! ";
                        }
                    }
                    Console.WriteLine(message);
                }
            }
        }

        

        internal static int GetValidatedIntInRange(string instruction, int lower, int upper)
        {
            while (true)
            {
                Console.Write(instruction);
                string? input = Console.ReadLine();
                if (input != null && int.TryParse(input, out int number))
                {
                    if (number <= upper && number >= lower)
                    {
                        return number;

                    }
                    else
                    {
                        Console.WriteLine("Felaktig inmatning, försök igen");
                    }
                }
                else
                {
                    Console.WriteLine("Felaktig inmatning, försök igen");
                }
            }
        }

        internal static int GetValidatedInt(string instruction)
        {
            while (true)
            {
                Console.Write(instruction);
                string? input = Console.ReadLine();
                if (input != null && int.TryParse(input, out int number))
                {
                    return number;
                }
                else
                {
                    Validator.WrongInput("Fel användarnamn eller lösenord");
                    if (Validator.ExitChoice())
                    {
                        return -1;
                    }
                }
            }
        }

        internal static double GetValidatedDouble(string instruction)
        {
            while (true)
            {
                Console.Write(instruction);
                string? input = Console.ReadLine();
                if (input != null && double.TryParse(input, out double number))
                {
                    return number;
                }
                else
                {
                    Console.WriteLine("Felaktig inmatning, försök igen");
                }
            }
        }

        internal static int GetValidatedIntList(IEnumerable<int> validationList, string instruction)
        {
            while (true)
            {
                int input = GetValidatedInt(instruction);
                if (validationList.Contains(input))
                {
                    return input;
                }
                else
                {
                    Validator.WrongInput("Felaktig inmatning, försök igen");
                    if (Validator.ExitChoice())
                    {
                        return -1;
                    }
                }
            }
        }

        internal static void WrongInput(string instruction)
        {
            Console.WriteLine(instruction + " försök igen eller tryck <TAB> för att gå tillbaka");
        }

        internal static bool ExitChoice()
        {
            Console.WriteLine(" tryck <TAB> för att gå tillbaka");
            var key = Console.ReadKey(true).Key;
            return key == ConsoleKey.Tab;
        }
    }
}
