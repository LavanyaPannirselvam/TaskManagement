using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementApplication.Utils
{
    public class ColorCode
    {
        public static void MenuCode()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        public static void DefaultCode(string msg)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(msg);
        }
        public static void SuccessCode(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(msg +"\n");
        }
        public static void FailureCode(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(msg + "\n");
        }
        public static void PartialCode(string msg)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(msg + "\n");
        }

    }
}
