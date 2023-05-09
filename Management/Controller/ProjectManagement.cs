using System.Linq;
using TaskManagementApplication.Controller.Interface;
using TaskManagementApplication.DataBase;
using TaskManagementApplication.Enumerations;
using TaskManagementApplication.Model;

namespace TaskManagementApplication.Controller
{
    public class ProjectManagement : IActivityManage, IActivityAssignment, IActivityModifications
    {
        private readonly Database _database;
        private readonly DatabaseHandler _dbHandler;
        private readonly User currentUser;
        public ProjectManagement()
        {
            _database = Database.GetInstance();
            _dbHandler = DatabaseHandler.GetInstance();
            currentUser = _database.GetUser(_dbHandler.CurrentUserEmail);
        }
        public string AssignUser(int projectId, int userId)
        {
            if (currentUser.Role == Role.MANAGER || currentUser.Role == Role.ADMIN)
            {
                Project project = _database.GetProject(projectId);
                User toBeAssignedUser = _database.GetUser(userId);
                if (!project.AssignedUsers.Contains(toBeAssignedUser))
                {
                    project.AssignedUsers.Add(toBeAssignedUser);
                    toBeAssignedUser.AssignedProjects.Add(project);
                    toBeAssignedUser.Notifications.Add(new($"Project id {projectId} is assigned to you"));
                    return "Project is assigned to " + toBeAssignedUser.Name + " successfully";
                }
                else return "Project is already assigned to the user";
            }
            else return "You don't have the access to assign user to this project";
        }
        public string DeassignUser(int projectId, int userId)
        {
            if (currentUser.Role == Role.MANAGER || currentUser.Role == Role.ADMIN)
            {
                User toBeAssignedUser = _database.GetUser(userId);
                Project project = _database.GetProject(projectId);
                if (project.AssignedUsers.Contains(toBeAssignedUser))
                {
                    project.AssignedUsers.Remove(toBeAssignedUser);
                    toBeAssignedUser.AssignedProjects.Remove(project);
                    toBeAssignedUser.Notifications.Add(new($"Project id {projectId} is deassigned from you"));
                    return "Project is deassigned from " + toBeAssignedUser.Name + " successfully";
                }
                else return "Project is already deassigned from the user";
            }
            else return "You don't have the access to deassign user from this project";
        }
        public string ChangePriorityOfActivity(int projectId, PriorityType priority)
        {
            if (currentUser.Role == Role.MANAGER || currentUser.Role == Role.ADMIN)
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
            Project project = _database.GetProject(projectId);
            if (currentUser.Role == Role.MANAGER || currentUser.Role == Role.ADMIN || project.AssignedUsers.Contains(currentUser))
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
        public string CreateActivity(string name, string desc, StatusType status, PriorityType type, DateOnly startDate, DateOnly endDate, int tid, int stid, int sst)
        {
            if (currentUser.Role == Role.MANAGER || currentUser.Role == Role.ADMIN)
            {
                Project project = new(name, desc, currentUser.Name, status, type, startDate, endDate);
                if (_database.AddProject(project) == Result.SUCCESS)
                    return "Project created successfully. Project Id is  : " + project.Id;
                else return "Project creation failed";
            }
            else return "You don't have the access to create a project";
        }
        public string RemoveActivity(int projectId)
        {
            if (currentUser.Role == Role.MANAGER || currentUser.Role == Role.ADMIN)
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

