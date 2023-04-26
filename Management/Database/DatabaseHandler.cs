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
        public string LogInUser(int userid, string password)
        {
            if (_database.CheckApprovedUser(userid, password) == Result.SUCCESS)
                return "Login successful";
            else if (_database.CheckApprovedUser(userid, password) == Result.PARTIAL)
                return "Password incorrect";
            else return "Invalid userid";

        }      
        public string LogOutUser()
        {
            Result result = _database.LogOutApprovedUser();
            if (result == Result.SUCCESS)
                return "LoggedOut successfully";
            else return "Logout unsuccessful";
        }
        
        public string SignOutUser()
        {
            Result result = _database.DeleteApprovedUser();
            if (result == Result.SUCCESS)
                return "Your account deleted successfully";
            else if (result == Result.PARTIAL)
                return "Cannot delete account now as you have projects pending to do";
            else return "Login yourself to proceed further";
        }
        public string LogInAdmin(int userid, string password)
        {
            if (_database.LogInAdmin(userid, password) == Result.SUCCESS)
                return "Login successful";
            else if (_database.LogInAdmin(userid, password) == Result.PARTIAL)
                return "Password incorrect";
            else return "Invalid userid";
        }

        public string LogOutAdmin()
        {
            if (_database.LogOutAdmin() == Result.SUCCESS)
                return "Logged out successfully";
            else return "Login yourself to proceed further";
        }

        public string LogInNonApprovedUser(string email,string password)
        {
            if (_database.CheckTemporaryUser(email, password) == Result.SUCCESS)
                return "Login successful";
            else if (_database.CheckTemporaryUser(email, password) == Result.PARTIAL)
                return $"Your request have been approved, try logging in with your userid and password";
            else return "Profile not available or password incorrect";//TODO should define another case to check and seperate the return stmt                  
        }
        public string LogOutTemporaryUser() 
        {
            Result result = _database.LogOutTemporaryUser();
            if (result == Result.SUCCESS)
                return "LoggedOut successfully";
            else return "Logout unsuccessful";
        }
    }
}
