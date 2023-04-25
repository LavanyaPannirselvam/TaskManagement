using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApplication.Enumerations;

namespace TaskManagementApplication.DataBase.Interface
{
    public interface ISignProcess
    {
        string SignUp(string name, string email, Role role, string password);
        string SignOutUser();

    }
}
