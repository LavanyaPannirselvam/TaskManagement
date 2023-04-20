using ConsoleApp1.Enumeration;
using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.DataBase
{
    public class DatabaseHandler : ISignProcess,ILogProcess
    {
        private Database _database = Database.GetInstance();
        private static DatabaseHandler? _databaseHandler=null;
        public static DatabaseHandler GetInstance() 
        {
            if(_databaseHandler==null)
                return new DatabaseHandler();
            return _databaseHandler;
        }
        public string SignUp(string name, string email, Role role, string password)
        {
            User newUser = new(name, email, role);
            if (_database.AddUser(newUser,password) == Result.FAILURE)
                return "User already exists";
            //else if (database.AddUser(newUser, password) != Result.SUCCESS) 
              //  return "Some error occured";
            else return "SignedUp successfully \nYour userId is : " + newUser.UserId;
        }

        public string LogIn(int userid, string password)
        {
            if (_database.CheckUser(userid, password) == Result.SUCCESS)
                return "Login successful";
            else if (_database.CheckUser(userid, password) == Result.PARTIAL)
                return "Password incorrect";
            else return "Invalid userId";
                
        }

        public string LogOut() 
        {
            Console.WriteLine(Database.CurrentUser);
            Result result = _database.LogOut();
            if(result == Result.SUCCESS)
                return "LoggedOut successfully";
            else return "Logout unsuccessful";
        }

        public string SignOut()
        {
            Console.WriteLine(Database.CurrentUser);
            Result result = _database.DeleteUser();
            if (result == Result.SUCCESS)
                return "Your account deleted successfully";
            else if (result == Result.PARTIAL) 
                return "Cannot delete account now as you have projects pending to do";
            else return "Login yourself to proceed further";
        }
    }
}
