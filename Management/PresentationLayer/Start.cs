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
        public static void Run()
        {
            Console.WriteLine("Welcome");
            while (true)
            {
                Console.WriteLine("Choose your option:");
                foreach(MainMenuOptions option in Enum.GetValues(typeof(MainMenuOptions)))
                    Console.WriteLine((int)option+1+" . "+option.ToString());
                int choice =Validator.getIntInRange(Enum.GetValues(typeof(MainMenuOptions)).Length);
                MainMenuOptions options = (MainMenuOptions)choice - 1;
                switch(choice)
                {
                    case MainMenuOptions.SIGNUP:
                        break;
                    case MainMenuOptions.SIGNIN:
                        break;
                    case MainMenuOptions.QUIT:Environment.Exit(0);
                        break;
                }
            }
        }
    }
}

