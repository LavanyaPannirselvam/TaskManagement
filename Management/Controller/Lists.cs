using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApplication.Controller.Interface;
using TaskManagementApplication.DataBase;

namespace TaskManagementApplication.Controller
{
    public class Lists : IList
    {
        private readonly Database _database = Database.GetInstance();

        public Dictionary<int, string> ProjectsList()
        {
            return _database.ProjectsList();
        }

        public Dictionary<int, string> TasksList()
        {
            return _database.TasksList();  
        }
        public Dictionary<int, string> SubTasksList()
        {
            return _database.SubTasksList();
        }
        public Dictionary<int, string> SmallSubTasksList()
        {
            return _database.SubTasksList();
        }
        public Dictionary<int, string> UsersList()
        {
            return _database.UsersList();
        }
    }
}
