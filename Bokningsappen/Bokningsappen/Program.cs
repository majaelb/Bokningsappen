
using Bokningsappen.Logic;
using Bokningsappen.Menus;

namespace Bokningsappen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //ShowData.ShowMostBookedStaff();
            //User user = Helpers.Login();
            //ShowData.ShowLoggedInUsersSalary(user);
            //ShowData.ShowShifts();
            StartMenu.Run();
            //ShowManager.ShowAllInfoEmployees();
            //Menus.ShowLoggedinUsersMenu(user);
            //InsertData.Login();
            //InsertData.AddNewEmployee();
        }
    }
}