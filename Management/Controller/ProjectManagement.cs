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
        private Database database;
        

        public ProjectManagement(Database database) 
        {
            this.database = database;
        }

        public string AssignUser(int projectId, User user)
        {
            if (database.GetCurrentUser() != null)
            {
                if (database.GetCurrentUser().Role != Role.EMPLOYEE)
                {
                    if (database.GetProject(projectId) != null)
                    {
                        if (!database.GetProject(projectId).AssignedUsers.Contains(user))
                        {
                            database.GetProject(projectId).AssignedUsers.Add(user);
                            user.AssignedProjects.Add(database.GetProject(projectId));
                            return "Project is assigned to " + user.UserId + " successfully";
                        }
                        else return "Project is already assigned to the user";
                    }
                    else return "Project is not available";
                }
                else return "You don't have the access to assign a project";
            }
            else return "Login yourself to proceed yourself";
        }

        public string DeassignUser(int projectId, User user)
        {
            if (database.GetCurrentUser() != null)
            {
                if (database.GetCurrentUser().Role != Role.EMPLOYEE)
                {
                    if (database.GetProject(projectId) != null)
                    {
                        if (database.GetProject(projectId).AssignedUsers.Contains(user))
                        {
                            database.GetProject(projectId).AssignedUsers.Remove(user);
                            user.AssignedProjects.Remove(database.GetProject(projectId));
                            return "Project is deassigned from " + user.UserId + " successfully";
                        }
                        else return "Project is already deassigned to the user";
                    }
                    else return "Project is not available";
                }
                else return "You don't have the access to deassign a project";
            }
            else return "Login yourself to proceed yourself";
        }

        public string ChangePriority(int projectId, PriorityType priority)
        {
            if (database.GetCurrentUser() != null)
            {
                if (database.GetCurrentUser().Role != Role.EMPLOYEE)
                {
                    if (database.GetProject(projectId) != null)
                    {
                        if (database.GetProject(projectId).Priority != priority)
                        {
                            database.GetProject(projectId).Priority = priority;
                            return "Priority setted successfully";
                        }
                        else return "Project is already in this priority";
                    }
                    else return "Project is not available";
                }
                else return "You don't have the access to change the priority of a project";
            }
            else return "Login yourself to proceed yourself";
        }

        public string ChangeStatus(int projectId, StatusType status)
        {
            if (database.GetCurrentUser() != null)
            {
                if (database.GetProject(projectId) != null)
                {
                    if(database.GetProject(projectId).AssignedUsers.Contains(database.GetCurrentUser()))
                    { 
                        if (database.GetProject(projectId).Status != status)
                        {
                            database.GetProject(projectId).Status = status;
                            return "Status updated successfully";
                        }
                        else return "Project is already in this status";
                    }
                    else return "You are not assigned to the project and thus you cannot change the status of this project";
                }
                else return "Project is not available";
            }
            else return "Login yourself to proceed yourself";
        }

        public string CreateProject(string name, string desc, StatusType status, PriorityType type, DateOnly startDate, DateOnly endDate)
        {
            if (database.GetCurrentUser() != null)
            {
                if (database.GetCurrentUser().Role == Role.MANAGER)
                {
                    Project project = new Project(name, desc,database.GetCurrentUser().UserId, status, type, startDate, endDate);
                    if (database.AddProject(project) == Result.SUCCESS)
                        return "Project created successfully";
                    else return "Project creation failed";
                }
                else return "You don't have the access to create a project";
            }
            else return "Login yourself to proceed further";
        }

        public string RemoveProject(int projectId)
        {
            if(database.GetCurrentUser() != null)
            {
                if(database.GetCurrentUser().Role== Role.MANAGER)
                {
                    if (database.DeleteProject(projectId) == Result.SUCCESS)
                        return "Project removed successfully";
                    else if (database.DeleteProject(projectId) == Result.PARTIAL)
                        return "Project cannot be deleted as it is yet to complete";
                    else return "Project doesn't available";
                }
                else return "You don't have the access to create a project";
            }
            else return "Login yourself to proceed further";
        }

       
    }
}