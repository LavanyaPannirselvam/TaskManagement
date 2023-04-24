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
    public class UserManagement : IView
    {
        private readonly Database _database = Database.GetInstance();

        public string ViewAssignedProjects()
        {
            if (_database.GetUser(_database.CurrentUser).AssignedProjects.Count != 0)
            {
                foreach (Project p in _database.GetUser(_database.CurrentUser).AssignedProjects)
                    ColorCode.SuccessCode(p.ToString());
                return "";
            }
            else return "You don't have projects assigned to show now";
        }

        public string ViewProfile()
        {
            return _database.GetUser(_database.CurrentUser).ToString();
        }
    }
}
