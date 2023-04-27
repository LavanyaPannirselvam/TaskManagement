using TaskManagementApplication.Presentation;
namespace TaskManagementApplication.Controller
{
    internal class Authenticator
    {
        private readonly DatabaseHandler _databaseHandler = new();
        public string GetLogInData(int id, string password)
        {
            if (id > 1000)
            {
                string msg = _databaseHandler.LogInAdmin(id, password);
                if (!msg.Contains("success"))
                    return msg;
                return new AdminOperations().ShowMenu(msg);
            }
            else
            {
                string msg = _databaseHandler.LogInUser(id, password);
                if (!msg.Contains("success"))
                    return msg;
                return new UserOperations().ShowMenu(msg);
            }
        }
        public string GetLogInDataForNonApprovedUser(string email,string password)
        {
            string msg = _databaseHandler.LogInNonApprovedUser(email, password);
            if (msg.Contains("not available"))
                return msg;
            return new NotApprovedUserOperation().ShowMenu(msg);
        }
        public string DoLogOutTemporaryUser()
        {
            return _databaseHandler.LogOutTemporaryUser();
        }
        public string DoLogOutApprovedUser()
        {
            return _databaseHandler.LogOutUser();
        }

        public string DoSignOutApprovedUser()
        {
            return _databaseHandler.SignOutUser();
        }

        public string DoLogoutAdmin() 
        {
            return _databaseHandler.LogOutAdmin();        
        }
    }
}

