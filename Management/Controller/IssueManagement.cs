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
        private readonly Database _database;
        private readonly DatabaseHandler _dbHandler;
        private readonly User currentUser;
        public IssueManagement()
        {
            _database = Database.GetInstance();
            _dbHandler = DatabaseHandler.GetInstance();
            currentUser = _database.GetUser(_dbHandler.CurrentUserEmail);
        }
        public string AssignUser(int issueId, int userId)
        {
            Issue issue = _database.GetIssue(issueId);
            if (_database.GetProject(issue.ProjectId).AssignedUsers.Contains(currentUser))//only users who have been assigned to the project of the task can assign the task to users
            {
                User toBeAssignedUser = _database.GetUser(userId);
                if (!issue.AssignedUsers.Contains(toBeAssignedUser))//check if the userId is not already assigned to the task 
                {
                    issue.AssignedUsers.Add(toBeAssignedUser);
                    toBeAssignedUser.AssignedIssues.Add(issue);
                    toBeAssignedUser.Notifications.Add(new($"Issue id {issueId} is assigned to you"));
                    return "Issue is assigned to " + toBeAssignedUser.Name + " successfully";
                }
                else return "Issue is already assigned to the user";
            }
            else return "You are not assigned to the project of this issue and thus you can't assign user to this issue";
        }
        public string DeassignUser(int issueId, int userId)
        {
            Issue issue = _database.GetIssue(issueId);
            if (_database.GetProject(issue.ProjectId).AssignedUsers.Contains(currentUser))//only users who have been assigned to the project of the task can deassign the task from users
            {
                User toBeAssignedUser = _database.GetUser(userId);
                if (issue.AssignedUsers.Contains(toBeAssignedUser))//check if the userId is not already assigned to the task 
                {
                    issue.AssignedUsers.Remove(toBeAssignedUser);
                    toBeAssignedUser.AssignedIssues.Remove(issue);
                    toBeAssignedUser.Notifications.Add(new($"Issue id {issueId} is deassigned from you"));
                    return "Issue is deassigned from " + toBeAssignedUser.Name + " successfully";
                }
                else return "Issue is already deassigned from the user";
            }
            else return "You are not assigned to the project of this issue and thus you can't deassign user from this issue";
        }
        public string ChangePriorityOfActivity(int issueId, PriorityType priority)
        {
            Issue issue = _database.GetIssue(issueId);
            if (_database.GetProject(issue.ProjectId).AssignedUsers.Contains(currentUser))//only users who have been assigned to the project of the task can change the priority of the task
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
            Issue issue = _database.GetIssue(issueId);
            if (issue.AssignedUsers.Contains(currentUser))//only users who have been assigned to the task can change the status of the task
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
            if (_database.GetProject(projectId).AssignedUsers.Contains(currentUser  ))//only users who have been assigned to the project of the task can assign the task to users
            {
                Issue issue = new(name, desc, currentUser.Name, status, priority, startDate, endDate, projectId);
                if (_database.AddIssue(issue) == Result.SUCCESS)
                    return "Issue created successfully. Issue Id is  : " + issue.Id;
                else return "Issue creation failed";
            }
            else return "You are not assigned to the project of this issue and thus you can't create a issue for this";
        }
        public string RemoveActivity(int issueId)
        {
            Issue issue = _database.GetIssue(issueId);
            if (_database.GetProject(issue.ProjectId).AssignedUsers.Contains(currentUser))//only users who have been assigned to the project of the task can assign the task to users
            {
                Result result = _database.DeleteIssue(issueId);
                if (result == Result.SUCCESS)
                    return "Issue removed successfully";
                else if (result == Result.PARTIAL)
                    return "Task cannot be deleted as it is yet to complete";
                else return "Issue not available";
            }
            else return "You are not assigned to the project of this issue and thus you can't delete this issue";
        }
        public string ViewActivity(int issueId)
        {
            if (_database.GetIssue(issueId) != null)
                return _database.GetIssue(issueId).ToString();
            else return "Issue not available";
        }
    }
}
