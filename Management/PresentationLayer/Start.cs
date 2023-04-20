using ConsoleApp1.DataBase;
using ConsoleApp1.Enumeration;
using ConsoleApp1.Utils;
using System;
using System.ComponentModel.DataAnnotations;
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
            Console.WriteLine("Welcome");
            while (true)
            {
                Console.WriteLine("Choose your option:");
                foreach(MainMenuOptions option in Enum.GetValues(typeof(MainMenuOptions)))
                    Console.WriteLine((int)option+1+" . "+option.ToString());
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

