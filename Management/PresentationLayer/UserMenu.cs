using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApplication.Enumerations;
using TaskManagementApplication.Utils;

namespace TaskManagementApplication.Presentation
{
    public class UserMenu
    {
        private static readonly CollectUserInput _collectUserInput = new();
        private readonly static TextInfo _myTI = new CultureInfo("en-US", false).TextInfo;

        public static void ShowUserMenu() 
        {  
            while (true)
            {
                ColorCode.MenuCode();
                Console.WriteLine("\t\t------User Menu------\t\t");
                foreach (UserMenuOptions option in Enum.GetValues(typeof(UserMenuOptions)))
                    Console.WriteLine((int) option + 1 + ". " + _myTI.ToTitleCase(option.ToString().Replace("_", " ").ToLowerInvariant()));
                Console.WriteLine("---------------------------------------------");
                ColorCode.DefaultCode("Choose your choice : ");
                int choice = Validation.getIntInRange(Enum.GetValues(typeof(UserMenuOptions)).Length);
                UserMenuOptions options = (UserMenuOptions)(choice - 1);
                switch (options)
                {
                    case UserMenuOptions.REQUEST_FOR_SIGNUP:
                        {
                            string msg = _collectUserInput.CollectSignUpInput();
                            if (msg.Contains("accepted"))
                                ColorCode.SuccessCode(msg);
                            else ColorCode.FailureCode(msg);
                            break;
                        }
                    case UserMenuOptions.LOGIN:
                        {
                            if (ShowUserTypeMenu() == 1)
                            {
                                string msg = _collectUserInput.CollectSignInInput();
                                if (!msg.Contains("success"))
                                    ColorCode.FailureCode(msg);
                            }
                            else
                            {
                                string msg = _collectUserInput.CollectNotApproveUserLoginInput();
                                if (!msg.Contains("success"))
                                    ColorCode.FailureCode(msg);
                            }
                            break;
                        }
                    case UserMenuOptions.BACK:
                        return;
                }
            }
        }

        private static int ShowUserTypeMenu()
        {
            ColorCode.MenuCode();
            Console.WriteLine("\t\t------Choose between user type------\t\t");
            foreach (UserApprovalOptions menu in Enum.GetValues(typeof(UserApprovalOptions)))
                Console.WriteLine((int)menu + 1 + ". " + _myTI.ToTitleCase(menu.ToString().Replace("_", " ").ToLowerInvariant()));
            Console.WriteLine("---------------------------------------------");
            ColorCode.DefaultCode("Choose your choice : ");
            int choice = Validation.getIntInRange(Enum.GetValues(typeof(UserApprovalOptions)).Length);
            return choice;
        }
    }
}
