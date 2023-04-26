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
    public class UserOperations
    {
        private readonly TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
        private readonly CollectProjectInput projectInput = new();
        private readonly CollectUserInput userInput = new();

        public string ShowMenu(string message)
        {
            ColorCode.SuccessCode(message);
            while (true)
            {
                ColorCode.MenuCode();
                Console.WriteLine("\t\t------User Menu------\t\t");
                foreach (UserOperationsOptions menu in Enum.GetValues(typeof(UserOperationsOptions)))
                    Console.WriteLine((int)menu + 1 + ". " + myTI.ToTitleCase(menu.ToString().Replace("_", " ").ToLowerInvariant()));
                Console.WriteLine("---------------------------------------------");
                ColorCode.DefaultCode("Choose your choice : ");
                int choice = Validation.getIntInRange(Enum.GetValues(typeof(UserOperationsOptions)).Length);
                UserOperationsOptions option = (UserOperationsOptions)(choice - 1);
                switch (option)
                {
                    case UserOperationsOptions.ASSIGN_USER: 
                        {
                            if(ShowActivityMenu()==1)
                            {
                                string msg = projectInput.CollectAssignUserInput(1);
                                if (msg.Contains("successfully"))
                                    ColorCode.SuccessCode(msg);
                                else if (msg.Contains("access"))
                                    ColorCode.FailureCode(msg);
                                else ColorCode.PartialCode(msg);
                            }
                            else
                            {
                                string msg = projectInput.CollectAssignUserInput(2);
                                if(msg.Contains("successfully"))
                                    ColorCode.SuccessCode(msg);
                                else if(msg.Contains("not available"))
                                    ColorCode.FailureCode(msg);
                                else ColorCode.PartialCode(msg);
                            }
                            break; 
                        }
                    case UserOperationsOptions.DEASSIGN_USER: 
                        {
                            if (ShowActivityMenu()==1)
                            {
                                string msg = projectInput.CollectDeassignUserInput(1);
                                if (msg.Contains("successfully"))
                                    ColorCode.SuccessCode(msg);
                                else if (msg.Contains("access"))
                                    ColorCode.FailureCode(msg);
                                else ColorCode.PartialCode(msg);
                            }
                            else
                            {
                                string msg = projectInput.CollectDeassignUserInput(2);
                                if (msg.Contains("successfully"))
                                    ColorCode.SuccessCode(msg);
                                else if (msg.Contains("not available"))
                                    ColorCode.FailureCode(msg);
                                else ColorCode.PartialCode(msg);
                            }
                            break; 
                        }
                    case UserOperationsOptions.VIEW_ASSIGNED:
                        {
                            if (ShowActivityMenu() == 1)
                            {
                                string msg = userInput.CallViewAssigned(1);
                                if (msg != "")
                                    ColorCode.FailureCode(msg);
                            }
                            else
                            {
                                string msg = userInput.CallViewAssigned(2);
                                if(msg!="")
                                    ColorCode.FailureCode(msg);
                            }
                                 break; 
                        }
                    case UserOperationsOptions.VIEW: 
                        {
                            if (ShowActivityMenu() == 1)
                            {
                                string msg = projectInput.CollectViewProjectInput(1);
                                if (msg.Contains("not available"))
                                    ColorCode.FailureCode("msg");
                                else ColorCode.DefaultCode(msg);
                            }
                            else
                            {
                                string msg = projectInput.CollectViewProjectInput(2);
                                if (msg.Contains("not available"))
                                    ColorCode.FailureCode("msg");
                                else ColorCode.DefaultCode(msg);
                            }
                            break;
                        }
                    case UserOperationsOptions.CHANGE_PRIORITY:
                        {
                            if (ShowActivityMenu() == 1)
                            {
                                string msg = projectInput.CollectChangePriorityInput(1);
                                if (msg.Contains("successfully"))
                                    ColorCode.SuccessCode(msg);
                                else if (msg.Contains("access"))
                                    ColorCode.FailureCode(msg);
                                else ColorCode.PartialCode(msg);
                            }
                            else
                            {
                                string msg = projectInput.CollectChangePriorityInput(2);
                                if(msg.Contains("successfully"))
                                    ColorCode.SuccessCode(msg);
                                else if(msg.Contains("not available"))
                                    ColorCode.FailureCode(msg);
                                else ColorCode.PartialCode(msg);
                            }
                            break; 
                        }
                    case UserOperationsOptions.CHANGE_STATUS: 
                        {
                            if (ShowActivityMenu() == 1)
                            {
                                string msg = projectInput.CollectChangeStatusInput(1);
                                if (msg.Contains("successfully"))
                                    ColorCode.SuccessCode(msg);
                                else if (msg.Contains("access"))
                                    ColorCode.FailureCode(msg);
                                else ColorCode.PartialCode(msg);
                            }
                            else
                            {
                                string msg = projectInput.CollectChangeStatusInput(2);
                                if (msg.Contains("successfully"))
                                    ColorCode.SuccessCode(msg);
                                else if (msg.Contains("not available"))
                                    ColorCode.FailureCode(msg);
                                else ColorCode.PartialCode(msg);
                            }
                            break; 
                        }
                    case UserOperationsOptions.CREATE: 
                        {
                            if (ShowActivityMenu() == 1)
                            {
                                string msg = projectInput.CollectCreateInput(1);
                                if (msg.Contains("successfully"))
                                    ColorCode.SuccessCode(msg);
                                else if (msg.Contains("access"))
                                    ColorCode.FailureCode(msg);
                                else ColorCode.PartialCode(msg);
                            }
                            else
                            {
                                string msg= projectInput.CollectCreateInput(2);
                                if(msg.Contains("successfully"))
                                    ColorCode.SuccessCode(msg);
                                else if(msg.Contains("not available"))
                                    ColorCode.FailureCode(msg);
                                else ColorCode.PartialCode(msg);
                            }
                            break; 
                        }
                    case UserOperationsOptions.DELETE: 
                        {
                            if (ShowActivityMenu() == 1)
                            {
                                string msg = projectInput.CollectDeleteInput(1);
                                if (msg.Contains("successfully"))
                                    ColorCode.SuccessCode(msg);
                                else if (msg.Contains("access"))
                                    ColorCode.FailureCode(msg);
                                else ColorCode.PartialCode(msg);
                            }
                            else
                            {
                                string msg = projectInput.CollectDeleteInput(2);
                                if (msg.Contains("successfully"))
                                    ColorCode.SuccessCode(msg);
                                else if (msg.Contains("not available"))
                                    ColorCode.FailureCode(msg);
                                else ColorCode.PartialCode(msg);
                            }
                            break; 
                        }
                    case UserOperationsOptions.VIEW_PROFILE:
                        {
                            ColorCode.DefaultCode(userInput.CallViewMyProfile());
                            break;
                        }
                    case UserOperationsOptions.VIEW_NOTIFICATION:
                        {
                            ColorCode.DefaultCode(userInput.CallViewMyNotification());
                            break;
                        }
                    case UserOperationsOptions.LOGOUT:
                        { 
                            string msg = userInput.CallLogOutApprovedUsers();
                            if (msg.Contains("successfully"))
                                ColorCode.SuccessCode(msg);
                            else ColorCode.FailureCode(msg);
                            Start.Run();
                            return ""; 
                        }
                    case UserOperationsOptions.SIGNOUT:
                        { 
                            string msg = userInput.CallSignOutApprovedUsers();
                            if (msg.Contains("successful"))
                                ColorCode.SuccessCode(msg);
                            else if (msg.Contains("Cannot"))
                                ColorCode.PartialCode(msg);
                            else ColorCode.FailureCode(msg);
                            Start.Run();
                            return ""; 
                        }
                    case UserOperationsOptions.BACK: return "";
                }
            }
        }
        private int ShowActivityMenu()
        {
            ColorCode.MenuCode();
            Console.WriteLine("\t\t------Choose between activity------\t\t");
            foreach (ActivityOptions menu in Enum.GetValues(typeof(ActivityOptions)))
                Console.WriteLine((int)menu + 1 + ". " + myTI.ToTitleCase(menu.ToString().Replace("_", " ").ToLowerInvariant()));
            Console.WriteLine("---------------------------------------------");
            ColorCode.DefaultCode("Choose your choice : ");
            int choice = Validation.getIntInRange(Enum.GetValues(typeof(ActivityOptions)).Length);
            Console.WriteLine($"Activity is : {choice}");
            return choice;
        }
    }
}
