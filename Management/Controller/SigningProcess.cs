using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApplication.DataBase;
using TaskManagementApplication.Enumerations;
using TaskManagementApplication.Model;

namespace TaskManagementApplication.Controller
{
    public class SigningProcess
    {
        private readonly Database _database = Database.GetInstance();
        public string SignOutUser()
        {
            throw new NotImplementedException();
        }

        public string SignUp(string name, string email, Role role, string password)
        {
            if (_database.AddTemporaryUser(new User(name, email, role, UserApprovalOptions.NOT_APPROVED), password) == Result.SUCCESS)
            {
                return "Request accepted,wait for confirmation and can check your status after logging into your account with the entered email and password as username and password and select not approved option during logging in";
            }
            else return "Request failed,try again";
        }
    }
}
