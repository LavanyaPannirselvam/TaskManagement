using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApplication.Enumerations;

namespace TaskManagementApplication.Controller.Interface
{
    public interface IModify
    {
        string ChangePriority(int id, PriorityType priority);
        string ChangeStatus(int id, StatusType status);
    }
}
