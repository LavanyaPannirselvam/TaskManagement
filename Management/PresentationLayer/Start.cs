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
    public class Start
    {
        public static void Run()
        {
            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
            while (true)
            {
                ColorCode.MenuCode();
                Console.WriteLine("\t\t------Main Menu------\t\t");
                foreach (MainMenuOptions option in Enum.GetValues(typeof(MainMenuOptions)))
                    Console.WriteLine(((int)option + 1 + ". ").PadRight(4) + myTI.ToTitleCase(option.ToString().Replace("_", " ").ToLowerInvariant()));
                Console.WriteLine("---------------------------------------------");
                ColorCode.DefaultCode("Choose your choice : ");
                int choice = Validation.GetIntInRange(Enum.GetValues(typeof(MainMenuOptions)).Length);
                ColorCode.DefaultCode("\n");
                MainMenuOptions options = (MainMenuOptions)(choice - 1);
                switch (options)
                {
                    /*case MainMenuOptions.ADMIN:
                        {     
                            CollectUserInput _userInput = new();
                            string msg = _userInput.CollectAdminSignInInput();
                            if (!msg.Contains("success"))
                                ColorCode.FailureCode(msg);
                            break;
                        }

                    */
                    case MainMenuOptions.USER:UserMenu.ShowUserMenu();
                        break;
                    case MainMenuOptions.QUIT:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
//need to have login function diretly for admin -> done
//need to get input from user in single line

