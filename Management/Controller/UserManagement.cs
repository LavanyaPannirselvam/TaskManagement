using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.DataBase;
using ConsoleApp1.Enumeration;
using ConsoleApp1.Models;

namespace ConsoleApp1.Controller
{
    public class UserManagement : IView
    {
        private Database database;

        public UserManagement(Database data)
        {
            this.database = data;
        }

        public string ViewMyProjects()
        {
            if (database.GetCurrentUser() != null)
            {
                if (database.GetCurrentUser().AssignedProjects.Count != 0)
                {
                    foreach (Project p in database.GetCurrentUser().AssignedProjects)
                        Console.WriteLine(p.ToString());
                    return "";
                }
                else return "You don't have projects assigned to show now";
            }
            else return "Login yourself to proceed further";
        }
        public string ViewProject(int projectId)
        {
            if(database.GetCurrentUser() != null)
            {
                if (database.GetProject(projectId) != null)
                    return database.GetProject(projectId).ToString();
                else return "Project not available";
            }
            else return "Login yourself to proceed further";
        }

    }
}
