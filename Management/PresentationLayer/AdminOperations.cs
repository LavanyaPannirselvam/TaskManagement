using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApplication.Controller;
using TaskManagementApplication.Enumerations;
using TaskManagementApplication.Utils;

namespace TaskManagementApplication.Presentation
{
    public class AdminOperations
    {
        private readonly TextInfo _myTI = new CultureInfo("en-US", false).TextInfo;
        private readonly AdminFunctions _adminFunctions = new();
        private readonly CollectUserInput _userInput = new();
        public string ShowMenu(string message)
        {
            ColorCode.SuccessCode(message);
            while (true)
            {
                ColorCode.MenuCode();
                Console.WriteLine("\t\t------Admin Menu------\t\t");
                foreach (AdminOperationOptions menu in Enum.GetValues(typeof(AdminOperationOptions)))
                    Console.WriteLine((int)menu + 1 + ". " + _myTI.ToTitleCase(menu.ToString().Replace("_", " ").ToLowerInvariant()));
                Console.WriteLine("---------------------------------------------");
                ColorCode.DefaultCode("Choose your choice : ");
                int choice = Validation.GetIntInRange(Enum.GetValues(typeof(AdminOperationOptions)).Length);
                ColorCode.DefaultCode("\n");
                AdminOperationOptions option = (AdminOperationOptions)(choice - 1);
                switch (option)
                {
                    case AdminOperationOptions.SHOW_NOTIFICATIONS:
                        {
                            string msg = _adminFunctions.ShowNotifications();
                            if(msg!="")
                                ColorCode.FailureCode(msg);
                            break;
                        }
                    case AdminOperationOptions.CREATE_USER:
                        {
                            string msg = _userInput.CollectSignUpInput();
                            if (msg.Contains("failed"))
                                ColorCode.FailureCode(msg);
                            else ColorCode.SuccessCode(msg);
                            break;
                        }
                        case AdminOperationOptions.DELETE_USER:
                        {
                            string msg = _userInput.CallSignOutUsers();
                            if (msg.Contains("successful"))
                                ColorCode.SuccessCode(msg);
                            else ColorCode.FailureCode(msg);
                            break;
                        }
                    case AdminOperationOptions.LOGOUT:
                        {
                            string msg =  _userInput.CallLogOutAdmin();
                            if (msg.Contains("successfully"))
                                ColorCode.SuccessCode(msg);
                            else ColorCode.FailureCode(msg);
                            return "";
                        }
                    case AdminOperationOptions.BACK:
                        {
                            _userInput.CallLogOutAdmin();
                            return "";
                        }
                }
            }
        }
    }
}
//back and logout should be handled
//should have menu option as Create user

