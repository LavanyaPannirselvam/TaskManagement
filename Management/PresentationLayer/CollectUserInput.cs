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
        private readonly Authenticator authenticator = new();
        private readonly UserManagement userManagement = new();

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
            return authenticator.GetSignUpData(name, email, role, password);
        }

        public string CollectSignInInput()
        {
            GetAndSetUserId();
            GetAndSetPassword();
            return authenticator.GetLogInData(userId, password);
        }
        public string CallLogOut()
        {
            return authenticator.DoLogOut();
        }

        public string CallSignOut()
        {
            return authenticator.DoSignOut();
        }
        public string CallViewMyProjects()
        {
            return userManagement.ViewAssignedProjects();
        }
        public string CallViewMyProfile()
        {
            return userManagement.ViewProfile();
        }
        private void GetAndSetName()
        {
            ColorCode.GetInputCode("Enter your name : ");
            name = Console.ReadLine();
            if (string.IsNullOrEmpty(name) || name.Trim().Length == 0 || Validation.ContainsSpecialOrNumericCharacters(name))
            {
                ColorCode.FailureCode("Name should not be empty or should contain any special character");
                GetAndSetName();
            }
        }

        private void GetAndSetEmail()
        {
            ColorCode.GetInputCode("Enter your email : ");
            email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email) || email.Trim().Length == 0 || !Validation.IsValidEmail(email))
            {
                ColorCode.FailureCode("Email should not be empty or should contain any special character other than @");
                GetAndSetEmail();
            }
        }

        private void GetAndSetPassword()
        {
            ColorCode.GetInputCode("Enter password (should contain atleast 1 uppercase letter, 1 lowercase letter, 1 special character and should be atleast of 5 characters) : ");
            password = Console.ReadLine();
            if (string.IsNullOrEmpty(password) || password.Trim().Length < 5 || !Validation.IsValidPassword(password))
            {
                ColorCode.FailureCode("Password doesn't satisfy the said condition");
                GetAndSetPassword();
            }
        }

        private void GetAndSetRole()
        {
            ColorCode.GetInputCode("Choose your role : ");
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
            Console.WriteLine("Enter your user id : ");
            if (!int.TryParse(Console.ReadLine(), out userId) || userId.ToString().Trim().Length == 0)
            {
                ColorCode.FailureCode("User id should be in number format and cannot be empty");
                GetAndSetUserId();
            }
        }
    }
}
