using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementApplication.Controller.Interface
{
    public interface IAssignment
    {
        string AssignUser(int projectId, int userId);
        string DeassignUser(int projectId, int userId);
    }
}

