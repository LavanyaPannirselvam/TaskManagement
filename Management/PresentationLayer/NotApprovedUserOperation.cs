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
    public class NotApprovedUserOperation
    {
        private readonly TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
        private readonly CollectUserInput userInput = new();
        public string ShowMenu(string message)
        {
            ColorCode.SuccessCode(message);
            while (true)
            {
                ColorCode.MenuCode();
                Console.WriteLine("\t\t------Not Approved User Menu------\t\t");
                foreach (NotApprovedUserOptions menu in Enum.GetValues(typeof(NotApprovedUserOptions)))
                    Console.WriteLine((int)menu + 1 + ". " + myTI.ToTitleCase(menu.ToString().Replace("_", " ").ToLowerInvariant()));
                Console.WriteLine("---------------------------------------------");
                ColorCode.DefaultCode("Choose your choice : ");
                int choice = Validation.getIntInRange(Enum.GetValues(typeof(NotApprovedUserOptions)).Length);
                NotApprovedUserOptions option = (NotApprovedUserOptions)(choice - 1);
                switch(option)
                {
                    case NotApprovedUserOptions.SHOW_NOTIFICATION:
                        {
                            ColorCode.DefaultCode(userInput.CallViewTemporaryUserNotification());
                            break;
                        }
                        case NotApprovedUserOptions.LOGOUT:
                        {
                            string msg = userInput.CallLogOutTemporaryUser();
                            if (msg.Contains("successful"))
                                ColorCode.SuccessCode(msg);
                            else ColorCode.FailureCode(msg);
                            return "";
                        }
                    case NotApprovedUserOptions.BACK: return "";
                }
            }
        }
    }
}
