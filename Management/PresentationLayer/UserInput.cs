﻿using System;
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
    public class UserInput
    {
        private readonly Authenticator _authenticator = new();
        private readonly UserManagement _userManagement = new();
        private readonly AdminManagement _adminManager = new();
        string? name;
        string? email;
        string? password;
        string? loginId;
        Role role;

        public string CollectSignUpInput()
        {
            GetAndSetName();
            GetAndSetEmail();
            GetAndSetPasswordForSignUp();
            GetAndSetRole();
            return _adminManager.SignUp(name!, email!, role, password!);
        }

        public string CollectSignInInput()
        {
            GetAndSetLoginId();
            GetAndSetLoginPassword();
            return _authenticator.LogInUser(loginId!, password!);
        }
        public string CallLogOutUsers()
        {
            return _authenticator.DoLogOutUser();
        }
        public string CollectSignOutUsers()
        {
            GetAndSetEmail();
            return _adminManager.SignOut(email!);
        }
        public string CallViewAssigned(int choice)
        {
            if (choice == 1)
                return _userManagement.ViewAssignedProjects();
            else if (choice == 2)
                return _userManagement.ViewAssignedTasks();
            else if (choice == 3)
                return _userManagement.ViewAssignedSubTasks();
            else if (choice == 4)
                return _userManagement.ViewAssignedSubtaskofSubtask();
            else return _userManagement.ViewAssignedIssues();
        }
        public string CallViewMyProfile()
        {
            return _userManagement.ViewProfile();
        }
        public string CallViewMyNotification()
        {
            return _userManagement.ViewNotifications();
        }
        private void GetAndSetName()
        {
            ColorCode.DefaultCode("Enter name".PadRight(20) + " : ");
            name = Console.ReadLine();
            if (string.IsNullOrEmpty(name) || name.Trim().Length == 0 || Validation.ContainsSpecialOrNumericCharacters(name))
            {
                ColorCode.FailureCode("Name should not be empty or should contain any special character");
                GetAndSetName();
            }
        }
        private void GetAndSetEmail()
        {
            ColorCode.DefaultCode("Enter email".PadRight(20) + " : ");
            email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email) || email.Trim().Length == 0 || !Validation.IsValidEmail(email))
            {
                ColorCode.FailureCode("Email should not be empty or should contain any special character other than @");
                GetAndSetEmail();
            }
        }

        private void GetAndSetPasswordForSignUp()
        {
            ColorCode.DefaultCode("Enter password (should contain atleast 1 special character, 1 number, 1 alphabet and should be atleast of 5 characters) : ");
            password = Console.ReadLine();
            if (string.IsNullOrEmpty(password) || password.Trim().Length < 5 || !Validation.IsValidPassword(password))
            {
                ColorCode.FailureCode("Password doesn't satisfy the said condition");
                GetAndSetPasswordForSignUp();
            }
        }
        private void GetAndSetRole()
        {
            foreach (Role role in Enum.GetValues(typeof(Role)))
                Console.WriteLine((int)role + 1 + " . " + role.ToString());
            ColorCode.DefaultCode("Choose your role".PadRight(20) + " : ");
            int choice = Validation.GetIntInRange(Enum.GetValues(typeof(Role)).Length);
            Role option = (Role)(choice - 1);
            switch (option)
            {
                case Role.EMPLOYEE: role = option; break;
                case Role.MANAGER: role = option; break;
                case Role.ADMIN: role = option; break;
            }
        }
        private void GetAndSetLoginId()
        {
            //ColorCode.DefaultCode("\n");
            ColorCode.DefaultCode("Enter your email id".PadRight(20) + " : ");
            loginId = Console.ReadLine();
            if (!Validation.IsValidEmail(loginId!))
                {
                    ColorCode.FailureCode("Invalid email id");
                    GetAndSetLoginId();
                }
        }
        private void GetAndSetLoginPassword()
        {
            ColorCode.DefaultCode("Enter your password".PadRight(20) + " : ");
            password = Console.ReadLine();
            if (string.IsNullOrEmpty(password) || password.Trim().Length < 5 || !Validation.IsValidPassword(password))
            {
                ColorCode.FailureCode("Password is of incorrect format");
                GetAndSetLoginPassword();
            }
        }
    }
}
//password definition should be different while logging in and for signing in -> done
