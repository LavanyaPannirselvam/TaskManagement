using ConsoleApp1.DataBase;
using ConsoleApp1.Enumeration;
using ConsoleApp1.Utils;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.PresentationLayer
{
    public class Start
    {
        private static readonly CollectUserInput collectUserInput = new();
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
                int choice = Utils.Validator.getIntInRange(Enum.GetValues(typeof(MainMenuOptions)).Length);
                MainMenuOptions options = (MainMenuOptions)(choice - 1);
                switch(options)
                {
                    case MainMenuOptions.SIGNUP:Console.WriteLine(collectUserInput.CollectSignUpInput());
                        break;
                    case MainMenuOptions.SIGNIN:Console.WriteLine(collectUserInput.CollectLogInInput());
                        break;
                    case MainMenuOptions.QUIT:Environment.Exit(0);
                        break;
                }
            }
        }
    }
}

