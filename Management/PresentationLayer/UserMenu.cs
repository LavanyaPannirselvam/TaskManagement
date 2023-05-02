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
                    Console.WriteLine(((int)option + 1 + ". ").PadRight(4) + _myTI.ToTitleCase(option.ToString().Replace("_", " ").ToLowerInvariant()));
                Console.WriteLine("---------------------------------------------");
                ColorCode.DefaultCode("Choose your choice : ");
                int choice = Validation.GetIntInRange(Enum.GetValues(typeof(UserMenuOptions)).Length);
                ColorCode.DefaultCode("\n");
                UserMenuOptions options = (UserMenuOptions)(choice - 1);
                switch (options)
                {
                    case UserMenuOptions.LOGIN:
                        {
                                string msg = _collectUserInput.CollectSignInInput();
                                if (!msg.Contains("success"))
                                    ColorCode.FailureCode(msg);                            
                            break;
                        }
                    case UserMenuOptions.BACK:
                        return;
                }
            }
        }
    }
}
