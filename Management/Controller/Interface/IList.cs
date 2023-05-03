using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApplication.DataBase;

namespace TaskManagementApplication.Controller.Interface
{
    public interface IList
    {
        Dictionary<int, string> ProjectsList();
        Dictionary<int, string> TasksList();
        Dictionary<int, string> SubTasksList();
        Dictionary<int, string> SmallSubTasksList();
        Dictionary<int, string> UsersList();
    }
}
