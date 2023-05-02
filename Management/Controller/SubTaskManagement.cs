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
            if (_database.GetProject(_database.GetSubTask(subTaskId).ProjectId).AssignedUsers.Contains(_database.GetUser(_database.CurrentUser).Name))//check if the current user is already assigned to the project
            {
                if (_database.GetTask(_database.GetSubTask(subTaskId).TaskId).AssignedUsers.Contains(_database.GetUser(_database.CurrentUser).Name))//check if the current user is already assigned to the task
                {
                    if (!_database.GetSubTask(subTaskId).AssignedUsers.Contains(_database.GetUser(userId).Name))//check if the userId is not already assigned to the task 
                    {
                        _database.GetSubTask(subTaskId).AssignedUsers.Add(_database.GetUser(userId).Name);
                        _database.GetUser(userId).AssignedTasks.Add(_database.GetTask(subTaskId));
                        _database.GetUser(userId).Notifications.Add(new($"Subtask id {subTaskId} is assigned to you"));
                        return "Subtask is assigned to " + _database.GetUser(userId).Name + " successfully";
                    }
                    else return "Subtask is already assigned to the user";
                }
                else return "Given user is not available in the assigned users list of the task and thus can't assign a subtask";
            }
            else return "Given user is not available in the assigned users list of the project and thus can't assign a subtask";
        }

        public string DeassignUser(int subTaskId, int userId)
        {
            if (_database.GetProject(_database.GetSubTask(subTaskId).ProjectId).AssignedUsers.Contains(_database.GetUser(_database.CurrentUser).Name))//check if the current user is already assigned to the project
            {
                if (_database.GetTask(_database.GetSubTask(subTaskId).TaskId).AssignedUsers.Contains(_database.GetUser(_database.CurrentUser).Name))//check if the current user is already assigned to the task
                {
                    if (_database.GetSubTask(subTaskId).AssignedUsers.Contains(_database.GetUser(userId).Name))//check if the userId is not already assigned to the task 
                    {
                        _database.GetSubTask(subTaskId).AssignedUsers.Add(_database.GetUser(userId).Name);
                        _database.GetUser(userId).AssignedTasks.Add(_database.GetTask(subTaskId));
                        _database.GetUser(userId).Notifications.Add(new($"Subtask id {subTaskId} is deassigned from you"));
                        return "Subtask is deassigned from " + _database.GetUser(userId).Name + " successfully";
                    }
                    else return "Subtask is already deassigned from the user";
                }
                else return "Given user is not available in the assigned users list of the task and thus can't deassign a subtask";
            }
            else return "Given user is not available in the assigned users list of the project and thus can't deassign a subtask";
        }
        public string ChangePriority(int subTaskId, PriorityType priority)
        {
            if (_database.GetProject(_database.GetSubTask(subTaskId).ProjectId).AssignedUsers.Contains(_database.GetUser(_database.CurrentUser).Name))//check if the current user is already assigned to the project
            {
                if (_database.GetTask(_database.GetSubTask(subTaskId).TaskId).AssignedUsers.Contains(_database.GetUser(_database.CurrentUser).Name))//check if the current user is already assigned to the task
                {
                    if (_database.GetSubTask(subTaskId).Priority != priority)
                    {
                        _database.GetSubTask(subTaskId).Priority = priority;
                        return "Priority setted successfully";
                    }
                    else return "Task is already in this priority";
                }
                else return "You are not assigned to the task and thus you don't have access to change the priority of a subtask";
            }
            else return "You are not assigned to the project and thus you don't have access to change the priority of a task";
        }

        public string ChangeStatus(int subTaskId, StatusType status)
        {
            if (_database.GetProject(_database.GetSubTask(subTaskId).ProjectId).AssignedUsers.Contains(_database.GetUser(_database.CurrentUser).Name))//check if the current user is already assigned to the project
            {
                if (_database.GetTask(_database.GetSubTask(subTaskId).TaskId).AssignedUsers.Contains(_database.GetUser(_database.CurrentUser).Name))//check if the current user is already assigned to the task
                {
                    if (_database.GetSubTask(subTaskId).AssignedUsers.Contains(_database.GetUser(_database.CurrentUser).Name))
                    {
                        if (_database.GetSubTask(subTaskId).Status != status)
                        {
                            _database.GetSubTask(subTaskId).Status = status;
                            return "Status setted successfully";
                        }
                        else return "Task is already in this status";
                    }
                    else return "You are not assigned to the task and thus you can't change the status of the task";
                }
                else return "You are not assigned to the project and thus you don't have access to change the priority of a task";
            }
            else return "You are not assigned to the project and thus you don't have access to change the priority of a task";
        }

        public string Create(string name, string desc, StatusType status, PriorityType type, DateOnly startDate, DateOnly endDate, int projectId,int taskId)
        {
            if (_database.GetProject(projectId).AssignedUsers.Contains(_database.GetUser(_database.CurrentUser).Name))
            {
                if (_database.GetTask(taskId).AssignedUsers.Contains(_database.GetUser(_database.CurrentUser).Name))//check if the current user is already assigned to the task
                {
                    SubTask subTask = new(name, desc, _database.GetUser(_database.CurrentUser).Name, status, type, startDate, endDate, projectId, taskId);
                    if (_database.AddSubTask(subTask) == Result.SUCCESS)
                        return "Subtask created successfully. Subtask Id is  : " + subTask.Id;
                    else return "Subtask creation failed";
                }
                else return "You are not assigned to the task and thus you can't create a subtask for this";
            }
            else return "You are not assigned to the project and thus you can't create a task for this";
        }

        public string Remove(int subTaskId)
        {
            if (_database.GetProject(_database.GetSubTask(subTaskId).ProjectId).AssignedUsers.Contains(_database.GetUser(_database.CurrentUser).Name))
            {
                if (_database.GetTask(_database.GetSubTask(subTaskId).TaskId).AssignedUsers.Contains(_database.GetUser(_database.CurrentUser).Name))//check if the current user is already assigned to the task
                {
                    if (_database.DeleteTask(subTaskId) == Result.SUCCESS)
                        return "Task removed successfully";
                    else if (_database.DeleteTask(subTaskId) == Result.PARTIAL)
                        return "Task cannot be deleted as it is yet to complete";
                    else return "Task not available";
                }
                else return "You are not assigned to the task of this subtask and thus you can't delete this subtask";
            }
            else return "You are not assigned to the project of this task and thus you can't delete this task";
        }
        public string View(int subTaskId)
        {
            if (_database.GetSubTask(subTaskId) != null)
                return _database.GetSubTask(subTaskId).ToString();
            else return "Subtask not available";
        }
    }
}
