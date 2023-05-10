using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApplication.Controller.Interface;
using TaskManagementApplication.DataBase;
using TaskManagementApplication.Model;

namespace TaskManagementApplication.Controller
{
    public class ActivityList : IActivityList
    {
        private readonly Database _database = Database.GetInstance();

        public Dictionary<int, Project> ProjectsList()
        {
            return _database.ProjectsList();
        }
        public Dictionary<int, Tasks> TasksList()
        {
            return _database.TasksList();  
        }
        public Dictionary<int, SubTask> SubTasksList()
        {
            return _database.SubTasksList();
        }
        public Dictionary<int, SmallSubTask> SmallSubTasksList()
        {
            return _database.SmallSubTasksList();
        }
        public Dictionary<int, Issue> IssuesList()
        {
            return _database.IssuesList();
        }
        public Dictionary<int, User> UsersList()
        {
            return _database.UsersList();
        }
    }
}
