using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementApplication.Utils
{
    public class ColorCode
    {
        internal static void MenuCode()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        internal static void DefaultCode(string msg)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(msg + "\n");
        }
        internal static void SuccessCode(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(msg + "\n");
        }
        internal static void FailureCode(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(msg + "\n");
        }
        internal static void PartialCode(string msg)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(msg + "\n");
        }

    }
}
