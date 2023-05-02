using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApplication.DataBase;
using TaskManagementApplication.Enumerations;
using TaskManagementApplication.Model;
using TaskManagementApplication.DataBase.Interface;

namespace TaskManagementApplication.Controller
{
    public class DatabaseHandler : ILogProcess
    {
        readonly Database _database = Database.GetInstance();
        public string LogInUser(string loginId, string password)
        {
            if (_database.CheckUser(loginId, password) == Result.SUCCESS)
                return "Login successful";
            else if (_database.CheckUser(loginId, password) == Result.PARTIAL)
                return "Password incorrect";
            else return "Invalid userid";
        }      
        public string LogOutUser()
        {
            Result result = _database.LogOutUser();
            if (result == Result.SUCCESS)
                return "LoggedOut successfully";
            else return "Logout unsuccessful";
        }       
        public string SignOutUser(string email)
        {
            Result result = _database.DeleteUser(email);
            if (result == Result.SUCCESS)
                return "Account deleted successfully";
            else if (result == Result.PARTIAL)
                return "Cannot delete account now as have activities pending to do";
            else return "Account doesn't exists";
        }
        public string LogInAdmin(int loginId, string password)
        {
            if (_database.LogInAdmin(loginId, password) == Result.SUCCESS)
                return "Login successful";
            else if (_database.LogInAdmin(loginId, password) == Result.PARTIAL)
                return "Password incorrect";
            else return "Invalid userid";
        }
        public string LogOutAdmin()
        {
            if (_database.LogOutAdmin() == Result.SUCCESS)
                return "Logged out successfully";
            else return "Login yourself to proceed further";
        }
    }
}
