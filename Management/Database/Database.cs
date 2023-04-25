using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApplication.Enumerations;
using TaskManagementApplication.Model;

namespace TaskManagementApplication.DataBase

{
    public class Database
    {
        private static Dictionary<int, User>? _allUsers;
        private static Dictionary<int, string>? _userCredentials;
        private static Dictionary<int, Project>? _allProjects;
        private static Dictionary<int, Tasks>? _allTasks;
        private readonly string adminPassword = "Admin@123";//TODO

        private int currentUser;
        private Admin admin;
        private bool adminLogin;
        public int CurrentUser { get { return currentUser; } private set { currentUser = value; } }
        public Admin Admin { get { return admin; } }
        public string AdminPassword { get { return adminPassword; } }
        public bool AdminLoginStatus { get; private set; }
        private Database()
        {
            _allUsers = new Dictionary<int, User>
            {
                {1,new User("Lavanya","lava@gmail.com",Role.EMPLOYEE) },
                {2 ,new User("Prithivi","prithivi.8@gmail.com",Role.MANAGER) },
                {3,new User("Deepika","deepi@gmail.com",Role.LEAD) }
            };
            _allProjects = new Dictionary<int, Project>
            {
                {1,new Project("P1","UI",2,StatusType.OPEN,PriorityType.MEDIUM,new DateTime(20/05/2023),new DateTime(20/06/2023)) },
                {2,new Project("P2","Backend",2,StatusType.OPEN, PriorityType.LOW,new DateTime(01/06/2023),new DateTime(01/07/2023)) }
            };
            _userCredentials = new Dictionary<int, string>
            {
                {1,"123Q!a"},
                {2,"234#Qas"},
                {3,"Ik!2w34"}
            };
            _allTasks = new Dictionary<int, Tasks>
            {
                {1,new Tasks("T1","Task 1",2,StatusType.OPEN,PriorityType.MEDIUM,new DateTime(20/06/2023),new DateTime(20/07/2023),2) }
            };
            admin = new Admin("Admin", "admin@gmail.com");
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
        public bool IsProjectAvailable(int projectId)
        {
            if (_allProjects!.ContainsKey(projectId))
                return true;
            return false;
        }
        //Task section
        public Result AddTask(Tasks task)
        {
            if (_allTasks!.ContainsKey(task.Id))
                return Result.FAILURE;
            _allTasks.Add(task.Id, task);
            return Result.SUCCESS;
        }
        public Tasks GetTask(int taskId)
        {
            if (_allTasks!.ContainsKey(taskId))
                return _allTasks[taskId];
            else return null;
        }
        public bool IsTaskAvailable(int taskId)
        {
            if (_allTasks!.ContainsKey(taskId))
                return true;
            return false;
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
        //User section
        public Result AddUser(User user, string password)
        {
            if (_allUsers!.ContainsKey(user.UserId))
                return Result.FAILURE;
            _allUsers.Add(user.UserId, user);
            _userCredentials!.Add(user.UserId, password);
            return Result.SUCCESS;
        }

        public User GetUser(int userId)
        {
            return _allUsers![userId];
        }
        public bool IsUserAvailable(int userId)
        {
            if (_allUsers!.ContainsKey(userId))
                return true;
            return false;
        }
        public Result DeleteUser()
        {
            if (CurrentUser != 0)
            {
                if (GetUser(CurrentUser).AssignedProjects.Count == 0)
                {
                    _allUsers!.Remove(CurrentUser);
                    _userCredentials!.Remove(CurrentUser);
                    CurrentUser = 0;
                    return Result.SUCCESS;
                }
                else return Result.PARTIAL;
            }
            else return Result.FAILURE;
        }

        public Result CheckUser(int userId, string password)//pakka
        {
            if (_allUsers!.ContainsKey(userId))
            {
                if (_userCredentials![userId] == password)
                {
                    CurrentUser = userId;
                    return Result.SUCCESS;
                }
                else return Result.PARTIAL;
            }
            else return Result.FAILURE;
        }
        public Result LogOutUser()
        {
            if (CurrentUser != 0)
            {
                CurrentUser = 0;
                return Result.SUCCESS;
            }
            else
            {
                return Result.FAILURE;
            }
        }
        //Admin section
        public Result 
    }
    }
