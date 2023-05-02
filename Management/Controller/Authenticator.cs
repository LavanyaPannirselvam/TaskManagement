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
        public string LogInAdmin(int userId,string password)
        {
            string msg = _databaseHandler.LogInAdmin(userId, password); ;
            if (!msg.Contains("success"))
                return msg;
            return new AdminOperations().ShowMenu(msg);
        }
        public string DoLogOutUser()
        {
            return _databaseHandler.LogOutUser();
        }
        public string DoSignOutUser(string email)
        {
            return _databaseHandler.SignOutUser(email);
        }
        public string DoLogoutAdmin() 
        {
            return _databaseHandler.LogOutAdmin();        
        }
    }
}

