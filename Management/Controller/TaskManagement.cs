using System.Threading.Tasks;
using TaskManagementApplication.Controller.Interface;
using TaskManagementApplication.DataBase;
using TaskManagementApplication.Enumerations;
using TaskManagementApplication.Model;

namespace TaskManagementApplication.Controller
{
    public class TaskManagement : IActivityAssignment, IActivityModifications, IActivityManage
    {
        private readonly Database _database = Database.GetInstance();
        private readonly DatabaseHandler _dbHandler = new();
        public string AssignUser(int taskId, int userId)
        {
            User user = _database.GetUser(_dbHandler.CurrentUserEmail);
            Tasks task = _database.GetTask(taskId);
            if (_database.GetProject(task.ProjectId).AssignedUsers.Contains(user.UserId))//only users who have been assigned to the project of the task can assign the task to users
            {            
                if (!task.AssignedUsers.Contains(user.UserId))//check if the userId is not already assigned to the task 
                {
                    task.AssignedUsers.Add(user.UserId);
                    user.AssignedTasks.Add(task);
                    user.Notifications.Add(new($"Task id {taskId} is assigned to you"));
                    return "Task is assigned to " + user.Name + " successfully";
                }
                else return "Task is already assigned to the user";
            }
            else return "You are not assigned to the project of this task and thus you can't assign user to this task";
        }
        public string DeassignUser(int taskId, int userId)
        {
            User user = _database.GetUser(_dbHandler.CurrentUserEmail);
            Tasks task = _database.GetTask(taskId);
            if (_database.GetProject(task.ProjectId).AssignedUsers.Contains(user.UserId))//only users who have been assigned to the project of the task can deassign the task from users
            {
                if (task.AssignedUsers.Contains(user.UserId))//check if the userId is not already assigned to the task 
                {
                    task.AssignedUsers.Remove(user.UserId);
                    user.AssignedTasks.Remove(task);
                    user.Notifications.Add(new($"Task id {taskId} is deassigned from you"));
                    return "Task is deassigned from " + user.Name + " successfully";
                }
                else return "Task is already deassigned from the user";
            }
            else return "You are not assigned to the project of this task and thus you can't deassign user from this task";
        }
            public string ChangePriorityOfActivity(int taskId, PriorityType priority)
        {
            User user = _database.GetUser(_dbHandler.CurrentUserEmail);
            Tasks task = _database.GetTask(taskId);
            if (_database.GetProject(task.ProjectId).AssignedUsers.Contains(user.UserId))//only users who have been assigned to the project of the task can change the priority of the task
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
            User user = _database.GetUser(_dbHandler.CurrentUserEmail);
            Tasks task = _database.GetTask(taskId);
            if (task.AssignedUsers.Contains(user.UserId))//only users who have been assigned to the task can change the status of the task
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
            User user = _database.GetUser(_dbHandler.CurrentUserEmail);
            if (_database.GetProject(projectId).AssignedUsers.Contains(user.UserId))//only users who have been assigned to the project of the task can assign the task to users
            {
                Tasks task = new(name, desc, user.Name, status, priority, startDate, endDate, projectId);
                if (_database.AddTask(task) == Result.SUCCESS)
                    return "Task created successfully. Task Id is  : " + task.Id;
                else return "Task creation failed";
            }
            else return "You are not assigned to the project of this task and thus you can't create a task for this";
        }
        public string RemoveActivity(int taskId)
        {
            User user = _database.GetUser(_dbHandler.CurrentUserEmail);
            Tasks task = _database.GetTask(taskId);
            if (_database.GetProject(task.ProjectId).AssignedUsers.Contains(user.UserId))//only users who have been assigned to the project of the task can assign the task to users
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
        public string ViewActivity(int taskId)
        {
            if (_database.GetTask(taskId) != null)
                return _database.GetTask(taskId).ToString();
            else return "Task not available";
        }
    }
}
//to assign a task, the user should be present in the assigned users list of the project
//anyone who is in the assigned users list of the project can create a task and assign it to a person 
