using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApplication.DataBase;
using TaskManagementApplication.Enumerations;
using TaskManagementApplication.Model;

namespace TaskManagementApplication.Controller
{
    public class CurrentUserHandler
    {
        private static string currentUserEmail = "";
        public static string CurrentUserEmail { get { return currentUserEmail; } private set { currentUserEmail = value; }}
        private readonly Database _database = Database.GetInstance();
        public string LogInUser(string loginId, string password)
        {
            if (_database.CheckUser(loginId, password) == Result.SUCCESS)
            {
                CurrentUserEmail = loginId;
                return "Login successful";
            }
            else if (_database.CheckUser(loginId, password) == Result.PARTIAL)
                return "Password incorrect";
            else return "Invalid userid";
        }
        public User GetUser(string loginId)
        {
            return _database.GetUser(loginId);
        }
        public string LogOutUser()
        {
            if (CurrentUserEmail != "")
            {
                CurrentUserEmail = "";
                return "Logged out successfully";
            }
            else
                return "Logout failed";
        }
        
    }
}
