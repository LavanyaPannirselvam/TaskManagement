using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApplication.Controller.Interface;
using TaskManagementApplication.DataBase;
using TaskManagementApplication.Enumerations;
using TaskManagementApplication.Model;

namespace TaskManagementApplication.Controller
{
    public class SigningProcess : ISignProcess
    {
        private readonly Database _database = Database.GetInstance();
        public string SignOutUser()
        {
            throw new NotImplementedException();
        }

        public string SignUp(string name, string email, Role role, string password)
        {
            User user = new(name, email, role);
            if (_database.AddUser(user, password) == Result.SUCCESS)
            {
                return $"Account created successfully\nUserid is {user.UserId} and Password is {password}";
            }
            else return "Creation failed as email already exists,try with a new email id";
        }
    }
}
