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
            while (true)
            {
                string? input = GetValidatedString(instruction);
                if (input == null) return null;
                if (input.Length == length)
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
        internal static string? ValidateTitle(string instruction)
        {
            while (true)
            {
                string? title = GetValidatedString(instruction).ToUpper();
                if (title == null) return null;
                if (title == "USK" || title == "ADM")
                {
                    return title;
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
        internal static string? ValidateUnit()
        {
            using (var db = new MyDbContext())
            {
                while (true)
                {
                    string? unitName = GetValidatedString("Vilken avdelning vill du visa? ");
                    if (unitName == null) return null;
                    var unitNames = db.Units.ToList();
                    foreach (var unit in unitNames)
                    {
                        if (unit.Name == unitName)
                        {
                            return unitName;
                        }
                    }

                    WrongInput("Fel namn på avdelning");
                    if (Validator.ExitChoice())
                    {
                        return null;
                    }
                }
            }
        }
        //KLAR
        internal static int GetValidatedIntInRange(string instruction, int lower, int upper)
        {
            while (true)
            {
                int input = GetValidatedInt(instruction);
                if (input == -1) return -1;

                if (input <= upper && input >= lower)
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
        //KLAR
        internal static int GetValidatedIntList(IEnumerable<int> validationList, string instruction)
        {
            while (true)
            {
                int input = GetValidatedInt(instruction);
                if (input == -1) return -1;
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
