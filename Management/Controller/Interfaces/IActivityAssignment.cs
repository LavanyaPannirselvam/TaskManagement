using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementApplication.Controller.Interface
{
    public interface IActivityAssignment
    {
        string AssignUser(int id, int userId);
        string DeassignUser(int id, int userId);
    }
}

