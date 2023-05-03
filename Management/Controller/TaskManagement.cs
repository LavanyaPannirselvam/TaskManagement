using TaskManagementApplication.Controller.Interface;
using TaskManagementApplication.DataBase;
using TaskManagementApplication.Enumerations;
using TaskManagementApplication.Model;

namespace TaskManagementApplication.Controller
{
    public class TaskManagement : IAssignment, IModify, IManage
    {
        private readonly Database _database = Database.GetInstance();

        public string AssignUser(int taskId, int userId)
        {
            if (_database.GetProject(_database.GetTask(taskId).ProjectId).AssignedUsers.Contains(_database.GetUser(_database.CurrentUser).Name))//check if the current user is already assigned to the project
            {
                if (!_database.GetTask(taskId).AssignedUsers.Contains(_database.GetUser(userId).Name))//check if the userId is not already assigned to the task 
                {
                    _database.GetTask(taskId).AssignedUsers.Add(_database.GetUser(userId).Name);
                    _database.GetUser(userId).AssignedTasks.Add(_database.GetTask(taskId));
                    _database.GetUser(userId).Notifications.Add(new($"Task id {taskId} is assigned to you"));
                    return "Task is assigned to " + _database.GetUser(userId).Name + " successfully";
                }
                else return "Task is already assigned to the user";
            }
            else return "You are not assigned to the project of this task and thus you can't assign a task";
        }


        public string DeassignUser(int taskId, int userId)
        {
            if (_database.GetProject(_database.GetTask(taskId).ProjectId).AssignedUsers.Contains(_database.GetUser(_database.CurrentUser).Name))//check if the current user is already assigned to the project
            {
                if (_database.GetTask(taskId).AssignedUsers.Contains(_database.GetUser(userId).Name))//check if the userId is not already assigned to the task 
                {
                    _database.GetTask(taskId).AssignedUsers.Remove(_database.GetUser(userId).Name);
                    _database.GetUser(userId).AssignedTasks.Remove(_database.GetTask(taskId));
                    _database.GetUser(userId).Notifications.Add(new($"Task id {taskId} is deassigned from you"));
                    return "Task is deassigned to " + _database.GetUser(userId).Name + " successfully";
                }
                else return "Task is already deassigned to the user";
            }
            else return "You are not assigned to the project of this task and thus you can't deassign a task";
        }
        public string ChangePriority(int taskId, PriorityType priority)
        {
            if (_database.GetProject(_database.GetTask(taskId).ProjectId).AssignedUsers.Contains(_database.GetUser(_database.CurrentUser).Name))//check if the current user is already assigned to the project
            {
                if (_database.GetTask(taskId).Priority != priority)
                {
                    _database.GetTask(taskId).Priority = priority;
                    return "Priority setted successfully";
                }
                else return "Task is already in this priority";
            }
            else return "You are not assigned to the project of this task and thus you don't have access to change the priority of a task";
        }

        public string ChangeStatus(int taskId, StatusType status)
        {
            if (_database.GetTask(taskId).AssignedUsers.Contains(_database.GetUser(_database.CurrentUser).Name))
                {
                    if (_database.GetTask(taskId).Status != status)
                    {
                        _database.GetTask(taskId).Status = status;
                        return "Status setted successfully";
                    }
                    else return "Task is already in this status";
                }
                else return "You are not assigned to the task and thus you can't change the status of the task";
            }

        public string Create(string name, string desc, StatusType status, PriorityType type, DateOnly startDate, DateOnly endDate, int projectId,int stid,int sstid)
        {
            if (_database.GetProject(projectId).AssignedUsers.Contains(_database.GetUser(_database.CurrentUser).Name))
            {
                Tasks task = new(name, desc, _database.GetUser(_database.CurrentUser).Name, status, type, startDate, endDate, projectId);
                if (_database.AddTask(task) == Result.SUCCESS)
                    return "Task created successfully. Task Id is  : " + task.Id;
                else return "Task creation failed";
            }
            else return "You are not assigned to the project of this task and thus you can't create a task for this";
        }
        public string Remove(int taskId)
        {
            if (_database.GetProject(_database.GetTask(taskId).ProjectId).AssignedUsers.Contains(_database.GetUser(_database.CurrentUser).Name))
            {
                if (_database.DeleteTask(taskId) == Result.SUCCESS)
                    return "Task removed successfully";
                else if (_database.DeleteTask(taskId) == Result.PARTIAL)
                    return "Task cannot be deleted as it is yet to complete";
                else return "Task not available";
            }
            else return "You are not assigned to the project of this task and thus you can't delete this task";
        }
        public string View(int taskId)
        {
            if (_database.GetTask(taskId) != null)
                return _database.GetTask(taskId).ToString();
            else return "Task not available";
        }
    }
}
//to assign a task, the user should be present in the assigned users list of the project
//anyone who is in the assigned users list of the project can create a task and assign it to a person 
