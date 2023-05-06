using System.Linq;
using TaskManagementApplication.Controller.Interface;
using TaskManagementApplication.DataBase;
using TaskManagementApplication.Enumerations;
using TaskManagementApplication.Model;

namespace TaskManagementApplication.Controller
{
    public class ProjectManagement : IActivityManage, IActivityAssignment, IActivityModifications 
    {
        private readonly Database _database = Database.GetInstance();
        private readonly DatabaseHandler _dbHandler = new();
        public string AssignUser(int projectId, int userId)
        {
            User user = _database.GetUser(_dbHandler.CurrentUserEmail);
            if (user.Role == Role.MANAGER || user.Role == Role.ADMIN)
            {                
                Project project = _database.GetProject(projectId);
                if (!project.AssignedUsers.Contains(user.UserId))
                {
                    project.AssignedUsers.Add(user.UserId);
                    user.AssignedProjects.Add(project);
                    user.Notifications.Add(new($"Project id {projectId} is assigned to you"));
                    return "Project is assigned to " + user.Name + " successfully";
                }
                else return "Project is already assigned to the user";
            }
            else return "You don't have the access to assign user to this project";
        }
        public string DeassignUser(int projectId, int userId)
        {
            User user = _database.GetUser(_dbHandler.CurrentUserEmail);
            if (user.Role == Role.MANAGER || user.Role == Role.ADMIN)
            {
                Project project = _database.GetProject(projectId);
                if (project.AssignedUsers.Contains(user.UserId))
                {
                    project.AssignedUsers.Remove(user.UserId);
                    user.AssignedProjects.Remove(project);
                    user.Notifications.Add(new($"Project id {projectId} is deassigned from you"));
                    return "Project is deassigned from " + user.Name + " successfully";
                }
                else return "Project is already deassigned from the user";
            }
            else return "You don't have the access to deassign user from this project";
        }
        public string ChangePriorityOfActivity(int projectId, PriorityType priority)
        {
            User user = _database.GetUser(_dbHandler.CurrentUserEmail);
            if (user.Role == Role.MANAGER || user.Role == Role.ADMIN)
            {
                Project project = _database.GetProject(projectId);
                if (project.Priority != priority)
                    {
                    project.Priority = priority;
                        return "Priority setted successfully";
                    }
                    else return "Project is already in this priority";              
            }
            else return "You don't have the access to change the priority of a project";
        }
        public string ChangeStatusOfActivity(int projectId, StatusType status)
        {
            User user = _database.GetUser(_dbHandler.CurrentUserEmail);
            Project project = _database.GetProject(projectId);
            if (user.Role == Role.MANAGER || user.Role == Role.ADMIN || project.AssignedUsers.Contains(user.UserId))
            {
                if (project.Status != status)
                {
                    project.Status = status;
                    return "Status setted successfully";
                }
                else return "Project is already in this status";
            }
            else return "You don't have the access to change the status of a project";
        }
        public string CreateActivity(string name, string desc, StatusType status, PriorityType type, DateOnly startDate, DateOnly endDate, int tid,int stid,int sst)
        {
            User user = _database.GetUser(_dbHandler.CurrentUserEmail);
            if (user.Role == Role.MANAGER || user.Role == Role.ADMIN)
            {
                Project project = new(name, desc, user.Name, status, type, startDate, endDate);
                if (_database.AddProject(project) == Result.SUCCESS)
                    return "Project created successfully. Project Id is  : " + project.Id;
                else return "Project creation failed";
            }
            else return "You don't have the access to create a project";
        }
        public string RemoveActivity(int projectId)
        {
            if (_database.GetUser(_dbHandler.CurrentUserEmail).Role == Role.MANAGER)
            {
                Result result = _database.DeleteProject(projectId);
                if (result == Result.SUCCESS)
                    return "Project removed successfully";
                else if (result == Result.PARTIAL)
                    return "Project cannot be deleted as it is yet to complete";
                else return "Project doesn't available";
            }
            else return "You don't have the access to delete a project";
        }
        public string ViewActivity(int projectId)
        {
            if (_database.GetProject(projectId) != null)
                return _database.GetProject(projectId).ToString();
            else return "Project not available";
        }
    }
}

