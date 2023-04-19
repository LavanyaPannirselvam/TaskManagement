using ConsoleApp1.DataBase;
using ConsoleApp1.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp1.PresentationLayer
{
    public class CollectInput
    {
        private DatabaseHandler databaseHandler;
        string? name;
        string? email;
        Role role;
        public CollectInput() 
        {
            databaseHandler = new DatabaseHandler();
        }

        public void CollectSignInInput()
        {
            getAndSetName();
            //Console.WriteLine("Enter your email");
            //email = Console.ReadLine();


        }
        private void getAndSetName()
        {
            Console.WriteLine("Enter your name");
            name = Console.ReadLine();
            if(string.IsNullOrEmpty(name) || ContainsSpecialOrNumericCharacters(name))
                getAndSetName();
        }

        private bool ContainsSpecialOrNumericCharacters(string input)
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
