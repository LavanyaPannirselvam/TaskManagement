using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementApplication.Controller.Interface
{
    public interface IView
    {
        string ViewAssignedProjects();
        string ViewAssignedTasks();
        string ViewProfile();
        string ViewNotifications();
    }
}
