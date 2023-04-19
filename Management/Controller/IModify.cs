using ConsoleApp1.Enumeration;
using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Controller
{
    public interface IModify
    {
        string ChangePriority(int projectId, PriorityType priority);
        string ChangeStatus(int projectId, StatusType status);
    }
}