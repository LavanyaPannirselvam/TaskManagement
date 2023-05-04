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
    public class IssueManagement : IAssignment, IModify, IManage
    {
        private readonly Database _database = Database.GetInstance();

        public string AssignUser(int issueId, int userId)
        {
            if (_database.CurrentUser == _database.Admin.Email || _database.GetProject(_database.GetIssue(issueId).ProjectId).AssignedUsers.Contains(_database.GetUser(_database.CurrentUser).Name))//check if the current user is already assigned to the project
            {
                if (!_database.GetIssue(issueId).AssignedUsers.Contains(_database.GetUser(userId).Name))//check if the userId is not already assigned to the task 
                {
                    _database.GetIssue(issueId).AssignedUsers.Add(_database.GetUser(userId).Name);
                    _database.GetUser(userId).AssignedIssues.Add(_database.GetIssue(issueId));
                    _database.GetUser(userId).Notifications.Add(new($"Issue id {issueId} is assigned to you"));
                    return "Issue is assigned to " + _database.GetUser(userId).Name + " successfully";
                }
                else return "Issue is already assigned to the user";
            }
            else return "You are not assigned to the project of this issue and thus you can't assign a issue";
        }


        public string DeassignUser(int issueId, int userId)
        {
            if (_database.CurrentUser == _database.Admin.Email || _database.GetProject(_database.GetIssue(issueId).ProjectId).AssignedUsers.Contains(_database.GetUser(_database.CurrentUser).Name))//check if the current user is already assigned to the project
            {
                if (_database.GetIssue(issueId).AssignedUsers.Contains(_database.GetUser(userId).Name))//check if the userId is not already assigned to the task 
                {
                    _database.GetIssue(issueId).AssignedUsers.Remove(_database.GetUser(userId).Name);
                    _database.GetUser(userId).AssignedIssues.Remove(_database.GetIssue(issueId));
                    _database.GetUser(userId).Notifications.Add(new($"Issue id {issueId} is deassigned from you"));
                    return "Issue is deassigned from " + _database.GetUser(userId).Name + " successfully";
                }
                else return "Issue is already deassigned from the user";
            }
            else return "You are not assigned to the project of this issue and thus you can't deassign a issue";
        }
        public string ChangePriority(int issueId, PriorityType priority)
        {
            if (_database.CurrentUser == _database.Admin.Email || _database.GetProject(_database.GetIssue(issueId).ProjectId).AssignedUsers.Contains(_database.GetUser(_database.CurrentUser).Name))//check if the current user is already assigned to the project
            {
                if (_database.GetIssue(issueId).Priority != priority)
                {
                    _database.GetIssue(issueId).Priority = priority;
                    return "Priority setted successfully";
                }
                else return "Issue is already in this priority";
            }
            else return "You are not assigned to the project of this issue and thus you don't have access to change the priority of the issue";
        }

        public string ChangeStatus(int issueId, StatusType status)
        {
            string userName;
            if (_database.CurrentUser == _database.Admin.Email)
                userName = _database.Admin.Name;
            else userName = _database.GetUser(_database.CurrentUser).Name;
            if (_database.GetIssue(issueId).AssignedUsers.Contains(userName))
            {
                if (_database.GetIssue(issueId).Status != status)
                {
                    _database.GetIssue(issueId).Status = status;
                    return "Status setted successfully";
                }
                else return "Issue is already in this status";
            }
            else return "You are not assigned to the issue and thus you can't change the status of the issue";
        }

        public string Create(string name, string desc, StatusType status, PriorityType type, DateOnly startDate, DateOnly endDate, int projectId, int stid, int sstid)
        {
            if (_database.CurrentUser == _database.Admin.Email || _database.GetProject(projectId).AssignedUsers.Contains(_database.GetUser(_database.CurrentUser).Name))
            {
                string createdBy;
                if (_database.CurrentUser == _database.Admin.Email)
                    createdBy = _database.Admin.Name;
                else createdBy = _database.GetUser(_database.CurrentUser).Name;
                Issue issue = new(name, desc, createdBy, status, type, startDate, endDate, projectId);
                if (_database.AddIssue(issue) == Result.SUCCESS)
                    return "Issue created successfully. Issue Id is  : " + issue.Id;
                else return "Issue creation failed";
            }
            else return "You are not assigned to the project of this issue and thus you can't create a issue for this project";
        }
        public string Remove(int issueId)
        {
            if (_database.CurrentUser == _database.Admin.Email || _database.GetProject(_database.GetIssue(issueId).ProjectId).AssignedUsers.Contains(_database.GetUser(_database.CurrentUser).Name))
            {
                if (_database.DeleteIssue(issueId) == Result.SUCCESS)
                    return "Issue removed successfully";
                else if (_database.DeleteIssue(issueId) == Result.PARTIAL)
                    return "Issue cannot be deleted as it is yet to complete";
                else return "Issue not available";
            }
            else return "You are not assigned to the project of this issue and thus you can't delete this issue";
        }
        public string View(int issueId)
        {
            if (_database.GetIssue(issueId) != null)
                return _database.GetIssue(issueId).ToString();
            else return "Issue not available";
        }
    }
}

