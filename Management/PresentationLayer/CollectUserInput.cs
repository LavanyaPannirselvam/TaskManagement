using ConsoleApp1.Controller;
using ConsoleApp1.DataBase;
using ConsoleApp1.Enumeration;
using ConsoleApp1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp1.PresentationLayer
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
            return authenticator.GetSignUpData(name, email,role, password);
        }

        public string CollectLogInInput()
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
            return userManagement.ViewMyProjects();
        }
        private void GetAndSetName()
        {
            Console.WriteLine("Enter your name : ");
            name = Console.ReadLine();
            if (string.IsNullOrEmpty(name) || name.Trim().Length == 0 || Validator.ContainsSpecialOrNumericCharacters(name))
            {
                Console.WriteLine("Name should not be empty or should contain any special character");
                GetAndSetName();
            }
        }

        private void GetAndSetEmail()
        {
            Console.WriteLine("Enter your email : ");
            email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email) || email.Trim().Length == 0 ||! Validator.IsValidEmail(email))
            {
                Console.WriteLine("Email should not be empty or should contain any special character other than @");
                GetAndSetEmail();
            }
        }

        private void GetAndSetPassword()
        {
            Console.WriteLine("Enter password (should contain atleast 1 uppercase letter, 1 lowercase letter, 1 special character and should be atleast of 5 characters) : ");
            password = Console.ReadLine();
            if (string.IsNullOrEmpty(password) || password.Trim().Length < 5 || !Validator.IsValidPassword(password))
            {
                Console.WriteLine("Password doesn't satisfy the said condition");
                GetAndSetPassword();
            }
        }

        private void GetAndSetRole()
        {
            Console.WriteLine("Choose your role : ");
            foreach(Role role in Enum.GetValues(typeof(Role)))
                Console.WriteLine((int)role+1+" . "+role.ToString());
            int choice = Validator.getIntInRange(Enum.GetValues(typeof(Role)).Length);
            Role option = (Role)(choice-1);
            switch(option)
            {
                case Role.EMPLOYEE: role = option; break;
                case Role.MANAGER: role = option; break;
                case Role.LEAD: role = option; break;
                default: GetAndSetRole(); break;
            }
            
        }
        private void GetAndSetUserId()
        {
            Console.WriteLine("Enter your user id : ");
            if (!int.TryParse(Console.ReadLine(), out userId) || userId.ToString().Trim().Length == 0)
            {
                Console.WriteLine("User id should be in number format and cannot be empty");
                GetAndSetUserId();
            }
        }
    }
}
