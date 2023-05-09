using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApplication.Controller.Interface;
using TaskManagementApplication.DataBase;
using TaskManagementApplication.Enumerations;
using TaskManagementApplication.Model;

namespace TaskManagementApplication.Controller
{
    internal class SubTaskManagement : IActivityAssignment, IActivityModifications, IActivityManage
    {
        private readonly Database _database;
        private readonly DatabaseHandler _dbHandler;
        private readonly User currentUser;
        public SubTaskManagement()
        {
            _database = Database.GetInstance();
            _dbHandler = DatabaseHandler.GetInstance();
            currentUser = _database.GetUser(_dbHandler.CurrentUserEmail);
        }
        public string AssignUser(int subtaskId, int userId)
        {
            SubTask subtask = _database.GetSubTask(subtaskId);
            if (_database.GetTask(subtask.TaskId).AssignedUsers.Contains(currentUser))//only users who have been assigned to the task of the subtask can assign the subtask to users
            {
                User toBeAssignedUser = _database.GetUser(userId);
                if (!subtask.AssignedUsers.Contains(toBeAssignedUser))//check if the userId is not already assigned to the subtask 
                {
                    subtask.AssignedUsers.Add(toBeAssignedUser);
                    toBeAssignedUser.AssignedSubTasks.Add(subtask);
                    toBeAssignedUser.Notifications.Add(new($"Subtask id {subtaskId} is assigned to you"));
                    return "Subtask is assigned to " + toBeAssignedUser.Name + " successfully";
                }
                else return "Subtask is already assigned to the user";
            }
            else return "You are not assigned to the task of this subtask and thus you can't assign user to this subtask";
        }
        public string DeassignUser(int subtaskId, int userId)
        {
            SubTask subtask = _database.GetSubTask(subtaskId);
            if (_database.GetTask(subtask.TaskId).AssignedUsers.Contains(currentUser))//only users who have been assigned to the task of the subtask can assign the subtask to users
            {
                User toBeAssignedUser = _database.GetUser(userId);
                if (subtask.AssignedUsers.Contains(toBeAssignedUser))//check if the userId is not already assigned to the subtask 
                {
                    subtask.AssignedUsers.Remove(toBeAssignedUser);
                    toBeAssignedUser.AssignedSubTasks.Remove(subtask);
                    toBeAssignedUser.Notifications.Add(new($"Subtask id {subtaskId} is deassigned from you"));
                    return "Subtask is deassigned from " + toBeAssignedUser.Name + " successfully";
                }
                else return "Subtask is already deassigned from the user";
            }
            else return "You are not assigned to the task of this subtask and thus you can't deassign user from this subtask";
        }
        public string ChangePriorityOfActivity(int subtaskId, PriorityType priority)
        {
            SubTask subtask = _database.GetSubTask(subtaskId);
            if (_database.GetTask(subtask.TaskId).AssignedUsers.Contains(currentUser))//only users who have been assigned to the task of the subtask can change the priority of the subtask
            {
                if (subtask.Priority != priority)
                {
                    subtask.Priority = priority;
                    return "Priority setted successfully";
                }
                else return "Subask is already in this priority";
            }
            else return "You are not assigned to the task of this subtask and thus you don't have access to change the priority of subtask";
        }

        public string ChangeStatusOfActivity(int subtaskId, StatusType status)
        {
            SubTask subtask = _database.GetSubTask(subtaskId);
            if (subtask.AssignedUsers.Contains(currentUser))//only users who have been assigned to the subtask can change the status of the subtask
            {
                if (subtask.Status != status)
                {
                    subtask.Status = status;
                    return "Status setted successfully";
                }
                else return "Subask is already in this status";
            }
            else return "You are not assigned to the task of this subtask and thus you don't have access to change the status of subtask";
        }

        public string CreateActivity(string name, string desc, StatusType status, PriorityType priority, DateOnly startDate, DateOnly endDate, int projectId, int taskId, int sstid)
        {
            if (_database.GetTask(taskId).AssignedUsers.Contains(currentUser))//only users who have been assigned to the project of the task can assign the task to users
            {
                SubTask subtask = new(name, desc, currentUser.Name, status, priority, startDate, endDate, projectId, taskId);
                if (_database.AddSubTask(subtask) == Result.SUCCESS)
                    return "Subtask created successfully. Subtask Id is  : " + subtask.Id;
                else return "Subtask creation failed";
            }
            else return "You are not assigned to the task of this subtask and thus you can't create a subtask for this";
        }
        public string RemoveActivity(int subtaskId)
        {
            SubTask subtask = _database.GetSubTask(subtaskId);
            if (_database.GetTask(subtask.TaskId).AssignedUsers.Contains(currentUser))//only users who have been assigned to the project of the task can assign the task to users
            {
                Result result = _database.DeleteSubTask(subtaskId);
                if (result == Result.SUCCESS)
                    return "Subtask removed successfully";
                else if (result == Result.PARTIAL)
                    return "Subtask cannot be deleted as it is yet to complete";
                else return "Subtask not available";
            }
            else return "You are not assigned to the task of this subtask and thus you can't delete this subtask";
        }
        public string ViewActivity(int subtaskId)
        {
            if (_database.GetSubTask(subtaskId) != null)
                return _database.GetSubTask(subtaskId).ToString();
            else return "Subtask not available";
        }
    }
}
