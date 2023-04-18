using System;

namespace ConsoleApp1.Utils
{
    public class Validator
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
                    Console.WriteLine("Wrong option entered!\nChoose your option:");
                    return getIntInRange(max);
                }
            }
            else
            {
                Console.WriteLine("Wrong format input\nEnter the correct option");
                scan.nextLine();
                return getIntInRange(max);
            }
        }
    }
}

