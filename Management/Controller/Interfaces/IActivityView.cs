using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementApplication.Controller.Interface
{
    public interface IActivityView
    {
        string ViewAssignedProjects();
        string ViewAssignedTasks();
        string ViewAssignedSubTasks();
        string ViewAssignedSubtaskofSubtask();
        string ViewAssignedIssues();
        string ViewProfile();
        string ViewNotifications();
    }
}
