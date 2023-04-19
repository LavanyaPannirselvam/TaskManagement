using ConsoleApp1.Enumeration;
using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Controller
{
    public interface IProjectManage
    {
        string CreateProject(string name, string desc, StatusType status, PriorityType type, DateOnly startDate, DateOnly endDate);
        string RemoveProject(int projectId);
    }
}