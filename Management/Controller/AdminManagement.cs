using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TaskManagementApplication.DataBase;
using TaskManagementApplication.Enumerations;
using TaskManagementApplication.Model;

namespace TaskManagementApplication.Controller
{
    public class AdminManagement
    {
        private readonly Database _database;
        public AdminManagement(Database db)
        {
            _database = db;
        }

        public string SignUp(string name,string email,Role role,string password)
        {
            User user = new(name,email,role);
            Result result = _database.AddUser(user, password);
            if (result == Result.SUCCESS)
                return $"Account created successfully\nUser id is {user.UserId} ans password is {password}";
            else return "Account creation failed as user email id already exists";
        }
        public string SignOut(string email)
        {
            Result result = _database.DeleteUser(email);
            if (result == Result.SUCCESS)
                return "Account deleted successfully";
            else if (result == Result.PARTIAL)
                return "Account cannot be deleted right now as the user has assigned activities to be completed";
            else return "Entered email doesn't exists";
        }
        
    }
}
