using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApplication.Model;

namespace TaskManagementApplication.Controller.Interface
{
    public interface IActivityList
    {
        public Dictionary<int, Project> ProjectsList();
        public Dictionary<int, Tasks> TasksList();
        public Dictionary<int, SubTask> SubTasksList();
        public Dictionary<int, SmallSubTask> SmallSubTasksList();
        public Dictionary<int, Issue> IssuesList();
        public Dictionary<int, User> UsersList();
    }
}
