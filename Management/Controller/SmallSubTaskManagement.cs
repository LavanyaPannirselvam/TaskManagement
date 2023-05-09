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
    public class SmallSubTaskManagement : IActivityAssignment, IActivityModifications, IActivityManage
    {
        private readonly Database _database;
        private readonly DatabaseHandler _dbHandler;
        private readonly User currentUser;
        public SmallSubTaskManagement()
        {
            _database = Database.GetInstance();
            _dbHandler = DatabaseHandler.GetInstance();
            currentUser = _database.GetUser(_dbHandler.CurrentUserEmail);
        }
        public string AssignUser(int smallSubtaskId, int userId)
        {
            SmallSubTask smallSubtask = _database.GetSmallSubTask(smallSubtaskId);
            if (_database.GetSubTask(smallSubtask.SubTaskId).AssignedUsers.Contains(currentUser))//only users who have been assigned to the subtask of the subtask of subtask can assign users
            {
                User toBeAssignedUser = _database.GetUser(userId);
                if (!smallSubtask.AssignedUsers.Contains(toBeAssignedUser))//check if the userId is not already assigned to the subtask 
                {
                    smallSubtask.AssignedUsers.Add(toBeAssignedUser);
                    toBeAssignedUser.AssignedSubtaskofSubtask.Add(smallSubtask);
                    toBeAssignedUser.Notifications.Add(new($"Subtask of subtask id {smallSubtaskId} is assigned to you"));
                    return "Subtask of subtask is assigned to " + toBeAssignedUser.Name + " successfully";
                }
                else return "Subtask of subtask is already assigned to the user";
            }
            else return "You are not assigned to the subtask of this subtask of subtask and thus you can't assign user to this subtask of subtask";
        }
        public string DeassignUser(int smallSubtaskId, int userId)
        {
            SmallSubTask smallSubtask = _database.GetSmallSubTask(smallSubtaskId);
            if (_database.GetSubTask(smallSubtask.SubTaskId).AssignedUsers.Contains(currentUser))//only users who have been assigned to the subtask of the subtask of subtask can assign users
            {
                User toBeAssignedUser = _database.GetUser(userId);
                if (smallSubtask.AssignedUsers.Contains(toBeAssignedUser))//check if the userId is not already assigned to the subtask 
                {
                    smallSubtask.AssignedUsers.Remove(toBeAssignedUser);
                    toBeAssignedUser.AssignedSubtaskofSubtask.Remove(smallSubtask);
                    toBeAssignedUser.Notifications.Add(new($"Subtask of subtask id {smallSubtaskId} is deassigned from you"));
                    return "Subtask of subtask is deassigned from " + toBeAssignedUser.Name + " successfully";
                }
                else return "Subtask of subtask is already deassigned from the user";
            }
            else return "You are not assigned to the subtask of this subtask of subtask and thus you can't deassign user from this subtask of subtask";

        }
        public string ChangePriorityOfActivity(int smallSubtaskId, PriorityType priority)
        {
            SmallSubTask smallSubtask = _database.GetSmallSubTask(smallSubtaskId);
            if (_database.GetSubTask(smallSubtask.SubTaskId).AssignedUsers.Contains(currentUser))//only users who have been assigned to the subtask of this subtask of subtask can change the priority of subtask of subtask
            {
                if (smallSubtask.Priority != priority)
                {
                    smallSubtask.Priority = priority;
                    return "Priority setted successfully";
                }
                else return "Subtask of subtask is already in this priority";
            }
            else return "You are not assigned to the subtask of this subtask of subtask and thus you don't have access to change the priority of this subtask of subtask";
        }

        public string ChangeStatusOfActivity(int smallSubtaskId, StatusType status)
        {
            SmallSubTask smallSubtask = _database.GetSmallSubTask(smallSubtaskId);
            if (smallSubtask.AssignedUsers.Contains(currentUser))//only users who have been assigned to the subtask of subtask can change the status of the subtask
            {
                if (smallSubtask.Status != status)
                {
                    smallSubtask.Status = status;
                    return "Status setted successfully";
                }
                else return "Subtask of subtask is already in this status";
            }
            else return "You are not assigned to the subtask of this subtask of subtask and thus you don't have access to change the status of this subtask of subtask";
        }

        public string CreateActivity(string name, string desc, StatusType status, PriorityType priority, DateOnly startDate, DateOnly endDate, int projectId, int taskId, int subtaskId)
        {
            if (_database.GetSubTask(subtaskId).AssignedUsers.Contains(currentUser))
            {
                SmallSubTask smallSubtask = new(name, desc, currentUser.Name, status, priority, startDate, endDate, projectId, taskId, subtaskId);
                if (_database.AddSmallSubTask(smallSubtask) == Result.SUCCESS)
                    return "Subtask of subtask is created successfully. Subtask of subtask Id is  : " + smallSubtask.Id;
                else return "Subtask of subtask creation failed";
            }
            else return "You are not assigned to the subtask of this subtask of subtask and thus you can't create a subtask for this";
        }
        public string RemoveActivity(int smallSubtaskId)
        {
            SmallSubTask smallSubtask = _database.GetSmallSubTask(smallSubtaskId);
            if (_database.GetSubTask(smallSubtask.SubTaskId).AssignedUsers.Contains(currentUser))//only users who have been assigned to the project of the task can assign the task to users
            {
                Result result = _database.DeleteSmallSubTask(smallSubtaskId);
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


