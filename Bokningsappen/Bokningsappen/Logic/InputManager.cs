﻿using Bokningsappen.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningsappen.Logic
{
    internal class InputManager
    {
        internal static void AddNewEmployee()
        {
            using (var database = new MyDbContext())
            {
                ShowManager.ShowEmployees();
                Console.Write("Ange följande uppgifter om den anställda:");
                Console.WriteLine();
                var newTitle = Validator.ValidateTitle();
                var newBirthDate = Validator.GetFixedStringLength("Födelsedatum (YYYYMMDD-XXXX): ", "YYYYMMDD-XXXX".Length);
                var newFirstName = Validator.GetValidatedString("Förnamn: ");
                var newLastName = Validator.GetValidatedString("Efternamn: ");
                var newAddress = Validator.GetValidatedString("Gatuadress och gatunummer: ");
                var newPostalCode = Validator.GetValidatedIntInRange("Postnummer: ", 10000, 99999);
                var newCity = Validator.GetValidatedString("Stad: ");
                var newCountry = Validator.GetValidatedString("Land: ");
                var newPhoneNumber = Validator.GetFixedStringLength("Telefonnummer: (070-1234567): ", "123-1234567".Length);
                var newEmail = Validator.GetValidatedString("Mailadress: ");
                var newSalary = Validator.GetValidatedDouble("Timlön (kr): ");
                var newUserName = Validator.GetFixedStringLength("Användarnamn (3 första bokstäverna i förnamnet och efternamnet): ".ToLower(), "aaabbb".Length);
                var newPassWord = Validator.GetValidatedString("Lösenord: ");

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
                    //skicka tillbaka - 1 på int och null på string för att komma ur metoden?
                    var newEmId = Validator.GetValidatedIntList(validUserIds, "Den anställdas Id - nummer: ");
                    if (newEmId == -1) return;
                    var newShId = Validator.GetValidatedIntList(validShiftIds, "Skiftets Id-nummer (Fm = 1, Em = 2, Natt = 3): ");
                    var newUnId = Validator.GetValidatedIntList(validUnitIds, "Avdelningens Id-nummer (Freja 1 = 1, Freja 2 = 2, Freja 3 = 3): ");
                    var newYear = Validator.GetValidatedIntInRange("År (YYYY): ", 2023, 2023);
                    var newWeek = Validator.GetValidatedIntInRange("Vecka: ", 1, 52);
                    var newDay = Validator.GetValidatedIntInRange("Dag (1-7): ", 1, 7);


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
                    database.SaveChanges();
                    Console.ReadKey();
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
                    Console.Write("Ange Id för det pass du vill ta bort bokningen från: ");
                    int postId = int.Parse(Console.ReadLine());
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
                        Console.WriteLine("Passet du valde finns inte, vänligen försök igen");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
            }
        }

        //public static void Login()
        //{
        //    bool failedLogin = true;
        //    while (failedLogin)
        //    {
        //        Console.Write("Ange ditt användarnamn: ");
        //        var username = Console.ReadLine();
        //        Console.Write("Ange ditt lösenord: ");
        //        var password = Console.ReadLine();

        //        Helpers.CheckLogIn(failedLogin, username, password);
        //        //Console.Clear();
        //    }
        //}
    }
}