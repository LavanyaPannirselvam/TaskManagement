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
    public class SmallSubTaskManagement : IAssignment, IModify, IManage
    {
        private readonly Database _database = Database.GetInstance();

        public string AssignUser(int smallSubTaskId, int userId)
        {
            if (_database.GetSubTask(_database.GetSmallSubTask(smallSubTaskId).SubTaskId).AssignedUsers.Contains(_database.GetUser(_database.CurrentUser).Name))//check if the current user is already assigned to the task
            {
                if (!_database.GetSmallSubTask(smallSubTaskId).AssignedUsers.Contains(_database.GetUser(userId).Name))//check if the userId is not already assigned to the task 
                {
                    _database.GetSmallSubTask(smallSubTaskId).AssignedUsers.Add(_database.GetUser(userId).Name);
                    _database.GetUser(userId).AssignedSubtaskofSubtask.Add(_database.GetSmallSubTask(smallSubTaskId));
                    _database.GetUser(userId).Notifications.Add(new($"Subtask of subtask id {smallSubTaskId} is assigned to you"));
                    return "Subtask of subtask is assigned to " + _database.GetUser(userId).Name + " successfully";
                }
                else return "Subtask of subtask is already assigned to the user";
            }
            else return "You are not assigned to the subtask and thus you can't assign a subtask of subtask";
        }

        public string DeassignUser(int smallSubTaskId, int userId)
        {
            if (_database.GetSubTask(_database.GetSmallSubTask(smallSubTaskId).SubTaskId).AssignedUsers.Contains(_database.GetUser(_database.CurrentUser).Name))//check if the current user is already assigned to the task
            {
                if (_database.GetSmallSubTask(smallSubTaskId).AssignedUsers.Contains(_database.GetUser(userId).Name))//check if the userId is not already assigned to the task 
                {
                    _database.GetSmallSubTask(smallSubTaskId).AssignedUsers.Remove(_database.GetUser(userId).Name);
                    _database.GetUser(userId).AssignedSubtaskofSubtask.Remove(_database.GetSmallSubTask(smallSubTaskId));
                    _database.GetUser(userId).Notifications.Add(new($"Subtask of subtask id {smallSubTaskId} is deassigned from you"));
                    return "Subtask of subtask is deassigned from " + _database.GetUser(userId).Name + " successfully";
                }
                else return "Subtask of subtask is already deassigned from the user";
            }
            else return "You are not assigned to the subtask and thus you can't deassign a subtask of subtask";
        }

        public string ChangePriority(int subTaskId, PriorityType priority)
        {
            if (_database.GetSubTask(_database.GetSmallSubTask(subTaskId).TaskId).AssignedUsers.Contains(_database.GetUser(_database.CurrentUser).Name))//check if the current user is already assigned to the task
            {
                if (_database.GetSubTask(subTaskId).Priority != priority)
                {
                    _database.GetSubTask(subTaskId).Priority = priority;
                    return "Priority setted successfully";
                }
                else return "Task is already in this priority";
            }
            else return "You are not assigned to the subtask and thus you can't change the priority of a subtask of subtask";
        }

        public string ChangeStatus(int smallSubTaskId, StatusType status)
        {
            if (_database.GetSmallSubTask(smallSubTaskId).AssignedUsers.Contains(_database.GetUser(_database.CurrentUser).Name))
            {
                if (_database.GetSmallSubTask(smallSubTaskId).Status != status)
                {
                    _database.GetSmallSubTask(smallSubTaskId).Status = status;
                    return "Status setted successfully";
                }
                else return "Subtask of subtask is already in this status";
            }
            else return "You are not assigned to this subtask of subtask and thus you can't change the status of it";
        }

        public string Create(string name, string desc, StatusType status, PriorityType type, DateOnly startDate, DateOnly endDate, int projectId, int taskId,int subtaskId)
        {
            if (_database.GetSubTask(subtaskId).AssignedUsers.Contains(_database.GetUser(_database.CurrentUser).Name))//check if the current user is already assigned to the task
            {
                SmallSubTask subTask = new(name, desc, _database.GetUser(_database.CurrentUser).Name, status, type, startDate, endDate, projectId, taskId,subtaskId);
                if (_database.AddSmallSubTask(subTask) == Result.SUCCESS)
                    return "Subtask of subtask created successfully. Subtask Id is  : " + subTask.Id;
                else return "Subtask of subtask creation failed";
            }
            else return "You are not assigned to the subtask and thus you can't create a subtask of subtask for this subtask";
        }


        public string Remove(int smallSubTaskId)
        {
            if (_database.GetSubTask(_database.GetSmallSubTask(smallSubTaskId).SubTaskId).AssignedUsers.Contains(_database.GetUser(_database.CurrentUser).Name))//check if the current user is already assigned to the task
            {
                if (_database.DeleteSmallSubTask(smallSubTaskId) == Result.SUCCESS)
                    return "Subtask of subtask removed successfully";
                else if (_database.DeleteSmallSubTask(smallSubTaskId) == Result.PARTIAL)
                    return "Subtask of subtask cannot be deleted as it is yet to complete";
                else return "Subtask of subtask not available";
            }
            else return "You are not assigned to the subtask of this Subtask of subtask and thus you can't delete this Subtask of subtask";
        }

        public string View(int smallSubTaskId)
        {
            if (_database.GetSmallSubTask(smallSubTaskId) != null)
                return _database.GetSmallSubTask(smallSubTaskId).ToString();
            else return "Subtask of subtask not available";
        }
    }
}

