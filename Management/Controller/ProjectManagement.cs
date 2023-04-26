using TaskManagementApplication.Controller.Interface;
using TaskManagementApplication.DataBase;
using TaskManagementApplication.Enumerations;
using TaskManagementApplication.Model;

namespace TaskManagementApplication.Controller
{
    public class ProjectManagement : IManage, IAssignment, IModify
    {
        private readonly Database _database = Database.GetInstance();
        public string AssignUser(int projectId, int userId)
        {
            if (_database.GetUser(_database.CurrentUser).Role != Role.EMPLOYEE)
            {
                    if (_database.IsProjectAvailable(projectId))
                    {
                        if (_database.IsUserAvailable(userId))
                        {
                            if (!_database.GetProject(projectId).AssignedUsers.Contains(_database.GetUser(userId)))
                            {
                                _database.GetProject(projectId).AssignedUsers.Add(_database.GetUser(userId));
                                _database.GetUser(userId).AssignedProjects.Add(_database.GetProject(projectId));
                                _database.GetUser(userId).Notifications.Add(new($"Project id {projectId} is assigned to you"));
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

        public string DeassignUser(int projectId, int userId)
        {
            if (_database.GetUser(_database.CurrentUser).Role != Role.EMPLOYEE)
                {
                    if (_database.IsProjectAvailable(projectId))
                    {
                        if (_database.IsUserAvailable(userId))
                        {
                            if (_database.GetProject(projectId).AssignedUsers.Contains(_database.GetUser(userId)))
                            {
                                _database.GetProject(projectId).AssignedUsers.Remove(_database.GetUser(userId));
                                _database.GetUser(userId).AssignedProjects.Remove(_database.GetProject(projectId));
                                _database.GetUser(userId).Notifications.Add(new($"Project id {projectId} is deassigned from you"));
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
        public string ChangePriority(int projectId, PriorityType priority)
        {
            if (_database.GetUser(_database.CurrentUser).Role != Role.EMPLOYEE)
                {
                    if (_database.IsProjectAvailable(projectId))
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
          

        public string ChangeStatus(int projectId, StatusType status)
        {
            if (_database.IsProjectAvailable(projectId))
                {
                    if (_database.GetProject(projectId).AssignedUsers.Contains(_database.GetUser(_database.CurrentUser)))
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
           
        public string Create(string name, string desc, StatusType status, PriorityType type, DateOnly startDate, DateOnly endDate, int id)
        {
            if (_database.GetUser(_database.CurrentUser).Role == Role.MANAGER)
                {
                    Project project = new(name, desc, _database.CurrentUser, status, type, startDate, endDate);
                    if (_database.AddProject(project) == Result.SUCCESS)
                        return "Project created successfully. Project Id is  : " + project.Id;
                    else return "Project creation failed";
                }
                else return "You don't have the access to create a project";
            }
           

        public string Remove(int projectId)
        {
            if (_database.GetUser(_database.CurrentUser).Role == Role.MANAGER)
                {
                    if (_database.DeleteProject(projectId) == Result.SUCCESS)
                        return "Project removed successfully";
                    else if (_database.DeleteProject(projectId) == Result.PARTIAL)
                        return "Project cannot be deleted as it is yet to complete";
                    else return "Project doesn't available";
                }
                else return "You don't have the access to delete a project";
            }
          
        public string View(int projectId)
        {
            if (_database.GetProject(projectId) != null)
                return _database.GetProject(projectId).ToString();
            else return "Project not available";
        }
    }
}
