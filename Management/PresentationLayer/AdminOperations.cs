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
                int choice = Validation.getIntInRange(Enum.GetValues(typeof(AdminOperationOptions)).Length);
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
                    case AdminOperationOptions.APPROVE_USER:
                        {
                            string msg = _userInput.CollectApproveUserInput();
                            if(msg.Contains("approved"))
                                ColorCode.SuccessCode(msg);
                            else ColorCode.FailureCode(msg);
                            break;
                        }
                    case AdminOperationOptions.LOGOUT:
                        {
                            _userInput.CallLogOutAdmin();
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
