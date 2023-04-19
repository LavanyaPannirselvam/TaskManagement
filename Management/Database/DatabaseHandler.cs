using ConsoleApp1.Enumeration;
using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.DataBase
{
    public class DatabaseHandler
    {
        private Database database;
        public DatabaseHandler()
        {
            database = Database.GetInstance();
        }
        public string SignUp(string password, string name, string email, Role role)
        {
            User newUser = new User(name, email, role);
            if (database.AddUser(newUser,password) == Result.FAILURE)
                return "User already exists";
            else if (database.AddUser(newUser, password) != Result.SUCCESS) return "Some error occured";
            else return "SignedUp successfully \nYour userId is : " + newUser.UserId;
        }

        public string LogIn(int userid, string password)
        {
            if (database.CheckUser(userid, password) == Result.SUCCESS)
                return "Login successful";
            else if (database.CheckUser(userid, password) == Result.PARTIAL)
                return "Password incorrect";
            else return "Invalid userId";
                
        }

        public string LogOut() 
        {
            if (database.GetCurrentUser() != null)
            {
                database.SetCurrentUser(null);//TODO
                return "LoggedOut successfully";
            }
            else return "Login yourself to proceed further";
        }

        public string SignOut()
        {
            if (database.DeleteUser() == Result.SUCCESS)
                return "Your account deleted successfully";
            else if (database.DeleteUser() == Result.PARTIAL) 
                return "Cannot delete account now as you have projects pending to do";
            else return "Login yourself to proceed further";
        }
    }
}
