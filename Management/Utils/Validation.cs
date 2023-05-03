using System.ComponentModel.Design;
using System.Text.RegularExpressions;

namespace TaskManagementApplication.Utils
{
    public class Validation
    {
        public static int GetIntInRange(int max)
        {
            int num;
            if (int.TryParse(Console.ReadLine(), out num))
            {
                if (num > 0 && num <= max)
                    return num;
                else
                {
                    ColorCode.FailureCode("Wrong option entered");
                    ColorCode.DefaultCode("\nChoose your choice : ");
                    return GetIntInRange(max);
                }
            }
            else
            {
                ColorCode.FailureCode("Wrong format input");
                ColorCode.DefaultCode("\nEnter the correct choice : ");
                return GetIntInRange(max);
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
            string regex = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)+(?=.*[-+_!@#$%^&*., ?]).+$";
            return Regex.IsMatch(password, regex, RegexOptions.IgnoreCase);
        }

        public static bool IsChoiceAvailable(int choice,Dictionary<int,string> list)
        {
            foreach(int i in list.Keys)
            {
                if(choice == i) 
                    return true;
            }
            return false;
        }
    }
}
