using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApplication.Controller.Interface;
using TaskManagementApplication.DataBase;
using TaskManagementApplication.Enumerations;
using TaskManagementApplication.Model;
using TaskManagementApplication.Utils;

namespace TaskManagementApplication.Controller
{
    public class AdminFunctions : IAdminOperations
    {
        private readonly Database _database = Database.GetInstance();

        public string ShowNotifications()
        {
            if (_database.Admin.Notifications.Count > 0)
            {
                foreach (Notification msg in _database.Admin.Notifications.Values)
                    ColorCode.DefaultCode(msg.ToString());
                return "";
            }
            else return "You don't have any notification to show";
        }
    }
}