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
        private sealed Database database = Database.GetInstance();
            

        public string SignUp(string username, string password,string name,string email,Role role)
        {
            User newUser = new User(username,name,email,role);
            if (database.AddUser(newUser) == Result.FAILURE)
                return "User already exists";
        }
    }
}
