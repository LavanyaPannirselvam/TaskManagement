using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TaskManagementApplication.Utils
{
    public class Validation
    {
        public static int getIntInRange(int max)
        {
            int num;
            if (int.TryParse(Console.ReadLine(), out num))
            {
                if (num > 0 && num <= max)
                    return num;
                else
                {
                    ColorCode.FailureCode("Wrong option entered");
                    ColorCode.GetInputCode("Choose your choice : ");
                    return getIntInRange(max);
                }
            }
            else
            {
                ColorCode.FailureCode("Wrong format input");
                ColorCode.GetInputCode("Enter the correct choice : ");
                return getIntInRange(max);
            }
        }
        public static bool ContainsSpecialOrNumericCharacters(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                    return true;
            }
            return false;
        }
        public static bool IsValidEmail(string email)
        {
            string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";
            return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
        }

        public static bool IsValidPassword(string password)
        {
            string regex = "^(?=.*[a-z])(?=.+ *[A-Z])(?=.*\\d)+(?=.*[-+_!@#$%^&*., ?]).+$";
            return Regex.IsMatch(password, regex, RegexOptions.IgnoreCase);
        }
    }
}
