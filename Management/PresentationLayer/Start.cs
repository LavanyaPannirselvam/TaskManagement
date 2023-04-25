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
            Console.WriteLine("Welcome");
            while (true)
            {
                ColorCode.MenuCode();
                Console.WriteLine("\t\t------Main Menu------\t\t");
                foreach (MainMenuOptions option in Enum.GetValues(typeof(MainMenuOptions)))
                    Console.WriteLine((int)option + 1 + ". " + myTI.ToTitleCase(option.ToString().Replace("_", " ").ToLowerInvariant()));
                Console.WriteLine("---------------------------------------------");
                ColorCode.GetInputCode("Choose your choice : ");
                int choice = Validation.getIntInRange(Enum.GetValues(typeof(MainMenuOptions)).Length);
                MainMenuOptions options = (MainMenuOptions)(choice - 1);
                switch (options)
                {
                    case MainMenuOptions.ADMIN:AdminMenu.ShowAdminMenu();
                        break;
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

