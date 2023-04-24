
using TaskManagementApplication.Enumerations;
using TaskManagementApplication.Presentation;

namespace TaskManagementApplication.Controller
{
    internal class Authenticator
    {
        DatabaseHandler _databaseHandler = new();
        public string GetSignUpData(string name, string email, Role role, string password)//TODO
        {
            string msg = _databaseHandler.SignUp(name, email, role, password);
            if (!msg.Contains("success"))
                return msg;
            else return msg + "\nLogin to continue further";
        }

        public string GetLogInData(int id, string password)
        {
            string msg = _databaseHandler.LogIn(id, password);
            if (!msg.Contains("success"))
                return msg;
            return new UserLoginMenu().ShowMenu(msg);
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

