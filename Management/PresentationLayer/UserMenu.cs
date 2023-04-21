using ConsoleApp1.Enumeration;
using ConsoleApp1.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.PresentationLayer
{
    public class UserMenu
    {
        private readonly CollectProjectInput projectInput = new();
        private readonly CollectUserInput userInput = new();

        public string ShowMenu(string msg)
        {
            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
            ColorCode.SuccessCode(msg);
            while (true)
            {
                ColorCode.MenuCode();
                Console.WriteLine("\t\t------User Menu------\t\t");
                foreach (UserMenuOptions menu in Enum.GetValues(typeof(UserMenuOptions)))
                    Console.WriteLine((int)menu + 1 + ". " + myTI.ToTitleCase(menu.ToString().Replace("_"," ").ToLowerInvariant()));
                Console.WriteLine("---------------------------------------------");
                ColorCode.GetInputCode("Choose your choice : ");
                int choice = Validator.getIntInRange(Enum.GetValues(typeof(UserMenuOptions)).Length);
                UserMenuOptions option = (UserMenuOptions)(choice - 1);
                switch (option)
                {
                    case UserMenuOptions.ASSIGN_USER: Console.WriteLine(projectInput.CollectAssignUserInput()); break;
                    case UserMenuOptions.DEASSIGN_USER: Console.WriteLine(projectInput.CollectDeassignUserInput()); break;
                    case UserMenuOptions.VIEW_MY_PROJECTS: Console.WriteLine(userInput.CallViewMyProjects()); break;
                    case UserMenuOptions.VIEW_PROJECT: Console.WriteLine(projectInput.CollectViewProjectInput()); break;
                    case UserMenuOptions.CHANGE_PRIORITY: Console.WriteLine(projectInput.CollectChangePriorityInput()); break;
                    case UserMenuOptions.CHANGE_STATUS: Console.WriteLine(projectInput.CollectChangeStatusInput()); break;
                    case UserMenuOptions.CREATE_PROJECT: Console.WriteLine(projectInput.CollectCreateProjectInput()); break;
                    case UserMenuOptions.DELETE_PROJECT: Console.WriteLine(projectInput.CollectDeleteProjectInput()); break;
                    case UserMenuOptions.LOGOUT: Console.WriteLine(userInput.CallLogOut()); return "";
                    case UserMenuOptions.SIGNOUT: Console.WriteLine(userInput.CallSignOut()); return "";
                }
            }
        }
    }
}
