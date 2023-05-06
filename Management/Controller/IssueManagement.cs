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
    public class IssueManagement : IActivityAssignment, IActivityModifications, IActivityManage
    {
        private readonly Database _database = Database.GetInstance();

        private readonly DatabaseHandler _dbHandler = new();
        public string AssignUser(int issueId, int userId)
        {
            User user = _database.GetUser(_dbHandler.CurrentUserEmail);
            Issue issue = _database.GetIssue(issueId);
            if (_database.GetProject(issue.ProjectId).AssignedUsers.Contains(user.UserId))//only users who have been assigned to the project of the task can assign the task to users
            {
                if (!issue.AssignedUsers.Contains(user.UserId))//check if the userId is not already assigned to the task 
                {
                    issue.AssignedUsers.Add(user.UserId);
                    user.AssignedIssues.Add(issue);
                    user.Notifications.Add(new($"Issue id {issueId} is assigned to you"));
                    return "Issue is assigned to " + user.Name + " successfully";
                }
                else return "Issue is already assigned to the user";
            }
            else return "You are not assigned to the project of this issue and thus you can't assign user to this issue";
        }
        public string DeassignUser(int issueId, int userId)
        {
            User user = _database.GetUser(_dbHandler.CurrentUserEmail);
            Issue issue = _database.GetIssue(issueId);
            if (_database.GetProject(issue.ProjectId).AssignedUsers.Contains(user.UserId))//only users who have been assigned to the project of the task can deassign the task from users
            {
                if (issue.AssignedUsers.Contains(user.UserId))//check if the userId is not already assigned to the task 
                {
                    issue.AssignedUsers.Remove(user.UserId);
                    user.AssignedIssues.Remove(issue);
                    user.Notifications.Add(new($"Issue id {issueId} is deassigned from you"));
                    return "Issue is deassigned from " + user.Name + " successfully";
                }
                else return "Issue is already deassigned from the user";
            }
            else return "You are not assigned to the project of this issue and thus you can't deassign user from this issue";
        }
        public string ChangePriorityOfActivity(int issueId, PriorityType priority)
        {
            User user = _database.GetUser(_dbHandler.CurrentUserEmail);
            Issue issue = _database.GetIssue(issueId);
            if (_database.GetProject(issue.ProjectId).AssignedUsers.Contains(user.UserId))//only users who have been assigned to the project of the task can change the priority of the task
            {
                if (issue.Priority != priority)
                {
                    issue.Priority = priority;
                    return "Priority setted successfully";
                }
                else return "Issue is already in this priority";
            }
            else return "You are not assigned to the project of this issue and thus you don't have access to change the priority of this issue";
        }

        public string ChangeStatusOfActivity(int issueId, StatusType status)
        {
            User user = _database.GetUser(_dbHandler.CurrentUserEmail);
            Issue issue = _database.GetIssue(issueId);
            if (issue.AssignedUsers.Contains(user.UserId))//only users who have been assigned to the task can change the status of the task
            {
                if (issue.Status != status)
                {
                    issue.Status = status;
                    return "Status setted successfully";
                }
                else return "Issue is already in this status";
            }
            else return "You are not assigned to the issue and thus you can't change the status of this issue";
        }

        public string CreateActivity(string name, string desc, StatusType status, PriorityType priority, DateOnly startDate, DateOnly endDate, int projectId, int stid, int sstid)
        {
            User user = _database.GetUser(_dbHandler.CurrentUserEmail);
            if (_database.GetProject(projectId).AssignedUsers.Contains(user.UserId))//only users who have been assigned to the project of the task can assign the task to users
            {
                Issue issue = new(name, desc, user.Name, status, priority, startDate, endDate, projectId);
                if (_database.AddIssue(issue) == Result.SUCCESS)
                    return "Issue created successfully. Issue Id is  : " + issue.Id;
                else return "Issue creation failed";
            }
            else return "You are not assigned to the project of this issue and thus you can't create a issue for this";
        }
        public string RemoveActivity(int issueId)
        {
            User user = _database.GetUser(_dbHandler.CurrentUserEmail);
            Issue issue = _database.GetIssue(issueId);
            if (_database.GetProject(issue.ProjectId).AssignedUsers.Contains(user.UserId))//only users who have been assigned to the project of the task can assign the task to users
            {
                Result result = _database.DeleteIssue(issueId);
                if (result == Result.SUCCESS)
                    return "Task removed successfully";
                else if (result == Result.PARTIAL)
                    return "Task cannot be deleted as it is yet to complete";
                else return "Task not available";
            }
            else return "You are not assigned to the project of this task and thus you can't delete this task";
        }
        public string ViewActivity(int issueId)
        {
            if (_database.GetIssue(issueId) != null)
                return _database.GetIssue(issueId).ToString();
            else return "Issue not available";
        }
    }
}
