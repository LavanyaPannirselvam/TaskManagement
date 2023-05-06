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
        private readonly TextInfo _myTI = new CultureInfo("en-US", false).TextInfo;
        private readonly CollectProjectInput _projectInput = new();
        private readonly CollectUserInput _userInput = new();

        public string ShowMenu(string message)
        {
            ColorCode.SuccessCode(message);
            while (true)
            {
                ColorCode.MenuCode();
                Console.WriteLine("\t\t------User Menu------\t\t");
                foreach (UserOperationsOptions menu in Enum.GetValues(typeof(UserOperationsOptions)))
                    Console.WriteLine(((int)menu + 1 + ". ").PadRight(4) + _myTI.ToTitleCase(menu.ToString().Replace("_", " ").ToLowerInvariant()));
                Console.WriteLine("---------------------------------------------");
                ColorCode.DefaultCode("Choose your choice : ");
                int choice = Validation.GetIntInRange(Enum.GetValues(typeof(UserOperationsOptions)).Length);
                ColorCode.DefaultCode("\n");
                UserOperationsOptions option = (UserOperationsOptions)(choice - 1);
                switch (option)
                {
                    case UserOperationsOptions.ASSIGN_USER:
                        {
                            int result = ShowActivityMenu();
                            string msg = "";
                            if (result == 1)
                            {
                                string msg1 = _projectInput.CollectAssignUserInput(1);
                                if (msg1.Contains("successfully"))
                                    ColorCode.SuccessCode(msg1);
                                else if (msg1.Contains("access"))
                                    ColorCode.FailureCode(msg1);
                                else ColorCode.PartialCode(msg1);
                            }
                            else if (result == 2)
                                msg = _projectInput.CollectAssignUserInput(2);
                            else if (result == 3)
                                msg = _projectInput.CollectAssignUserInput(3);
                            else if (result == 4)
                                msg = _projectInput.CollectAssignUserInput(4);
                            else if (result == 5)
                                msg = _projectInput.CollectAssignUserInput(5);
                            else ShowMenu("");
                            if (msg.Contains("successfully"))
                                ColorCode.SuccessCode(msg);
                            else if (msg.Contains("not assigned"))
                                ColorCode.FailureCode(msg);
                            else ColorCode.PartialCode(msg);
                            break;
                        }
                    case UserOperationsOptions.DEASSIGN_USER:
                        {
                            int result = ShowActivityMenu();
                            string msg = "";
                            if (result == 1)
                            {
                                string msg1 = _projectInput.CollectDeassignUserInput(1);
                                if (msg1.Contains("successfully"))
                                    ColorCode.SuccessCode(msg1);
                                else if (msg1.Contains("access"))
                                    ColorCode.FailureCode(msg1);
                                else ColorCode.PartialCode(msg1);
                            }
                            else if (result == 2)
                                msg = _projectInput.CollectDeassignUserInput(2);
                            else if (result == 3)
                                msg = _projectInput.CollectDeassignUserInput(3);
                            else if (result == 4)
                                msg = _projectInput.CollectDeassignUserInput(4);
                            else if (result == 5)
                                msg = _projectInput.CollectDeassignUserInput(5);
                            else ShowMenu("");
                            if (msg.Contains("successfully"))
                                ColorCode.SuccessCode(msg);
                            else if (msg.Contains("not assigned"))
                                ColorCode.FailureCode(msg);
                            else ColorCode.PartialCode(msg);
                            break;
                        }
                    case UserOperationsOptions.VIEW_ASSIGNED:
                        {
                            int result;
                            string msg="";
                            do
                            {
                                result = ShowActivityMenu();
                                if (result == 1)
                                    msg = _userInput.CallViewAssigned(1);
                                else if (result == 2)
                                    msg = _userInput.CallViewAssigned(2);
                                else if (result == 3)
                                    msg = _userInput.CallViewAssigned(3);
                                else if (result == 4)
                                    msg = _userInput.CallViewAssigned(4);
                                else if (result == 5)
                                    msg = _userInput.CallViewAssigned(5);
                                if (msg != "")
                                    ColorCode.FailureCode(msg);
                            } while (result != 6);                              
                            break;
                        }
                    case UserOperationsOptions.VIEW:
                        {
                            string msg = "";
                            int result;
                            do
                            {
                                result = ShowActivityMenu();
                                if (result == 1)
                                    msg = _projectInput.CollectViewActivityInput(1);
                                else if (result == 2)
                                    msg = _projectInput.CollectViewActivityInput(2);
                                else if (result == 3)
                                    msg = _projectInput.CollectViewActivityInput(3);
                                else if (result == 4)
                                    msg = _projectInput.CollectViewActivityInput(4);
                                else if (result == 5)
                                    msg = _projectInput.CollectViewActivityInput(5);
                                if (msg.Contains("not available"))
                                    ColorCode.FailureCode(msg);
                                else ColorCode.DefaultCode(msg);
                            } while (result != 6);
                            break;
                        }
                    case UserOperationsOptions.CHANGE_PRIORITY:
                        {
                            int result = ShowActivityMenu();
                            string msg="";
                            if (result == 1)
                            {
                                string msg1 = _projectInput.CollectChangePriorityInput(1);
                                if (msg1.Contains("successfully"))
                                    ColorCode.SuccessCode(msg1);
                                else if (msg1.Contains("access"))
                                    ColorCode.FailureCode(msg1);
                                else ColorCode.PartialCode(msg1);
                            }
                            else if (result == 2)
                                msg = _projectInput.CollectChangePriorityInput(2);
                            else if (result == 3)
                                msg = _projectInput.CollectChangePriorityInput(3);
                            else if (result == 4)
                                msg = _projectInput.CollectChangePriorityInput(4);
                            else if (result == 5)
                                msg = _projectInput.CollectChangePriorityInput(5);
                            else ShowMenu("");
                            if (msg.Contains("successfully"))
                                ColorCode.SuccessCode(msg);
                            else if (msg.Contains("not assigned"))
                                ColorCode.FailureCode(msg);
                            else ColorCode.PartialCode(msg);
                            break;
                        }
                    case UserOperationsOptions.CHANGE_STATUS:
                        {
                            int result = ShowActivityMenu();
                            string msg = "";
                            if (result == 1)
                                msg = _projectInput.CollectChangeStatusInput(1);
                            else if (result == 2)
                                msg = _projectInput.CollectChangeStatusInput(2);
                            else if (result == 3)
                                msg = _projectInput.CollectChangeStatusInput(3);
                            else if (result == 4)
                                msg = _projectInput.CollectChangeStatusInput(4);
                            else if (result == 5)
                                msg = _projectInput.CollectChangeStatusInput(5);
                            else ShowMenu("");
                                if (msg.Contains("successfully"))
                                    ColorCode.SuccessCode(msg);
                                else if (msg.Contains("not assigned"))
                                    ColorCode.FailureCode(msg);
                                else ColorCode.PartialCode(msg);
                            break;
                        }
                    case UserOperationsOptions.CREATE:
                        {
                            int result = ShowActivityMenu();
                            string msg = "";
                            if (result == 1)
                            {
                                string msg1 = _projectInput.CollectCreateInput(1);
                                if (msg1.Contains("successfully"))
                                    ColorCode.SuccessCode(msg1);
                                else if (msg1.Contains("access"))
                                    ColorCode.FailureCode(msg1);
                                else ColorCode.PartialCode(msg1);
                            }
                            else if (result == 2)
                                msg = _projectInput.CollectCreateInput(2);
                            else if (result == 3)
                                msg = _projectInput.CollectCreateInput(3);
                            else if (result == 4)
                                msg = _projectInput.CollectCreateInput(4);
                            else if(result == 5)
                                msg = _projectInput.CollectCreateInput(5);
                            else
                                ShowMenu("");
                            if (msg.Contains("successfully"))
                                ColorCode.SuccessCode(msg);
                            else if (msg.Contains("not assigned"))
                                ColorCode.FailureCode(msg);
                            else ColorCode.PartialCode(msg);
                            break;
                        }
                    case UserOperationsOptions.DELETE:
                        {
                            int result = ShowActivityMenu();
                            string msg = "";
                            if (result == 1)
                            {
                                string msg1 = _projectInput.CollectDeleteInput(1);
                                if (msg1.Contains("successfully"))
                                    ColorCode.SuccessCode(msg1);
                                else if (msg1.Contains("access"))
                                    ColorCode.FailureCode(msg1);
                                else ColorCode.PartialCode(msg1);
                            }
                            else if (result == 2)
                                msg = _projectInput.CollectDeleteInput(2);
                            else if (result == 3)
                                msg = _projectInput.CollectDeleteInput(3);
                            else if (result == 4)
                                msg = _projectInput.CollectDeleteInput(4);
                            else if(result == 5)
                                msg = _projectInput.CollectDeleteInput(5);
                            else
                                ShowMenu("");
                            if (msg.Contains("successfully"))
                                ColorCode.SuccessCode(msg);
                            else if (msg.Contains("not assigned"))
                                ColorCode.FailureCode(msg);
                            else ColorCode.PartialCode(msg);
                            break;
                        }
                    case UserOperationsOptions.VIEW_MY_PROFILE:
                        {
                            ColorCode.DefaultCode(_userInput.CallViewMyProfile());
                            break;
                        }
                    case UserOperationsOptions.VIEW_NOTIFICATION:
                        {
                            ColorCode.DefaultCode(_userInput.CallViewMyNotification());
                            break;
                        }
                    case UserOperationsOptions.LOGOUT:
                        {
                            string msg = _userInput.CallLogOutUsers();
                            if (msg.Contains("successfully"))
                            {
                                ColorCode.SuccessCode(msg);
                                Start.Run();

                            }
                            else ColorCode.FailureCode(msg);
                            return "";
                        }
                    case UserOperationsOptions.BACK: _userInput.CallLogOutUsers(); return "";
                }
            }
        }
        private int ShowActivityMenu()
        {
            ColorCode.MenuCode();
            Console.WriteLine("\t\t------Choose between activity------\t\t");
            foreach (ActivityOptions menu in Enum.GetValues(typeof(ActivityOptions)))
                Console.WriteLine((int)menu + 1 + ". " + _myTI.ToTitleCase(menu.ToString().Replace("_", " ").ToLowerInvariant()));
            Console.WriteLine("---------------------------------------------");
            ColorCode.DefaultCode("Choose your choice : ");
            int choice = Validation.GetIntInRange(Enum.GetValues(typeof(ActivityOptions)).Length);
            ColorCode.DefaultCode("\n");
            return choice;
        }
    }
}
//if project is selected,prompt should have project mentioned in it and for task respectively -> done
//list of available tasks with their project id and id should be mentioned also for userd -> done