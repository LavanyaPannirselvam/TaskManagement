using TaskManagementApplication.Presentation;
namespace TaskManagementApplication.Controller
{
    public class Authenticator
    {
        private readonly DatabaseHandler _dbHandler = DatabaseHandler.GetInstance();
        public string LogInUser(string loginId, string password)
        { 
                string msg = _dbHandler.LogInUser(loginId, password);
            if (!msg.Contains("success"))
            {
                return msg;
            }
            return new UserOperations().ShowMenu(msg,_dbHandler.GetUser(loginId).Role);
        }
        public string DoLogOutUser()
        {
            return _dbHandler.LogOutUser();
            
        }
    }
}

