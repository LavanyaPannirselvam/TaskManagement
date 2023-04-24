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
        private static readonly CollectUserInput collectUserInput = new();

        public static void ShowUserMenu() 
        {  
            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
            while (true)
            {
                ColorCode.MenuCode();
                Console.WriteLine("\t\t------User Menu------\t\t");
                foreach (UserMenuOptions option in Enum.GetValues(typeof(UserMenuOptions)))
                    Console.WriteLine((int) option + 1 + ". " + myTI.ToTitleCase(option.ToString().Replace("_", " ").ToLowerInvariant()));
                Console.WriteLine("---------------------------------------------");
                ColorCode.GetInputCode("Choose your choice : ");
                int choice = Validation.getIntInRange(Enum.GetValues(typeof(UserMenuOptions)).Length);
                UserMenuOptions options = (UserMenuOptions)(choice - 1);
                switch (options)
                {
                    case UserMenuOptions.REQUEST_FOR_SIGNUP://TODO 
                        Console.WriteLine(collectUserInput.CollectSignUpInput());
                        break;
                    case UserMenuOptions.SIGNIN:
                        {
                            string msg = collectUserInput.CollectSignInInput();
                            if (!msg.Contains("success"))
                                ColorCode.FailureCode(msg);
                            break;
                        }
                    case UserMenuOptions.QUIT:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
