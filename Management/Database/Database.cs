using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApplication.Enumerations;
using TaskManagementApplication.Model;

namespace TaskManagementApplication.DataBase
{
    public class Database
    {
        private Dictionary<string, User>? _allUsers;
        private Dictionary<string, string>? _userCredentials;
        private Dictionary<int, Project>? _allProjects;
        private Dictionary<int, Tasks>? _allTasks;
        private Dictionary<int, SubTask>? _allSubTasks;
        private string _adminPassword = "Admin@123";

        private string currentUser;
        private readonly Admin admin;
        //private bool adminLogin;
        public string CurrentUser { get { return currentUser; } private set { currentUser = value; } }
        public Admin Admin { get { return admin; } }
        public string AdminPassword { get { return _adminPassword; } }
        public bool AdminLoginStatus { get; private set; }
        private Database()
        {
            _allUsers = new Dictionary<string, User>
            {
                {"lava@gmail.com",new User("Lavanya","lava@gmail.com",Role.EMPLOYEE) },
                {"prithivi.8@gmail.com" ,new User("Prithivi","prithivi.8@gmail.com",Role.MANAGER) },
                {"deepi@gmail.com",new User("Deepika","deepi@gmail.com",Role.LEAD) }
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
            admin = new Admin("Admin", "admin@gmail.com");
            currentUser = "";
            GetTask(1).SubTasks.Add(GetSubTask(1));
            GetProject(2).CreatedTasks.Add(GetTask(1));
            GetProject(2).SubTasks.Add(GetSubTask(1));
            GetProject(1).AssignedUsers.Add("Prithivi");
            GetTask(1).AssignedUsers.Add("Lavanya");
            GetSubTask(1).AssignedUsers.Add("Deepika");
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
                if (GetProject(projectId).AssignedUsers.Count == 0)
                {
                    _allProjects.Remove(projectId);
                    return Result.SUCCESS;
                }
                else return Result.PARTIAL;
            }
            return Result.FAILURE;
        }
        public Dictionary<int, string> ProjectsList()
        {
            Dictionary<int, string> projectsList = new();
            foreach (Project p in _allProjects!.Values)
            {
                projectsList.Add(p.Id, p.Name);
            }
            return projectsList;
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
                if (GetTask(taskId).AssignedUsers.Count == 0)
                {
                    _allTasks.Remove(taskId);
                    return Result.SUCCESS;
                }
                else return Result.PARTIAL;
            }
            return Result.FAILURE;
        }
        public Dictionary<int, string> TasksList()
        {
            Dictionary<int, string> tasksList = new();
            foreach (Tasks p in _allTasks!.Values)
            {
                tasksList.Add(p.Id, p.Name);
            }
            return tasksList;
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
        public Result DeleteSubTask(int subTaskId)
        {
            if (_allSubTasks!.ContainsKey(subTaskId))
            {
                if (GetTask(subTaskId).AssignedUsers.Count == 0)
                {
                    _allSubTasks.Remove(subTaskId);
                    return Result.SUCCESS;
                }
                else return Result.PARTIAL;
            }
            return Result.FAILURE;
        }
        public Dictionary<int, string> SubTasksList()
        {
            Dictionary<int, string> subTasksList = new();
            foreach (SubTask p in _allSubTasks!.Values)
            {
                subTasksList.Add(p.Id, p.Name);
            }
            return subTasksList;
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
            foreach (User u in _allUsers!.Values)
            {
                if (u.UserId == userId)
                    return u;
            }
            return null;
        }
        public Result DeleteUser(string email)
        {
            if (_allUsers!.ContainsKey(email))
            {
                if (GetUser(email).AssignedProjects.Count == 0)
                {
                    _allUsers!.Remove(email);
                    _userCredentials!.Remove(email);
                    return Result.SUCCESS;
                }
                else return Result.PARTIAL;
            }
            else return Result.FAILURE;
        }
        public Result CheckUser(string logId, string password)
        {
            if (_allUsers!.ContainsKey(logId))
            {
                if (_userCredentials![logId] == password)
                {
                    CurrentUser = logId;
                    return Result.SUCCESS;
                }
                else return Result.PARTIAL;
            }
            else return Result.FAILURE;
        }
        public Result LogOutUser()
        {
            if (CurrentUser != "")
            {
                CurrentUser = "";
                return Result.SUCCESS;
            }
            else
                return Result.FAILURE;
        }
        public Dictionary<int, string> UsersList()
        {
            Dictionary<int, string> usersList = new();
            foreach (User p in _allUsers!.Values)
            {
                usersList.Add(p.UserId, p.Name);
            }
            return usersList;
        }
        //Admin section
        public Result LogInAdmin(int userId, string password)
        {
            if (Admin.Id == userId)
            {
                if (AdminPassword == password)
                {
                    AdminLoginStatus = true;
                    return Result.SUCCESS;
                }
                else return Result.PARTIAL;
            }
            else return Result.FAILURE;
        }
        public Result LogOutAdmin()
        {
            if (AdminLoginStatus)
            {
                AdminLoginStatus = false;
                return Result.SUCCESS;
            }
            else return Result.FAILURE;
        }
    }
}
