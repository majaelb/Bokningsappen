using Bokningsappen.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bokningsappen.UserInterface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Dapper;

namespace Bokningsappen.Logic
{  
    internal class InputManager
    {
        static readonly string connString = "Server=tcp:dbdemomaja.database.windows.net,1433;Initial Catalog=Bokningsappen;Persist Security Info=False;User ID=majasadmin;Password=onXPkQbvhHCh8Ap;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        internal static void AddNewEmployee()
        {
            using (var database = new MyDbContext())
            {
                ShowManager.ShowEmployees();
                Console.Write("Ange följande uppgifter om den anställda:");
                Console.WriteLine();

                //Använder valideringsmetoder som även tar in en utskriftssträng, för att slippa massor av Console.Writeline

                var newTitle = Validator.ValidateTitle("Titel (USK/ADM): ");              
                if (newTitle == null) return; //Tar användaren ur metoden utan att göra klart. String returnerar null
                var newBirthDate = Validator.GetFixedStringLength("Födelsedatum (YYYYMMDD-XXXX): ", "YYYYMMDD-XXXX".Length);
                if (newBirthDate == null) return;
                var newFirstName = Validator.GetString("Förnamn: ");
                if (newFirstName == null) return;
                var newLastName = Validator.GetString("Efternamn: ");
                if (newLastName == null) return;
                var newAddress = Validator.GetString("Gatuadress och gatunummer: ");
                if (newAddress == null) return;
                var newPostalCode = Validator.GetIntInRange("Postnummer: ", 10000, 99999);
                if (newPostalCode == -1) return; //Tar användaren ur metoden utan att göra klart. Int returnerar -1
                var newCity = Validator.GetString("Stad: ");
                if (newCity == null) return;
                var newCountry = Validator.GetString("Land: ");
                if (newCountry == null) return;
                var newPhoneNumber = Validator.GetFixedStringLength("Telefonnummer: (070-1234567): ", "123-1234567".Length);
                if (newPhoneNumber == null) return;
                var newEmail = Validator.GetString("Mailadress: ");
                if (newEmail == null) return;
                var newSalary = Validator.GetDouble("Timlön (kr): ");
                if (newSalary == -1) return;
                var newUserName = Validator.GetFixedStringLength("Användarnamn (3 första bokstäverna i förnamnet och efternamnet): ".ToLower(), "aaabbb".Length);
                if (newUserName == null) return;
                var newPassWord = Validator.GetString("Lösenord: ");
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
     
        internal static bool ChangeEmployeeInfo()
        {
            while (true)
            {
                List<int> validUserIds = new();
                using (var db = new MyDbContext())
                {
                    foreach (User user in db.Users.Where(u => u.Title.Equals("USK")))
                    {
                        validUserIds.Add(user.Id);
                    }
                }
                ShowManager.ShowEmployees();
                var id = Validator.GetIntList(validUserIds, "Ange Id på den person du vill ändra: "); //Tar in listan av UserId:s och kontrollerar
                if (id == -1) return true;
                ShowManager.ShowAllInfoEmployees(id);
                string? column = Validator.GetString("Vilken kolumn vill du ändra på? ");
                if (column == null) return true;
                string? newValue = Validator.GetString("Ange det nya värdet: ");
                if (newValue == null) return true;

                //Använder Dapper för att uppdatera värden

                int affectedRow = 0;
                string sql = $"UPDATE [Users] SET [{column}] = '{newValue}' WHERE Id = {id}";

                using (var connection = new SqlConnection(connString))
                {
                    affectedRow = connection.Execute(sql);
                }
                return affectedRow > 0;
            }
        }
       
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
                    var newEmId = Validator.GetIntList(validUserIds, "Den anställdas Id - nummer: ");
                    if (newEmId == -1) return; //Tar användaren ut ur metoden utan att göra klart
                    var newShId = Validator.GetIntList(validShiftIds, "Skiftets Id-nummer (Fm = 1, Em = 2, Natt = 3): ");
                    if (newShId == -1) return;
                    var newUnId = Validator.GetIntList(validUnitIds, "Avdelningens Id-nummer (Freja 1 = 1, Freja 2 = 2, Freja 3 = 3): ");
                    if (newUnId == -1) return;
                    var newYear = Validator.GetIntInRange("År (YYYY): ", 2023, 2023); //Tillåter bara år 2023
                    if (newYear == -1) return;
                    var newWeek = Validator.GetIntInRange("Vecka: ", 1, 52); //Tillåter bara vecka 1-52
                    if (newWeek == -1) return;
                    var newDay = Validator.GetIntInRange("Dag (1-7): ", 1, 7); //Tillåter bara dag 1-7 (Enums)
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
                    //Kontrollerar först om det finns någon bokning på de valda alternativen
                    var bookings = database.Bookings.Where(b => newShId == b.ShiftId && newUnId == b.UnitId && newYear == b.Year && newWeek == b.Week && newDay == b.Day);
                    //Kontrollerar sedan om den valda användaren redan jobbar på en annan avdelning den dagen och tiden
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
                            
                            success = true; //Bokningen släpptes igenom och sparas om alla villkor uppfylls
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
                    GUI.PressAnyKey();
                }
            }
        }
      
        internal static void RemoveEmployeeFromShift()
        {
            using (var db = new MyDbContext())
            {
                bool success = false;
                while (!success)
                {
                    ShowManager.ShowAllBookings();
                    int postId = Validator.GetInt("Ange Id för det pass du vill ta bort bokningen från: ");
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
