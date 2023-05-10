using TaskManagementApplication.Presentation;
namespace TaskManagementApplication.Controller
{
    public class Authenticator
    {
        private readonly CurrentUserHandler _currentUserHandler = new();
        public string LogInUser(string loginId, string password)
        { 
            string msg = _currentUserHandler.LogInUser(loginId, password);
            if (!msg.Contains("success"))
            {
                return msg;
            }
            return new UserOperations().ShowMenu(msg,_currentUserHandler.GetUser(loginId).Role);
        }
        public string LogOutUser()
        {
            return _currentUserHandler.LogOutUser();
            
        }
    }
}

