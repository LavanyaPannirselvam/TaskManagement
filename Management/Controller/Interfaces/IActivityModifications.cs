using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApplication.Enumerations;

namespace TaskManagementApplication.Controller.Interface
{
    public interface IActivityModifications
    {
        string ChangePriorityOfActivity(int activityId,PriorityType priority);
        string ChangeStatusOfActivity(int activityId, StatusType status);
    }
}
