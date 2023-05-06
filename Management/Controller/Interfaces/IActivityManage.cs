using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApplication.Enumerations;

namespace TaskManagementApplication.Controller.Interface
{
    public interface IActivityManage
    {
        string CreateActivity(string name, string desc, StatusType status, PriorityType type, DateOnly startDate, DateOnly endDate,int projectId = 0,int taskId = 0,int subtaskId = 0);
        string RemoveActivity(int id);
        string ViewActivity(int id);

    }
}
