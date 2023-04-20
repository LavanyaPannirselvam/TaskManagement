using ConsoleApp1.DataBase;
using ConsoleApp1.Enumeration;
using ConsoleApp1.PresentationLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Controller
{
    public  class Authenticator
    {
        DatabaseHandler _databaseHandler=DatabaseHandler.GetInstance();
       
        public string GetSignUpData(string name, string email,Role role,string password)
        {
            string msg = _databaseHandler.SignUp(name, email, role, password);
            if (msg.Contains("success"))
                return msg + "\nLogin to continue further";
            else return msg; 
        }

        public string GetLogInData(int id,string password)
        {
            string msg = _databaseHandler.LogIn(id, password);
            if(msg.Contains("success")) 
                new UserMenu().ShowMenu(msg);
            return msg;
        }

        public string DoLogOut()
        {
            return _databaseHandler.LogOut();
        }

        public string DoSignOut() 
        {
            return _databaseHandler.SignOut();
        }
    }
}
