using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApplication.DataBase;
using TaskManagementApplication.Model;
using TaskManagementApplication.Utils;

namespace TaskManagementApplication.Controller.Interface
{
    public class UserManagement 
    {
        private readonly Database _database = Database.GetInstance();
        private User user;
        public string ViewAssignedProjects()
        {
            user = _database.GetUser(CurrentUserHandler.CurrentUserEmail);
            if (user.AssignedProjects.Count != 0)
            {
                foreach (Project p in user.AssignedProjects)
                    ColorCode.DefaultCode(p.ToString());
                return "";
            }
            else return "You don't have projects assigned to show now";
        }
        public string ViewAssignedTasks()
        {
            user = _database.GetUser(CurrentUserHandler.CurrentUserEmail);
            if (user.AssignedTasks.Count != 0)
            {
                foreach (Tasks t in user.AssignedTasks)
                    ColorCode.DefaultCode(t.ToString());
                return "";
            }
            else return "You don't have tasks assigned to show now";
        }
        public string ViewAssignedSubTasks()
        {
            user = _database.GetUser(CurrentUserHandler.CurrentUserEmail);
            if (user.AssignedSubTasks.Count != 0)
            {
                foreach (SubTask st in user.AssignedSubTasks)
                    ColorCode.DefaultCode(st.ToString());
                return "";
            }
            else return "You don't have subtasks assigned to show now";
        }
        public string ViewAssignedSubtaskofSubtask()
        {
            user = _database.GetUser(CurrentUserHandler.CurrentUserEmail);
            if (user.AssignedSubtaskofSubtask.Count != 0)
            {
                foreach (SmallSubTask sst in user.AssignedSubtaskofSubtask)
                    ColorCode.DefaultCode(sst.ToString());
                return "";
            }
            else return "You don't have subtask of subtask assigned to show now";
        }
        public string ViewAssignedIssues()
        {
            user = _database.GetUser(CurrentUserHandler.CurrentUserEmail);
            if (user.AssignedIssues.Count != 0)
            {
                foreach (Issue i in user.AssignedIssues)
                    ColorCode.DefaultCode(i.ToString());
                return "";
            }
            else return "You don't have issues assigned to show now";
        }
        public string ViewMyNotifications()
        {
            user = _database.GetUser(CurrentUserHandler.CurrentUserEmail);
            if (user.Notifications.Count == 0)
                return "You don't have any notification to show now";
            else
            {
                foreach (Notification notification in user.Notifications)
                    ColorCode.DefaultCode(notification.ToString());
                return "";
            }
        }
        public string ViewMyProfile()
        {
            user = _database.GetUser(CurrentUserHandler.CurrentUserEmail);
            return user.ToString();
        }
    }
}
