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
        

        
    }
}
