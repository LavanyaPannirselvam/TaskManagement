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
    public class DatabaseHandler
    {
        private DatabaseHandler() 
        {
            CurrentUserEmail = "";
        }
        private static readonly DatabaseHandler instance = new();
        public static DatabaseHandler GetInstance()
        {
            return instance;
        }
        public string CurrentUserEmail { get; private set;}
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
        public string SignInUser(User user,string password)
        {
            if (_database.AddUser(user, password) == Result.SUCCESS)
                return $"Account created successfully\nUserid is {user.UserId} and Password is {password}";
            else return "Creation failed as email already exists,try with a new email id";
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
    }
}
