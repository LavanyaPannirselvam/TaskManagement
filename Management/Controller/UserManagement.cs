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
        private readonly Database _database= Database.GetInstance();
        
        public string ViewMyProjects()
        {
            if (_database.GetUser(Database.CurrentUser).AssignedProjects.Count != 0)
                {
                    foreach (Project p in _database.GetUser(Database.CurrentUser).AssignedProjects)
                        Console.WriteLine(p.ToString());
                    return "";
                }
                else return "You don't have projects assigned to show now";
            }
        }
    }

