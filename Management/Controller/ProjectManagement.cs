using ConsoleApp1;
using ConsoleApp1.Enumeration;
using ConsoleApp1.Models;
using ConsoleApp1.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Controller
{
    public class ProjectManagement : IProjectManage, IAssignment, IModify
    {
        private readonly Database _database = Database.GetInstance();
        
        public string AssignUser(int projectId, int userId)
        {
            if (_database.IsUserAvailable(Database.CurrentUser))
            {
                if (_database.GetUser(Database.CurrentUser).Role != Role.EMPLOYEE)
                {
                    if (_database.GetProject(projectId) != null)
                    {
                        if (_database.IsUserAvailable(userId))
                        {
                            if (!_database.GetProject(projectId).AssignedUsers.Contains(_database.GetUser(userId)))
                            {
                                _database.GetProject(projectId).AssignedUsers.Add(_database.GetUser(userId));
                                _database.GetUser(userId).AssignedProjects.Add(_database.GetProject(projectId));
                                return "Project is assigned to " + userId + " successfully";
                            }
                            else return "Project is already assigned to the user";
                        }
                        else return "Given user is not available in the users list";
                    }
                    else return "Project is not available";
                }
                else return "You don't have the access to assign a project";
            }
            else return "Login yourself to proceed further";
        }

        public string DeassignUser(int projectId, int userId)
        {
            if (Database.CurrentUser != 0)
            {
                if (_database.GetUser(Database.CurrentUser).Role != Role.EMPLOYEE)
                {
                    if (_database.GetProject(projectId) != null)
                    {
                        if (_database.IsUserAvailable(userId))
                        {
                            if (_database.GetProject(projectId).AssignedUsers.Contains(_database.GetUser(userId)))
                            {
                                _database.GetProject(projectId).AssignedUsers.Remove(_database.GetUser(userId));
                                _database.GetUser(userId).AssignedProjects.Remove(_database.GetProject(projectId));
                                return "Project is deassigned from " + userId + " successfully";
                            }
                            else return "Project is already deassigned to the user";
                        }
                        else return "Given user is not available in the users list";
                    }
                    else return "Project is not available";
                }
                else return "You don't have the access to deassign a project";
            }
            else return "Login yourself to proceed further";
        }

        public string ChangePriority(int projectId, PriorityType priority)
        {
            if (Database.CurrentUser != 0)
            {
                if (_database.GetUser(Database.CurrentUser).Role != Role.EMPLOYEE)
                {
                    if (_database.GetProject(projectId) != null)
                    {
                        if (_database.GetProject(projectId).Priority != priority)
                        {
                            _database.GetProject(projectId).Priority = priority;
                            return "Priority setted successfully";
                        }
                        else return "Project is already in this priority";
                    }
                    else return "Project is not available";
                }
                else return "You don't have the access to change the priority of a project";
            }
            else return "Login yourself to proceed further";
        }

        public string ChangeStatus(int projectId, StatusType status)
        {
            if (Database.CurrentUser != 0)
            {
                if (_database.GetProject(projectId) != null)
                {
                    if(_database.GetProject(projectId).AssignedUsers.Contains(_database.GetUser(Database.CurrentUser)))
                    { 
                        if (_database.GetProject(projectId).Status != status)
                        {
                            _database.GetProject(projectId).Status = status;
                            return "Status updated successfully";
                        }
                        else return "Project is already in this status";
                    }
                    else return "You are not assigned to the project and thus you cannot change the status of this project";
                }
                else return "Project is not available";
            }
            else return "Login yourself to proceed further";
        }

        public string CreateProject(string name, string desc, StatusType status, PriorityType type, DateOnly startDate, DateOnly endDate)
        {
            if (Database.CurrentUser != 0)
            {
                if (_database.GetUser(Database.CurrentUser).Role == Role.MANAGER)
                {
                    Project project = new Project(name, desc,Database.CurrentUser, status, type, startDate, endDate);
                    if (_database.AddProject(project) == Result.SUCCESS)
                        return "Project created successfully";
                    else return "Project creation failed";
                }
                else return "You don't have the access to create a project";
            }
            else return "Login yourself to proceed further";
        }

        public string RemoveProject(int projectId)
        {
            if(Database.CurrentUser != 0)
            {
                if(_database.GetUser(Database.CurrentUser).Role== Role.MANAGER)
                {
                    if (_database.DeleteProject(projectId) == Result.SUCCESS)
                        return "Project removed successfully";
                    else if (_database.DeleteProject(projectId) == Result.PARTIAL)
                        return "Project cannot be deleted as it is yet to complete";
                    else return "Project doesn't available";
                }
                else return "You don't have the access to create a project";
            }
            else return "Login yourself to proceed further";
        }

        public string ViewProject(int projectId)
        {
            if (_database.GetProject(projectId) != null)
                    return _database.GetProject(projectId).ToString();
                else return "Project not available";
            }

    }
}