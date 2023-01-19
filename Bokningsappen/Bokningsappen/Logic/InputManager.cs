using Bokningsappen.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bokningsappen.UserInterface;

namespace Bokningsappen.Logic
{
    internal class InputManager
    {
        //KLAR (kan man göra metod av return-kollen?)
        internal static void AddNewEmployee()
        {
            using (var database = new MyDbContext())
            {
                ShowManager.ShowEmployees();
                Console.Write("Ange följande uppgifter om den anställda:");
                Console.WriteLine();
                var newTitle = Validator.ValidateTitle();
                if (newTitle == null) return;
                var newBirthDate = Validator.GetFixedStringLength("Födelsedatum (YYYYMMDD-XXXX): ", "YYYYMMDD-XXXX".Length);
                if (newBirthDate == null) return;
                var newFirstName = Validator.GetValidatedString("Förnamn: ");
                if (newFirstName == null) return;
                var newLastName = Validator.GetValidatedString("Efternamn: ");
                if (newLastName == null) return;
                var newAddress = Validator.GetValidatedString("Gatuadress och gatunummer: ");
                if (newAddress == null) return;
                var newPostalCode = Validator.GetValidatedIntInRange("Postnummer: ", 10000, 99999);
                if (newPostalCode == -1) return;
                var newCity = Validator.GetValidatedString("Stad: ");
                if (newCity == null) return;
                var newCountry = Validator.GetValidatedString("Land: ");
                if (newCountry == null) return;
                var newPhoneNumber = Validator.GetFixedStringLength("Telefonnummer: (070-1234567): ", "123-1234567".Length);
                if (newPhoneNumber == null) return;
                var newEmail = Validator.GetValidatedString("Mailadress: ");
                if (newEmail == null) return;
                var newSalary = Validator.GetValidatedDouble("Timlön (kr): ");
                if (newSalary == -1) return;
                var newUserName = Validator.GetFixedStringLength("Användarnamn (3 första bokstäverna i förnamnet och efternamnet): ".ToLower(), "aaabbb".Length);
                if (newUserName == null) return;
                var newPassWord = Validator.GetValidatedString("Lösenord: ");
                if (newPassWord == null) return;

                var newUser = new User
                {
                    Title = newTitle,
                    BirthDate = newBirthDate,
                    FirstName = newFirstName,
                    LastName = newLastName,
                    Address = newAddress,
                    PostalCode = newPostalCode,
                    City = newCity,
                    Country = newCountry,
                    PhoneNumber = newPhoneNumber,
                    Email = newEmail,
                    SalaryPerHour = newSalary,
                    UserName = newUserName,
                    PassWord = newPassWord
                };

                database.Add(newUser);
                database.SaveChanges();
                Console.WriteLine(newFirstName + " " + newLastName + " är nu tillagd!");
            }
        }
        //KLAR (bortsett från getvalidatedintlist vid vissa inmatningar)
        internal static void BookEmployeeForShift()
        {
            bool success = false;
            while (!success)
            {
                using (var database = new MyDbContext())
                {
                    Console.Clear();
                    ShowManager.ShowEmployees();

                    List<int> validUserIds = new();
                    foreach (User user in database.Users.Where(u => u.Title.Equals("USK")))
                    {
                        validUserIds.Add(user.Id);
                    }
                    List<int> validShiftIds = new();
                    foreach (Shift shift in database.Shifts)
                    {
                        validShiftIds.Add(shift.Id);
                    }
                    List<int> validUnitIds = new();
                    foreach (Unit unit in database.Units)
                    {
                        validUnitIds.Add(unit.Id);
                    }

                    Console.Write("Ange följande uppgifter för att boka en anställd på passet:");
                    Console.WriteLine();
                    var newEmId = Validator.GetValidatedIntList(validUserIds, "Den anställdas Id - nummer: ");
                    if (newEmId == -1) return; //Tar användaren ut ur metoden utan att göra klart
                    var newShId = Validator.GetValidatedIntList(validShiftIds, "Skiftets Id-nummer (Fm = 1, Em = 2, Natt = 3): ");
                    if (newShId == -1) return;
                    var newUnId = Validator.GetValidatedIntList(validUnitIds, "Avdelningens Id-nummer (Freja 1 = 1, Freja 2 = 2, Freja 3 = 3): ");
                    if (newUnId == -1) return;
                    var newYear = Validator.GetValidatedIntInRange("År (YYYY): ", 2023, 2023);
                    if (newYear == -1) return;
                    var newWeek = Validator.GetValidatedIntInRange("Vecka: ", 1, 52);
                    if (newWeek == -1) return;
                    var newDay = Validator.GetValidatedIntInRange("Dag (1-7): ", 1, 7);
                    if (newDay == -1) return;

                    var newBooking = new Booking
                    {
                        UserId = newEmId,
                        ShiftId = newShId,
                        UnitId = newUnId,
                        Year = newYear,
                        Week = newWeek,
                        Day = newDay
                    };

                    var bookings = database.Bookings.Where(b => newShId == b.ShiftId && newUnId == b.UnitId && newYear == b.Year && newWeek == b.Week && newDay == b.Day);

                    var occupiedUser = database.Bookings.Where(b => newEmId == b.UserId && newShId == b.ShiftId && newYear == b.Year && newWeek == b.Week && newDay == b.Day);

                    string printInfo = "";
                    bool freeShift = !bookings.Any();
                    bool freeUser = !occupiedUser.Any();
                    if (freeShift)
                    {
                        if (freeUser)
                        {
                            printInfo = "Bokningen genomförd";
                            database.Add(newBooking);
                            database.SaveChanges();
                            success = true;
                        }
                        else
                        {
                            printInfo = "Vikarien är redan bokad på en annan avdelning detta tillfälle, välj annan vikarie";
                        }
                    }
                    else
                    {
                        printInfo = "Detta skift är redan bokat, vänligen välj ett annat datum";
                    }

                    Console.WriteLine(printInfo);
                }
            }
        }
        //KLAR
        internal static void RemoveEmployeeFromShift()
        {
            using (var db = new MyDbContext())
            {
                bool success = false;
                while (!success)
                {
                    ShowManager.ShowAllBookings();
                    int postId = Validator.GetValidatedInt("Ange Id för det pass du vill ta bort bokningen från: ");
                    if (postId == -1) return; //Tar användaren ut ur metoden utan att göra klart

                    var deletePost = (from post in db.Bookings
                                      where post.Id == postId
                                      select post).SingleOrDefault();
                    if (deletePost != null)
                    {
                        db.Bookings.Remove(deletePost);
                        db.SaveChanges();
                        Console.WriteLine("Passet är nu avbokat");
                        success = true;                       
                    }
                    else
                    {
                        Validator.WrongInput("Passet du valde finns inte");
                        if (Validator.ExitChoice()) //Tar användaren ut ur metoden utan att göra klart
                        {
                            return;
                        }
                        Console.Clear();
                    }
                }
            }
        }
    }
}
