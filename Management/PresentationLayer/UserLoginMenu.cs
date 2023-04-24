using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApplication.Enumerations;
using TaskManagementApplication.Utils;

namespace TaskManagementApplication.Presentation
{
    public class UserLoginMenu
    {
        private readonly CollectProjectInput projectInput = new();
        private readonly CollectUserInput userInput = new();

        public string ShowMenu(string message)
        {
            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
            ColorCode.SuccessCode(message);
            while (true)
            {
                ColorCode.MenuCode();
                Console.WriteLine("\t\t------User Menu------\t\t");
                foreach (UserLoginMenuOptions menu in Enum.GetValues(typeof(UserLoginMenuOptions)))
                    Console.WriteLine((int)menu + 1 + ". " + myTI.ToTitleCase(menu.ToString().Replace("_", " ").ToLowerInvariant()));
                Console.WriteLine("---------------------------------------------");
                ColorCode.GetInputCode("Choose your choice : ");
                int choice = Validation.getIntInRange(Enum.GetValues(typeof(UserLoginMenuOptions)).Length);
                UserLoginMenuOptions option = (UserLoginMenuOptions)(choice - 1);
                switch (option)
                {
                    case UserLoginMenuOptions.ASSIGN_USER: 
                        { string msg = projectInput.CollectAssignUserInput();
                            if (msg.Contains("successfully"))
                                ColorCode.SuccessCode(msg);
                            else if(msg.Contains("access"))
                                ColorCode.FailureCode(msg);
                            else ColorCode.PartialCode(msg);
                                    break; 
                        }
                    case UserLoginMenuOptions.DEASSIGN_USER: 
                        {
                            string msg = projectInput.CollectDeassignUserInput();
                            if (msg.Contains("successfully"))
                                ColorCode.SuccessCode(msg);
                            else if (msg.Contains("access"))
                                ColorCode.FailureCode(msg);
                            else ColorCode.PartialCode(msg);
                            break; 
                        }
                    case UserLoginMenuOptions.VIEW_MY_PROJECTS:
                        {
                            string msg = userInput.CallViewMyProjects();
                            if(msg!="")
                                ColorCode.FailureCode(msg);
                                 break; 
                        }
                    case UserLoginMenuOptions.VIEW_PROJECT: 
                        {
                            string msg = projectInput.CollectViewProjectInput();
                            if (msg.Contains("not available"))
                                ColorCode.FailureCode("msg");
                            else ColorCode.GetInputCode(msg);
                            break;
                        }
                    case UserLoginMenuOptions.CHANGE_PRIORITY:
                        {
                            string msg = projectInput.CollectChangePriorityInput();
                            if (msg.Contains("successfully"))
                                ColorCode.SuccessCode(msg);
                            else if (msg.Contains("access"))
                                ColorCode.FailureCode(msg);
                            else ColorCode.PartialCode(msg); 
                            break; 
                        }
                    case UserLoginMenuOptions.CHANGE_STATUS: 
                        { 
                            string msg = projectInput.CollectChangeStatusInput();
                            if (msg.Contains("successfully"))
                                ColorCode.SuccessCode(msg);
                            else if (msg.Contains("access"))
                                ColorCode.FailureCode(msg);
                            else ColorCode.PartialCode(msg); 
                            break; 
                        }
                    case UserLoginMenuOptions.CREATE_PROJECT: 
                        { 
                            string msg = projectInput.CollectCreateProjectInput();
                            if (msg.Contains("successfully"))
                                ColorCode.SuccessCode(msg);
                            else if (msg.Contains("access"))
                                ColorCode.FailureCode(msg);
                            else ColorCode.PartialCode(msg); 
                            break; 
                        }
                    case UserLoginMenuOptions.DELETE_PROJECT: 
                        { 
                            string msg = projectInput.CollectDeleteProjectInput();
                            if (msg.Contains("successfully"))
                                ColorCode.SuccessCode(msg);
                            else if (msg.Contains("access"))
                                ColorCode.FailureCode(msg);
                            else ColorCode.PartialCode(msg);
                            break; 
                        }
                    case UserLoginMenuOptions.VIEW_PROFILE:
                        {
                            ColorCode.GetInputCode(userInput.CallViewMyProfile());
                            break;
                        }
                    case UserLoginMenuOptions.LOGOUT: 
                        { 
                            string msg = userInput.CallLogOut();
                            if (msg.Contains("successfully"))
                                ColorCode.SuccessCode(msg);
                            else ColorCode.FailureCode(msg);
                            return ""; 
                        }
                    case UserLoginMenuOptions.SIGNOUT: 
                        { 
                            string msg = userInput.CallSignOut();
                            if (msg.Contains("successful"))
                                ColorCode.SuccessCode(msg);
                            else if (msg.Contains("Cannot"))
                                ColorCode.PartialCode(msg);
                            else ColorCode.FailureCode(msg);
                            return ""; 
                        }
                }
            }
        }
    }
}
