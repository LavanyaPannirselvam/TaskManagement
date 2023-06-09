﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApplication.Enumerations;
using TaskManagementApplication.Model;

namespace TaskManagementApplication.DataBase
{
    public class Database
    {
        private readonly Dictionary<string, User>? _allUsers;
        private readonly Dictionary<string, string>? _userCredentials;
        private readonly Dictionary<int, Project>? _allProjects;
        private readonly Dictionary<int, Tasks>? _allTasks;
        private readonly Dictionary<int, SubTask>? _allSubTasks;
        private readonly Dictionary<int, SmallSubTask>? _allSmallSubTasks;
        private readonly Dictionary<int, Issue>? _allIssues;
        private Database()//all the below are hardcoded data
        {
            User lavanya = new("Lavanya", "lava@gmail.com", Role.EMPLOYEE);
            User prithivi = new("Prithivi", "prithivi.8@gmail.com", Role.MANAGER);
            User deepika = new("Deepika", "deepi@gmail.com", Role.ADMIN);
            _allUsers = new Dictionary<string, User>
            {
                {"lava@gmail.com",lavanya },
                {"prithivi.8@gmail.com" ,prithivi},
                {"deepi@gmail.com",deepika }
            };
            _allProjects = new Dictionary<int, Project>
            {
                {1,new Project("P1","UI","Prithivi",StatusType.OPEN,PriorityType.MEDIUM,new DateOnly(2023,06,05),new DateOnly(2023,07,05)) },
                {2,new Project("P2","Backend","Prithivi",StatusType.OPEN, PriorityType.LOW,new DateOnly(2023,10,05),new DateOnly(2023,11,05)) }
            };
            _userCredentials = new Dictionary<string, string>
            {
                {"lava@gmail.com","123Q!a"},
                {"prithivi.8@gmail.com","234#Qas"},
                {"deepi@gmail.com","Ik!2w34"}
            };
            _allTasks = new Dictionary<int, Tasks>
            {
                {1,new Tasks("T1","Task 1","Prithivi",StatusType.OPEN,PriorityType.MEDIUM,new DateOnly(2023,08,05),new DateOnly(2023,09,05),2) }
            };
            _allSubTasks = new Dictionary<int, SubTask>
            {
                {1,new SubTask("ST1","SubTask 1","Prithivi",StatusType.OPEN,PriorityType.MEDIUM,new DateOnly(2023,08,05),new DateOnly(2023,09,05),2,1) }
            };
            _allSmallSubTasks = new Dictionary<int, SmallSubTask>
            {
                {1,new SmallSubTask("ST1","Subtask of Subtask 1","Prithivi",StatusType.OPEN,PriorityType.MEDIUM,new DateOnly(2023,08,05),new DateOnly(2023,09,05),2,1,1) }
            };
            _allIssues = new Dictionary<int, Issue>
            {
                {1,new Issue("T1","Task 1","Prithivi",StatusType.OPEN,PriorityType.MEDIUM,new DateOnly(2023,08,05),new DateOnly(2023,09,05),2) }
            };
            GetProject(1).AssignedUsers.Add(lavanya);
            GetTask(1).SubTasks.Add(GetSubTask(1));
            GetProject(2).CreatedTasks.Add(GetTask(1));
            GetProject(2).SubTasks.Add(GetSubTask(1));
            GetProject(2).Issues.Add(GetIssue(1));
            GetProject(2).AssignedUsers.Add(prithivi);
            GetTask(1).AssignedUsers.Add(lavanya);
            GetSubTask(1).AssignedUsers.Add(deepika);
            GetUser("prithivi.8@gmail.com").AssignedProjects.Add(GetProject(1));
            GetUser("prithivi.8@gmail.com").AssignedProjects.Add(GetProject(2));
            GetUser("lava@gmail.com").AssignedTasks.Add(GetTask(1));
            GetUser("lava@gmail.com").AssignedProjects.Add(GetProject(1));
            GetUser("deepi@gmail.com").AssignedSubTasks.Add(GetSubTask(1));
            GetProject(2).SubtaskofSubtask.Add(GetSmallSubTask(1));
            GetTask(1).SubtaskofSubtask.Add(GetSmallSubTask(1));
            GetSubTask(1).Subtask.Add(GetSmallSubTask(1));
            GetIssue(1).AssignedUsers.Add(prithivi);
        }
        private static readonly Database instance = new();
        public static Database GetInstance()
        {
            return instance;
        }
        //Projects section
        public Result AddProject(Project project)
        {
            if (_allProjects!.ContainsKey(project.Id))
                return Result.FAILURE;
            _allProjects.Add(project.Id, project);
            return Result.SUCCESS;
        }
        public Project GetProject(int projectId)
        {
            if (_allProjects!.ContainsKey(projectId))
                return _allProjects[projectId];
            else return null;
        }
        public Result DeleteProject(int projectId)
        {
            if (_allProjects!.ContainsKey(projectId))
            {
                if (GetProject(projectId).Status == StatusType.CLOSED)
                {
                    _allProjects.Remove(projectId);
                    return Result.SUCCESS;
                }
                else return Result.PARTIAL;
            }
            return Result.FAILURE;
        }
        public Dictionary<int, Project> ProjectsList()
        {
            return new(_allProjects!);
        }
        //Task section
        public Result AddTask(Tasks task)
        {
            if (_allTasks!.ContainsKey(task.Id))
                return Result.FAILURE;
            _allTasks.Add(task.Id, task);
            GetProject(task.ProjectId).CreatedTasks.Add(task);
            return Result.SUCCESS;
        }
        public Tasks GetTask(int taskId)
        {
            if (_allTasks!.ContainsKey(taskId))
                return _allTasks[taskId];
            else return null;
        }
        public Result DeleteTask(int taskId)
        {
            if (_allTasks!.ContainsKey(taskId))
            {
                if (GetTask(taskId).Status == StatusType.CLOSED)
                {
                    _allTasks.Remove(taskId);
                    return Result.SUCCESS;
                }
                else return Result.PARTIAL;
            }
            return Result.FAILURE;
        }
        public Dictionary<int, Tasks> TasksList()
        {
            return new(_allTasks!);
        }
        //Subtask section
        public Result AddSubTask(SubTask subTask)
        {
            if (_allSubTasks!.ContainsKey(subTask.Id))
                return Result.FAILURE;
            _allSubTasks!.Add(subTask.Id, subTask);
            GetProject(subTask.ProjectId).SubTasks.Add(subTask);
            GetTask(subTask.TaskId).SubTasks.Add(subTask);
            return Result.SUCCESS;
        }
        public SubTask GetSubTask(int subTaskId)
        {
            if (_allSubTasks!.ContainsKey(subTaskId))
                return _allSubTasks[subTaskId];
            else return null;
        }
        public Result DeleteSubTask(int smallSubTaskId)
        {
            if (_allSubTasks!.ContainsKey(smallSubTaskId))
            {
                if (GetTask(smallSubTaskId).Status == StatusType.CLOSED)
                {
                    _allSubTasks.Remove(smallSubTaskId);
                    return Result.SUCCESS;
                }
                else return Result.PARTIAL;
            }
            return Result.FAILURE;
        }
        public Dictionary<int, SubTask> SubTasksList()
        {
            return new(_allSubTasks!);
        }
        //Small subtask section
        public Result AddSmallSubTask(SmallSubTask smallsubTask)
        {
            if (_allSmallSubTasks!.ContainsKey(smallsubTask.Id))
                return Result.FAILURE;
            _allSmallSubTasks!.Add(smallsubTask.Id, smallsubTask);
            GetProject(smallsubTask.ProjectId).SubtaskofSubtask.Add(smallsubTask);
            GetTask(smallsubTask.TaskId).SubtaskofSubtask.Add(smallsubTask);
            GetSubTask(smallsubTask.SubTaskId).Subtask.Add(smallsubTask);
            return Result.SUCCESS;
        }
        public SmallSubTask GetSmallSubTask(int smallSubTaskId)
        {
            if (_allSmallSubTasks!.ContainsKey(smallSubTaskId))
                return _allSmallSubTasks[smallSubTaskId];
            else return null;
        }
        public Result DeleteSmallSubTask(int smallSubTaskId)
        {
            if (_allSmallSubTasks!.ContainsKey(smallSubTaskId))
            {
                if (GetSmallSubTask(smallSubTaskId).Status == StatusType.CLOSED)
                {
                    _allSmallSubTasks.Remove(smallSubTaskId);
                    return Result.SUCCESS;
                }
                else return Result.PARTIAL;
            }
            return Result.FAILURE;
        }
        public Dictionary<int, SmallSubTask> SmallSubTasksList()
        {
            return new(_allSmallSubTasks!);
        }
        //Issue section
        public Result AddIssue(Issue issue)
        {
            if (_allIssues!.ContainsKey(issue.Id))
                return Result.FAILURE;
            _allIssues.Add(issue.Id, issue);
            GetProject(issue.ProjectId).Issues.Add(issue);
            return Result.SUCCESS;
        }
        public Issue GetIssue(int issueId)
        {
            if (_allIssues!.ContainsKey(issueId))
                return _allIssues[issueId];
            else return null;
        }
        public Result DeleteIssue(int issueId)
        {
            if (_allIssues!.ContainsKey(issueId))
            {
                if (GetIssue(issueId).Status==StatusType.CLOSED)
                {
                    _allIssues.Remove(issueId);
                    return Result.SUCCESS;
                }
                else return Result.PARTIAL;
            }
            return Result.FAILURE;
        }
        public Dictionary<int, Issue> IssuesList()
        {
            return new(_allIssues!);
        }
        //User section
        public Result AddUser(User user, string password)
        {
            if (_allUsers!.ContainsKey(user.Email))
                return Result.FAILURE;
            else
            {
                _allUsers!.Add(user.Email, user);
                _userCredentials!.Add(user.Email, password);
                return Result.SUCCESS;
            }
        }
        public User GetUser(string email)
        {
            return _allUsers![email];
        }
        public User GetUser(int userId)
        {
            foreach (User user in _allUsers!.Values)
            {
                if(user.UserId == userId)
                    return user;
            }
            return null;
        }
        public Result DeleteUser(string email)
        {
            if (_allUsers!.ContainsKey(email))
            {
                User u = GetUser(email);
                if (u.AssignedProjects.Count == 0 && u.AssignedTasks.Count ==0 && u.AssignedSubTasks.Count ==0 && u.AssignedSubtaskofSubtask.Count ==0 && u.AssignedIssues.Count ==0)
                {
                    _allUsers!.Remove(email);
                    _userCredentials!.Remove(email);
                    return Result.SUCCESS;
                }
                else return Result.PARTIAL;
            }
            else return Result.FAILURE;
        }        
        public Dictionary<int, User> UsersList()
        {
            Dictionary<int,User> users = new Dictionary<int,User>();
            foreach(User user in _allUsers!.Values)
            {
                users.Add(user.UserId, user);   
            }
            return users;
        }

        public Result CheckUser(string loginId,string password)
        {
            if (_allUsers!.ContainsKey(loginId))
            {
                if (_userCredentials![loginId] == password)
                {
                    return Result.SUCCESS;
                }
                else return Result.PARTIAL;
            }
            else return Result.FAILURE;
        }
    }
}
