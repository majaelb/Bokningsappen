using Bokningsappen.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningsappen.Logic
{
    internal class ShowManager
    {
        //KLAR
        internal static void ShowEmployees()
        {
            using (var database = new MyDbContext())
            {
                var userList = database.Users.ToList();

                Console.WriteLine("Nuvarande anställda: ");
                Console.WriteLine("--------------------------------------------------------------------------");
                Console.WriteLine("Id" + "   \t" + "Titel" + "\t" + "Namn" + "  \t\t\t" + "Telefonnummer" + "\t\t" + "Timlön");
                Console.WriteLine();
                foreach (var post in userList.Where(u => u.Title.Equals("USK")))
                {
                    Console.WriteLine(post.Id + "   \t" + post.Title + "   \t" + post.FirstName + " " + post.LastName + "  \t\t" + post.PhoneNumber + "  \t\t" + post.SalaryPerHour + " kr");
                }
                Console.WriteLine("--------------------------------------------------------------------------");
            }
        }

        internal static void ShowUnits()
        {
            using (var database = new MyDbContext())
            {
                var unitList = database.Units.ToList();

                Console.WriteLine("Omsorgsbolagets vårdavdelningar: ");
                Console.WriteLine("---------------------------------");
                Console.WriteLine("Id" + "   \t" + "Namn");
                Console.WriteLine();
                foreach (var post in unitList)
                {
                    Console.WriteLine(post.Id + "   \t" + post.Name);
                }
                Console.WriteLine("---------------------------------");
            }
        }

        internal static void ShowShifts()
        {
            using (var database = new MyDbContext())
            {
                var shiftList = database.Shifts.ToList();

                Console.WriteLine("Omsorgsbolagets skifttider: ");
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("Id" + "   \t" + "Typ av skift" + "   \t\t" + "Arbetstid (varav rast): ");
                Console.WriteLine();
                foreach (var post in shiftList)
                {
                    Console.WriteLine(post.Id + "   \t" + post.Name + "   \t\t" + post.Time + " h (30 min)");
                }
                Console.WriteLine("---------------------------------------------------------------");
            }
        }

        internal static void ShowLoggedinUsersBookings(User user)
        {
            using (var db = new MyDbContext())
            {
                var result = from
                                b in db.Bookings
                             join
                                u in db.Users
                                on b.UserId equals u.Id
                             join
                                s in db.Shifts
                                on b.ShiftId equals s.Id
                             join un in db.Units
                                on b.UnitId equals un.Id
                             select new
                             {
                                 ShiftName = s.Name,
                                 UnitName = un.Name,
                                 UserId = u.Id,
                                 b.Year,
                                 b.Week,
                                 b.Day
                             };

                Console.WriteLine("Här är dina bokade pass: ");
                Console.WriteLine("------------------------------------------------------------------");
                Console.WriteLine("Skift" + "   \t\t" + "Avdelning" + "   \t\t" + "År/vecka" + "  \t" + "Dag ");
                Console.WriteLine();
                foreach (var u in result.Where(u => u.UserId == user.Id))
                {
                    Console.WriteLine(u.ShiftName + "    \t\t" + u.UnitName + "  \t\t" + u.Year + "/" + u.Week + "\t\t" + System.Enum.GetName(typeof(Enum.Day), u.Day));
                }
                Console.WriteLine("---------------------------------------------------------------");
            }
        }
        //KLAR
        internal static void ShowAllBookings()
        {
            using (var db = new MyDbContext())
            {
                var result = from
                                b in db.Bookings
                             join
                                u in db.Users
                                on b.UserId equals u.Id
                             join
                                s in db.Shifts
                                on b.ShiftId equals s.Id
                             join un in db.Units
                                on b.UnitId equals un.Id
                             select new
                             {
                                 UserName = u.FirstName + " " + u.LastName,
                                 ShiftName = s.Name,
                                 UnitName = un.Name,
                                 UserId = u.Id,
                                 b.Year,
                                 b.Week,
                                 b.Day,
                                 b.Id
                             };

                Console.WriteLine("Här är alla bokade pass: ");
                Console.WriteLine("--------------------------------------------------------------------------------------------------------");
                Console.WriteLine("Vikarie" + "      \t\t\t" + "Skift" + "   \t\t" + "Avdelning" + "   \t\t" + "År/vecka" + "  \t" + "Dag ");
                Console.WriteLine();
                foreach (var r in result)
                {
                    Console.WriteLine("[" + r.Id + "]" + r.UserName + "      \t\t" + r.ShiftName + "    \t\t" + r.UnitName + "  \t\t" + r.Year + "/" + r.Week + "\t\t" + System.Enum.GetName(typeof(Enum.Day), r.Day));
                }
                Console.WriteLine("--------------------------------------------------------------------------------------------------------");
            }
        }
        //korta ner och gör metod av vissa delar
        internal static void ShowBookingsPerWeek()
        {
            using (var db = new MyDbContext())
            {
                string[] days = new string[7];

                string[] shifts = { "Förmiddag", "Eftermiddag", "Natt" };

                int selectedWeek = Validator.GetValidatedIntInRange("Vilken vecka vill du visa bokningar för? ", 1, 52);
                string selectedUnit = Validator.ValidateUnit();

                bool runProgram = true;
                while (runProgram)
                {
                    Console.Clear();

                    var result = from
                                     b in db.Bookings
                                 join
                                    u in db.Users
                                    on b.UserId equals u.Id
                                 join
                                    s in db.Shifts
                                    on b.ShiftId equals s.Id
                                 join un in db.Units
                                    on b.UnitId equals un.Id
                                 select new
                                 {
                                     UserName = u.FirstName + " " + u.LastName,
                                     ShiftId = s.Id,
                                     UnitName = un.Name,
                                     UserId = u.Id,
                                     b.Week,
                                     b.Day
                                 };

                    Console.WriteLine(selectedUnit.ToUpper());
                    Console.WriteLine("-----------");
                    Console.WriteLine("Vecka " + selectedWeek);
                    Console.WriteLine();
                    for (int i = 0; i < shifts.Length; i++)
                    {
                        Console.Write("\t\t" + shifts[i] + "\t");
                    }
                    Console.WriteLine();
                    Console.WriteLine();

                    for (int day = 1; day <= days.Length; day++)
                    {
                        Console.Write((System.Enum.GetName(typeof(Enum.Day), day)));

                        for (int shift = 1; shift <= shifts.Length; shift++)
                        {
                            string printInfo = "Obokat pass";
                            foreach (var r in result)
                            {
                                if (r.Week == selectedWeek && r.Day == day && r.ShiftId == shift && r.UnitName == selectedUnit)
                                {
                                    printInfo = r.UserName;
                                }
                            }
                            if (printInfo != "Obokat pass")
                            {
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.Write("\t\t" + printInfo + "\t");
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.Write("\t\t" + printInfo + "\t");
                            }
                        }
                        Console.WriteLine();
                        Console.WriteLine();
                    }
                    CountBookingsPerWeek(selectedWeek, selectedUnit);
                    Console.WriteLine();
                    Console.WriteLine();

                    if (selectedWeek > 1)
                    {
                        Console.Write("[F]öregående vecka \t\t");
                    }
                    if (selectedWeek < 52)
                    {
                        Console.Write("[N]ästa vecka \t\t");
                    }
                    Console.Write("[B]yt avdelning \t\t");
                    Console.Write("[T]illbaka");
                    Console.WriteLine();
                    var key = Console.ReadKey(true).Key;
                    switch (key)
                    {
                        case ConsoleKey.F:
                            if (selectedWeek > 1)
                            {
                                selectedWeek -= 1;
                            }
                            break;
                        case ConsoleKey.N:
                            if (selectedWeek < 52)
                            {
                                selectedWeek += 1;
                            }
                            break;
                        case ConsoleKey.B:
                            selectedUnit = Validator.ValidateUnit();
                            break;
                        case ConsoleKey.T:
                            runProgram = false;
                            break;
                    }
                }
            }
        }
        //Fixa till procent?
        internal static void CountBookingsPerWeek(int selectedWeek, string selectedUnit)
        {
            using (var db = new MyDbContext())
            {
                var result = from
                                     b in db.Bookings
                             join un in db.Units
                                 on b.UnitId equals un.Id
                             select new
                             {
                                 UnitName = un.Name,
                                 b.Week,
                             };
                var count = result.Where(x => x.Week == selectedWeek && x.UnitName.Equals(selectedUnit))
                    .Count();
                if (count <= 4)
                {
                    Console.WriteLine("Denna vecka är endast " + count + " av 21 pass bokade. Behöver du boka fler vikarier?");
                }
                else
                {
                    Console.WriteLine("Denna vecka är " + count + " av 21 pass bokade.");
                }
            }
        }

        internal static void ShowLoggedInUsersSalary(User user)
        {
            using (var db = new MyDbContext())
            {
                var result = from
                                     b in db.Bookings
                             where user.Id == b.UserId
                             join
                                u in db.Users
                                on b.UserId equals u.Id
                             join
                                s in db.Shifts
                                on b.ShiftId equals s.Id
                             join un in db.Units
                                 on b.UnitId equals un.Id
                             select new
                             {
                                 ShiftTime = s.Time,
                                 ShiftId = s.Id,
                                 ShiftName = s.Name,
                                 UserId = u.Id,
                                 UnitName = un.Name,
                                 Salary = u.SalaryPerHour,
                                 b.Week,
                                 b.Day
                             };

                double? totalSalary = 0;
                Console.WriteLine("Här din lön för dina gamla och kommande bokningar: ");
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("Avdelning" + "  \t\t" + "Skift" + "   \t\t" + "Dag" + "   \t\t\t" + "Vecka" + "  \t\t" + "Lön per pass (rast borträknat)");
                Console.WriteLine();
                foreach (var r in result)
                {
                    var salary = ((r.Salary * r.ShiftTime) - (r.Salary / 2));
                    Console.WriteLine(r.UnitName + "  \t\t" + r.ShiftName + "  \t\t" + System.Enum.GetName(typeof(Enum.Day), r.Day) + "  \t\t" + r.Week + "  \t\t" + salary + " kr");
                    totalSalary += salary;
                }
                Console.WriteLine();
                Console.WriteLine("Total lön för dina bokningar: " + totalSalary + " kr");

            }
        }

        internal static void ShowSalaryForEmployees()
        {
            using (var db = new MyDbContext())
            {
                var result = from
                                     b in db.Bookings
                             join
                                u in db.Users
                                on b.UserId equals u.Id
                             join
                                s in db.Shifts
                                on b.ShiftId equals s.Id
                             join un in db.Units
                                 on b.UnitId equals un.Id
                             select new
                             {
                                 ShiftTime = s.Time,
                                 ShiftId = s.Id,
                                 ShiftName = s.Name,
                                 UserId = u.Id,
                                 UserName = u.FirstName + " " + u.LastName,
                                 UnitName = un.Name,
                                 Salary = u.SalaryPerHour,
                                 b.Week,
                                 b.Day
                             };

                var names = from r in result.ToList()
                            group r by r.UserName;


                foreach (var group in names)
                {
                    double? totalSalary = 0;
                    Console.WriteLine(group.Key);

                    foreach (var person in group.OrderBy(p => p.Week))
                    {
                        var salary = ((person.Salary * person.ShiftTime) - (person.Salary / 2));
                        Console.Write(person.ShiftName + "   \t" + System.Enum.GetName(typeof(Enum.Day), person.Day) + "\tv." + person.Week + "\t" + salary + " kr");
                        totalSalary += salary;
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                    Console.WriteLine("Total lön: " + totalSalary + " kr");
                    Console.WriteLine("--------------------------------------------------");
                    Console.WriteLine();
                }
            }
        }

        internal static void ShowMostBookedStaff()
        {
            using (var db = new MyDbContext())
            {
                var bookedUsers = db.Users.Include(x => x.Bookings).ToList();

                var result = bookedUsers.OrderByDescending(x => x.Bookings.Count).Take(1);

                foreach (var r in result)
                {
                    Console.WriteLine("Den vikarie som blir bokad oftast är " + r.FirstName + " " + r.LastName + ". Bokningar: " + r.Bookings.Count + " st");
                }
            }
        }

        internal static void ShowMostBookedUnit()
        {
            using (var db = new MyDbContext())
            {
                var bookedUnits = db.Units.Include(x => x.Bookings).ToList();

                var result = bookedUnits.OrderByDescending(x => x.Bookings.Count).Take(1);

                foreach (var r in result)
                {
                    Console.WriteLine("Den avdelning som oftast behöver vikarier är " + r.Name + ". Bokningar: " + r.Bookings.Count + " st");
                }
            }
        }
    }
}
