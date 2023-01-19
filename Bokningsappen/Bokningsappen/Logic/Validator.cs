using Bokningsappen.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningsappen.Logic
{
    internal class Validator
    {
        //KLAR
        internal static string? GetValidatedString(string instruction)
        {
            while (true)
            {
                Console.Write(instruction);
                string? input = Console.ReadLine();
                if (input.Length > 0 || input != null)
                {
                    return input;
                }
                else
                {
                    Validator.WrongInput("Felaktig inmatning");
                    if (Validator.ExitChoice())
                    {
                        return null;
                    }
                }
            }
        }
        //KLAR
        internal static string? GetFixedStringLength(string instruction, int length)
        {
            string message;
            while (true)
            {
                string? input = GetValidatedString(instruction);
                if (input.Length == length)
                {
                    return input;
                }
                else
                {
                    Validator.WrongInput(message = "Felaktig inmatning");
                    if (Validator.ExitChoice())
                    {
                        return null;
                    }
                }
            }
            Console.WriteLine(message);
        }
        //KLAR
        internal static string? ValidateTitle()
        {
            string message;
            while (true)
            {
                Console.Write("Titel (USK/ADM): ");
                string title = Console.ReadLine();
                if (title == "USK" || title == "ADM")
                {
                    return title;
                }
                else
                {
                    Validator.WrongInput(message = "Felaktig inmatning");
                    if (Validator.ExitChoice())
                    {
                        return null;
                    }
                }
            }
            Console.WriteLine(message);
        }
        //KLAR
        internal static string? ValidateUnit()
        {
            using (var db = new MyDbContext())
            {
                while (true)
                {
                    string? unitName = GetValidatedString("Vilken avdelning vill du visa? ");
                    var unitNames = db.Units.ToList();
                    foreach (var unit in unitNames)
                    {
                        if (unit.Name == unitName)
                        {
                            return unitName;
                        }
                        else
                        {
                            WrongInput("Fel namn på avdelning");
                            if (Validator.ExitChoice())
                            {
                                return null;
                            }
                            break;
                        }
                    }               
                }              
            }
        }
        //KLAR
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
                        Validator.WrongInput("Felaktig inmatning");
                        if (Validator.ExitChoice())
                        {
                            return -1;
                        }
                    }
                }
                else
                {
                    Validator.WrongInput("Felaktig inmatning");
                    if (Validator.ExitChoice())
                    {
                        return -1;
                    }
                }
            }
        }
        //KLAR
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
                    Validator.WrongInput("Felaktig inmatning");
                    if (Validator.ExitChoice())
                    {
                        return -1;
                    }
                }
            }
        }
        //KLAR
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
                    Validator.WrongInput("Felaktig inmatning");
                    if (Validator.ExitChoice())
                    {
                        return -1;
                    }
                }
            }
        }
        //dubbla utskrifter vid vissa val
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
                    Validator.WrongInput("Felaktig inmatning");
                    if (Validator.ExitChoice())
                    {
                        return -1;
                    }                    
                }
            }          
        }
        //KLAR
        internal static void WrongInput(string instruction)
        {
            Console.WriteLine(instruction + ", försök igen eller tryck <TAB> för att gå tillbaka");
        }
        //KLAR
        internal static bool ExitChoice()
        {
            var key = Console.ReadKey(true).Key;
            return key == ConsoleKey.Tab;
        }
    }
}
