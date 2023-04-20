using ConsoleApp1.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.DataBase
{
    public  interface ISignProcess
    {
        string SignUp(string name, string email, Role role, string password);
        string SignOut();


    }
}
