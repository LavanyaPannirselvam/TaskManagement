using ConsoleApp1.Controller;
using ConsoleApp1.Enumeration;
using ConsoleApp1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.PresentationLayer
{
    public class UserMenu
    {
        private readonly CollectProjectInput projectInput = new();
        private readonly CollectUserInput userInput = new();

        public void ShowMenu(string msg)
        {
            //Console.WriteLine(msg);
            while (true)
            {
                Console.WriteLine("Choose your option : ");
                foreach (UserMenuOptions menu in Enum.GetValues(typeof(UserMenuOptions)))
                    Console.WriteLine((int)menu + 1 + " . " + menu.ToString());
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
                    case UserMenuOptions.LOGOUT: Console.WriteLine(userInput.CallLogOut()); return;
                    case UserMenuOptions.SIGNOUT: Console.WriteLine(userInput.CallSignOut()); break;

                }
            }
        }
    }
}
