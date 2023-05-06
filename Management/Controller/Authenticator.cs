using TaskManagementApplication.Presentation;
namespace TaskManagementApplication.Controller
{
    public class Authenticator
    {
        private readonly DatabaseHandler _databaseHandler = new();
        public string LogInUser(string loginId, string password)
        { 
                string msg = _databaseHandler.LogInUser(loginId, password);
                if (!msg.Contains("success"))
                    return msg;
                return new UserOperations().ShowMenu(msg);
        }
        public string DoLogOutUser()
        {
            return _databaseHandler.LogOutUser();
        }
    }
}

