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
    public class AdminMenu//TODO
    {
        private static readonly CollectUserInput collectUserInput = new();

        public static void ShowAdminMenu()
        {
            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
            while (true)
            {
                ColorCode.MenuCode();
                Console.WriteLine("\t\t------Admin Menu------\t\t");
                foreach (AdminMenuOptions option in Enum.GetValues(typeof(AdminMenuOptions)))
                    Console.WriteLine((int)option + 1 + ". " + myTI.ToTitleCase(option.ToString().Replace("_", " ").ToLowerInvariant()));
                Console.WriteLine("---------------------------------------------");
                ColorCode.GetInputCode("Choose your choice : ");
                int choice = Validation.getIntInRange(Enum.GetValues(typeof(AdminMenuOptions)).Length);
                AdminMenuOptions options = (AdminMenuOptions)(choice - 1);
                switch (options)
                {
                    case AdminMenuOptions.LOGIN:
                        {
                            string msg = collectUserInput.CollectSignInInput();
                            ColorCode.FailureCode(msg);
                            break;
                        }
                    case AdminMenuOptions.LOGOUT:
                        {
                            return;
                        }
                        
                }
            }
        }
    }
}
