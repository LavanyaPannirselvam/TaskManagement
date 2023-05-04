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
    internal class SubTaskManagement : IAssignment, IModify, IManage
    {
        private readonly Database _database = Database.GetInstance();

        public string AssignUser(int subTaskId, int userId)
        {
            if (_database.CurrentUser == _database.Admin.Email || _database.GetTask(_database.GetSubTask(subTaskId).TaskId).AssignedUsers.Contains(_database.GetUser(_database.CurrentUser).Name))//check if the current user is already assigned to the task
            {
                if (!_database.GetSubTask(subTaskId).AssignedUsers.Contains(_database.GetUser(userId).Name))//check if the userId is not already assigned to the task 
                {
                    _database.GetSubTask(subTaskId).AssignedUsers.Add(_database.GetUser(userId).Name);
                    _database.GetUser(userId).AssignedSubTasks.Add(_database.GetSubTask(subTaskId));
                    _database.GetUser(userId).Notifications.Add(new($"Subtask id {subTaskId} is assigned to you"));
                    return "Subtask is assigned to " + _database.GetUser(userId).Name + " successfully";
                }
                else return "Subtask is already assigned to the user";
            }
            else return "You are not assigned to the task and thus you can't assign a subtask";
        }

        public string DeassignUser(int subTaskId, int userId)
        {
            if (_database.CurrentUser == _database.Admin.Email || _database.GetTask(_database.GetSubTask(subTaskId).TaskId).AssignedUsers.Contains(_database.GetUser(_database.CurrentUser).Name))//check if the current user is already assigned to the task
            {
                if (_database.GetSubTask(subTaskId).AssignedUsers.Contains(_database.GetUser(userId).Name))//check if the userId is not already assigned to the task 
                {
                    _database.GetSubTask(subTaskId).AssignedUsers.Remove(_database.GetUser(userId).Name);
                    _database.GetUser(userId).AssignedSubTasks.Remove(_database.GetSubTask(subTaskId));
                    _database.GetUser(userId).Notifications.Add(new($"Subtask id {subTaskId} is deassigned from you"));
                    return "Subtask is deassigned from " + _database.GetUser(userId).Name + " successfully";
                }
                else return "Subtask is already deassigned from the user";
            }
            else return "You are not assigned to the task and thus you can't deassign a subtask";
        }

        public string ChangePriority(int subTaskId, PriorityType priority)
        {
            if (_database.CurrentUser == _database.Admin.Email || _database.GetTask(_database.GetSubTask(subTaskId).TaskId).AssignedUsers.Contains(_database.GetUser(_database.CurrentUser).Name))//check if the current user is already assigned to the task
            {
                if (_database.GetSubTask(subTaskId).Priority != priority)
                {
                    _database.GetSubTask(subTaskId).Priority = priority;
                    return "Priority setted successfully";
                }
                else return "Subtask is already in this priority";
            }
            else return "You are not assigned to the subtask and thus you can't change the priority of this subtask";
        }

        public string ChangeStatus(int subTaskId, StatusType status)
        {
            string userName;
            if (_database.CurrentUser == _database.Admin.Email)
                userName = _database.Admin.Name;
            else userName = _database.GetUser(_database.CurrentUser).Name;
            if (_database.CurrentUser == _database.Admin.Email || _database.GetSubTask(subTaskId).AssignedUsers.Contains(userName))
            {
                if (_database.GetSubTask(subTaskId).Status != status)
                {
                    _database.GetSubTask(subTaskId).Status = status;
                    return "Status setted successfully";
                }
                else return "Subtask is already in this status";
            }
            else return "You are not assigned to this subtask and thus you can't change the status of it";
        }


        public string Create(string name, string desc, StatusType status, PriorityType type, DateOnly startDate, DateOnly endDate, int projectId, int taskId, int subtaskId)
        {
            if (_database.CurrentUser == _database.Admin.Email || _database.GetTask(taskId).AssignedUsers.Contains(_database.GetUser(_database.CurrentUser).Name))//check if the current user is already assigned to the task
            {
                string createdBy;
                if (_database.CurrentUser == _database.Admin.Email)
                    createdBy = _database.Admin.Name;
                else createdBy = _database.GetUser(_database.CurrentUser).Name;
                SubTask subTask = new(name, desc, createdBy, status, type, startDate, endDate, projectId, taskId);
                if (_database.AddSubTask(subTask) == Result.SUCCESS)
                    return "Subtask created successfully. Subtask Id is  : " + subTask.Id;
                else return "Subtask creation failed";
            }
            else return "You are not assigned to the task and thus you can't create a subtask for this task";
        }
        public string Remove(int subTaskId)
        {
            if (_database.CurrentUser == _database.Admin.Email || _database.GetTask(_database.GetSubTask(subTaskId).TaskId).AssignedUsers.Contains(_database.GetUser(_database.CurrentUser).Name))//check if the current user is already assigned to the task
            {
                if (_database.DeleteSubTask(subTaskId) == Result.SUCCESS)
                    return "Subtask removed successfully";
                else if (_database.DeleteSubTask(subTaskId) == Result.PARTIAL)
                    return "Subtask cannot be deleted as it is yet to complete";
                else return "Subtask not available";
            }
            else return "You are not assigned to the task of this subtask and thus you can't delete this subtask";
        }
        public string View(int subTaskId)
        {
            if (_database.GetSubTask(subTaskId) != null)
                return _database.GetSubTask(subTaskId).ToString();
            else return "Subtask not available";
        }
    }
}
