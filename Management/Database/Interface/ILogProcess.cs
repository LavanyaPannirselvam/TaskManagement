using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementApplication.DataBase.Interface

{
    public interface ILogProcess
    {
        string LogInUser(int userid, string password);
        string LogOutUser();
    }
}

