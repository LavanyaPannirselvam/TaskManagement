using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApplication.Controller;
using TaskManagementApplication.Controller.Interface;
using TaskManagementApplication.Enumerations;
using TaskManagementApplication.Utils;

namespace TaskManagementApplication.Presentation
{
    public class CollectUserInput
    {
        private readonly Authenticator _authenticator = new();
        private readonly UserManagement _userManagement = new();
        private readonly AdminFunctions _adminFunctions = new();

        string? name;
        string? email;
        string? password;
        int userId;
        Role role;

        public string CollectSignUpInput()
        {
            GetAndSetName();
            GetAndSetEmail();
            GetAndSetPassword();
            GetAndSetRole();
            return new SigningProcess().SignUp(name!, email!, role, password!);
        }

        public string CollectSignInInput()
        {
            GetAndSetUserId();
            GetAndSetPassword();
            return _authenticator.GetLogInData(userId, password!);
        }
        public string CallLogOutApprovedUsers()
        {
            return _authenticator.DoLogOutApprovedUser();
        }
        public string CallLogOutAdmin()
        {
            return _authenticator.DoLogoutAdmin();
        }
        public string CallLogOutTemporaryUser()
        {
            return _authenticator.DoLogOutTemporaryUser();
        }
        public string CallSignOutApprovedUsers()
        {
            return _authenticator.DoSignOutApprovedUser();
        }
        public string CallViewAssigned(int choice)
        {
            if (choice == 1)
                return _userManagement.ViewAssignedProjects();
            else return _userManagement.ViewAssignedTasks();
        }
        public string CallViewMyProfile()
        {
            return _userManagement.ViewProfile();
        }
        public string CallViewMyNotification()
        {
            return _userManagement.ViewNotifications();
        }
        public string CallViewTemporaryUserNotification()
        {
            return _userManagement.ViewNotification();
        }
        public string CollectApproveUserInput()
        {
            GetAndSetEmail();
            return _adminFunctions.ApproveUser(email!);
        }

        public string CollectNotApproveUserLoginInput()
        {
            GetAndSetEmail();
            GetAndSetPassword();
            return _authenticator.GetLogInDataForNonApprovedUser(email!, password!);
        }
        private void GetAndSetName()
        {
            ColorCode.DefaultCode("Enter name : ");
            name = Console.ReadLine();
            if (string.IsNullOrEmpty(name) || name.Trim().Length == 0 || Validation.ContainsSpecialOrNumericCharacters(name))
            {
                ColorCode.FailureCode("Name should not be empty or should contain any special character");
                GetAndSetName();
            }
        }

        private void GetAndSetEmail()
        {
            ColorCode.DefaultCode("Enter email : ");
            email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email) || email.Trim().Length == 0 || !Validation.IsValidEmail(email))
            {
                ColorCode.FailureCode("Email should not be empty or should contain any special character other than @");
                GetAndSetEmail();
            }
        }

        private void GetAndSetPassword()
        {
            ColorCode.DefaultCode("Enter password (should contain atleast 1 special character, 1 number, 1 alphabet and should be atleast of 5 characters) : ");
            password = Console.ReadLine();
            if (string.IsNullOrEmpty(password) || password.Trim().Length < 5 || !Validation.IsValidPassword(password))
            {
                ColorCode.FailureCode("Password doesn't satisfy the said condition");
                GetAndSetPassword();
            }
        }

        private void GetAndSetRole()
        {
            ColorCode.DefaultCode("Choose your role : ");
            foreach (Role role in Enum.GetValues(typeof(Role)))
                Console.WriteLine((int)role + 1 + " . " + role.ToString());
            int choice = Validation.getIntInRange(Enum.GetValues(typeof(Role)).Length);
            Role option = (Role)(choice - 1);
            switch (option)
            {
                case Role.EMPLOYEE: role = option; break;
                case Role.MANAGER: role = option; break;
                case Role.LEAD: role = option; break;
            }

        }
        private void GetAndSetUserId()
        {
            ColorCode.DefaultCode("Enter your user id : ");
            if (!int.TryParse(Console.ReadLine(), out userId) || userId.ToString().Trim().Length == 0)
            {
                ColorCode.FailureCode("User id should be in number format and cannot be empty");
                GetAndSetUserId();
            }
        }
    }
}
