using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementApplication.DataBase.Interface

{
    public interface ILogProcess
    {
        string LogInUser(string userid, string password);
        string LogOutUser();
        string LogInAdmin(int userid, string password);
        string LogOutAdmin();
    }
}

