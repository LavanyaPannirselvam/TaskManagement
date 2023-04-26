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
    public class UserManagement : IView , ITemporaryUserView
    {
        private readonly Database _database = Database.GetInstance();

        public string ViewAssignedProjects()
        {
            if (_database.GetUser(_database.CurrentUser).AssignedProjects.Count != 0)
            {
                foreach (Project p in _database.GetUser(_database.CurrentUser).AssignedProjects)
                    ColorCode.DefaultCode(p.ToString());
                return "";
            }
            else return "You don't have projects assigned to show now";
        }
        public string ViewAssignedTasks()
        {
            if (_database.GetUser(_database.CurrentUser).AssignedTasks.Count != 0)
            {
                foreach (Tasks t in _database.GetUser(_database.CurrentUser).AssignedTasks)
                    ColorCode.DefaultCode(t.ToString());
                return "";
            }
            else return "You don't have tasks assigned to show now";
        }

        public string ViewNotifications()
        {
            if (_database.GetUser(_database.CurrentUser).Notifications.Count == 0)
                return "You don't have any notification to show now";
            else
            {
                foreach (Notification notification in _database.GetUser(_database.CurrentUser).Notifications)
                    ColorCode.DefaultCode(notification.ToStringWithoutId());
                return "";
            }
        }
        string ITemporaryUserView.ViewNotifications()
        {
            if (_database.GetUser(_database.CurrentTemporaryUser).Notifications.Count == 0)
                return "You don't have any notification to show now";
            else
            {
                foreach (Notification notification in _database.GetUser(_database.CurrentTemporaryUser).Notifications)
                    ColorCode.DefaultCode(notification.ToStringWithoutId());
                return "";
            }
        }
        public string ViewProfile()
        {
            return _database.GetUser(_database.CurrentUser).ToString();
        }
    }
}
