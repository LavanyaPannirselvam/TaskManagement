using TaskManagementApplication.Presentation;
namespace TaskManagementApplication.Controller
{
    public class Authenticator
    {
        private readonly CurrentUserHandler _dbHandler = new();
        public string LogInUser(string loginId, string password)
        { 
            string msg = _dbHandler.LogInUser(loginId, password);
            if (!msg.Contains("success"))
            {
                return msg;
            }
            return new UserOperations().ShowMenu(msg,_dbHandler.GetUser(loginId).Role);
        }
        public string LogOutUser()
        {
            return _dbHandler.LogOutUser();
            
        }
    }
}

