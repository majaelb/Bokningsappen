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

        /*Alla validators tar in en instruction, instället för en console writeline innan den anropas. */
       
        internal static string? GetString(string instruction)
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
       
        
        internal static string? GetFixedStringLength(string instruction, int length)    /* Kontrollerar längden på en inmatad sträng. Förhindrar många felinmatningar men
                                                                                        * innehållet i strängen kan ändå bli fel, trots att längden är rätt.
                                                                                        * Förbättringspotential: att fixa metod för kontroll av stränginnehåll */
        {
            while (true)
            {
                string? input = GetString(instruction);
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
      
        internal static string? ValidateTitle(string instruction)
        {
            while (true)
            {
                string? title = GetString(instruction).ToUpper();
                if (title == null) return null;
                if (title == "USK" || title == "ADM")
                {
                    return title; //Returnerar enbart titeln vid godkänt titelnamn
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
       
        internal static string? ValidateUnit()
        {
            using (var db = new MyDbContext())
            {
                while (true)
                {
                    string? unitName = GetString("Vilken avdelning vill du visa? (Freja 1/Freja 2/Freja 3)"); /*Har använt unit.Name ist f. id. Kräver exakt rätt inmatning
                                                                                                               så kanske hade id varit smidigare för användaren*/
                   
                    if (unitName == null) return null; //Tar användaren ur metoden utan att göra klart
                    var unitNames = db.Units.ToList();
                    foreach (var unit in unitNames)
                    {
                        if (unit.Name == unitName)
                        {
                            return unitName; //kontrollerar och returnerar korrekt namn på avdelning
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
        
        internal static int GetIntInRange(string instruction, int lower, int upper)
        {
            while (true)
            {
                int input = GetInt(instruction);
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
       
        internal static int GetInt(string instruction)
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
 
        internal static double GetDouble(string instruction)
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

        internal static int GetIntList(IEnumerable<int> validationList, string instruction)
        {
            while (true)
            {
                int input = GetInt(instruction);
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

        internal static void WrongInput(string instruction) //Text som visas vid felinmatning
        {
            Console.WriteLine(instruction + ", försök igen eller tryck <TAB> för att gå tillbaka");
        }
    
        internal static bool ExitChoice() //Möjliggör för användaren att ta sig ur en inmatning utan att fylla i resten
        {
            var key = Console.ReadKey(true).Key;
            return key == ConsoleKey.Tab;
        }
    }
}
