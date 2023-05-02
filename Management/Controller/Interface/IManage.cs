using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApplication.Enumerations;

namespace TaskManagementApplication.Controller.Interface
{
    public interface IManage
    {
        string Create(string name, string desc, StatusType status, PriorityType type, DateOnly startDate, DateOnly endDate,int projectId = 0,int taskId = 0);
        string Remove(int id);
        string View(int id);

    }
}
