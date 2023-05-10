using System.Threading.Tasks;
using TaskManagementApplication.Controller.Interface;
using TaskManagementApplication.DataBase;
using TaskManagementApplication.Enumerations;
using TaskManagementApplication.Model;

namespace TaskManagementApplication.Controller
{
    public class TaskManagement : IActivityAssignment, IActivityModifications, IActivityManage
    {
        private readonly Database _database;
        private readonly User _currentUser;
        public TaskManagement()
        {
            _database = Database.GetInstance();
            _currentUser = _database.GetUser(CurrentUserHandler.CurrentUserEmail);
        }
        public string AssignUser(int taskId, int userId)
        {
            Tasks task = _database.GetTask(taskId);
            if (_currentUser.Role == Role.ADMIN || _currentUser.Role == Role.MANAGER || _database.GetProject(task.ProjectId).AssignedUsers.Contains(_currentUser))//only users who have been assigned to the project of the task can assign the task to users
            {    
                User toBeAssignedUser = _database.GetUser(userId);
                if (!task.AssignedUsers.Contains(toBeAssignedUser))//check if the userId is not already assigned to the task 
                {
                    task.AssignedUsers.Add(toBeAssignedUser);
                    toBeAssignedUser.AssignedTasks.Add(task);
                    toBeAssignedUser.Notifications.Add(new($"Task id {taskId} is assigned to you"));
                    return "Task is assigned to " + toBeAssignedUser.Name + " successfully";
                }
                else return "Task is already assigned to the user";
            }
            else return "You are not assigned to the project of this task and thus you can't assign user to this task";
        }
        public string DeassignUser(int taskId, int userId)
        {
            Tasks task = _database.GetTask(taskId);
            if (_currentUser.Role == Role.ADMIN || _currentUser.Role == Role.MANAGER || _database.GetProject(task.ProjectId).AssignedUsers.Contains(_currentUser))//only users who have been assigned to the project of the task can deassign the task from users
            {
                User toBeAssigned = _database.GetUser(userId);
                if (task.AssignedUsers.Contains(toBeAssigned))//check if the userId is not already assigned to the task 
                {
                    task.AssignedUsers.Remove(toBeAssigned);
                    toBeAssigned.AssignedTasks.Remove(task);
                    toBeAssigned.Notifications.Add(new($"Task id {taskId} is deassigned from you"));
                    return "Task is deassigned from " + toBeAssigned.Name + " successfully";
                }
                else return "Task is already deassigned from the user";
            }
            else return "You are not assigned to the project of this task and thus you can't deassign user from this task";
        }
            public string ChangePriorityOfActivity(int taskId, PriorityType priority)
        {
            Tasks task = _database.GetTask(taskId);
            if (_currentUser.Role == Role.ADMIN || _currentUser.Role == Role.MANAGER || _database.GetProject(task.ProjectId).AssignedUsers.Contains(_currentUser))//only users who have been assigned to the project of the task can change the priority of the task
            {
                if (task.Priority != priority)
                {
                    task.Priority = priority;
                    return "Priority setted successfully";
                }
                else return "Task is already in this priority";
            }
            else return "You are not assigned to the project of this task and thus you don't have access to change the priority of a task";
        }

        public string ChangeStatusOfActivity(int taskId, StatusType status)
        {
            Tasks task = _database.GetTask(taskId);
            if (_currentUser.Role == Role.ADMIN || _currentUser.Role == Role.MANAGER || task.AssignedUsers.Contains(_currentUser))//only users who have been assigned to the task can change the status of the task
            {
                if (task.Status != status)
                {
                    task.Status = status;
                    return "Status setted successfully";
                }
                else return "Task is already in this status";
            }
            else return "You are not assigned to the task and thus you can't change the status of the task";
        }

        public string CreateActivity(string name, string desc, StatusType status, PriorityType priority, DateOnly startDate, DateOnly endDate, int projectId, int stid, int sstid)
        {
            if (_currentUser.Role == Role.ADMIN || _currentUser.Role == Role.MANAGER ||_database.GetProject(projectId).AssignedUsers.Contains(_currentUser))//only users who have been assigned to the project of the task can assign the task to users
            {
                Tasks task = new(name, desc,_currentUser.Name, status, priority, startDate, endDate, projectId);
                if (_database.AddTask(task) == Result.SUCCESS)
                    return "Task created successfully. Task Id is  : " + task.Id;
                else return "Task creation failed";
            }
            else return "You are not assigned to the project of this task and thus you can't create a task for this";
        }
        public string RemoveActivity(int taskId)
        {
            Tasks task = _database.GetTask(taskId);
            if (_currentUser.Role == Role.ADMIN || _currentUser.Role == Role.MANAGER || _database.GetProject(task.ProjectId).AssignedUsers.Contains(_currentUser))//only users who have been assigned to the project of the task can assign the task to users
            {
                Result result = _database.DeleteTask(taskId);
                if (result == Result.SUCCESS)
                    return "Task removed successfully";
                else if (result == Result.PARTIAL)
                    return "Task cannot be deleted as it is yet to complete";
                else return "Task not available";
            }
            else return "You are not assigned to the project of this task and thus you can't delete this task";
        }
        
    }
}
//to assign a task, the user should be present in the assigned users list of the project
//anyone who is in the assigned users list of the project can create a task and assign it to a person 
