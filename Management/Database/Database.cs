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
        private static Dictionary<int, ApprovedUser>? _allUsers;
        private static Dictionary<int, string>? _userCredentials;
        private static Dictionary<int, Project>? _allProjects;
        private static Dictionary<int, Tasks>? _allTasks;
        private static Dictionary<string, User>? _temporaryUsers;
        private static Dictionary<string, string>? _temporaryUserCredentials;
        private static ICollection<string>? _approvedUsers;
        private readonly string adminPassword = "Admin@123";//TODO

        private int currentUser;
        private Admin admin;
        private readonly bool adminLogin;
        private string currentTemporaryUser;
        public int CurrentUser { get { return currentUser; } private set { currentUser = value; } }
        public string CurrentTemporaryUser { get { return currentTemporaryUser; } private set { currentTemporaryUser = value; } }
        public Admin Admin { get { return admin; } }
        public string AdminPassword { get { return adminPassword; } }
        public bool AdminLoginStatus { get; private set; }
        private Database()
        {
            _allUsers = new Dictionary<int, ApprovedUser>
            {
                {1,new ApprovedUser("Lavanya","lava@gmail.com",Role.EMPLOYEE,UserApprovalOptions.APPROVED) },
                {2 ,new ApprovedUser("Prithivi","prithivi.8@gmail.com",Role.MANAGER, UserApprovalOptions.APPROVED) },
                {3,new ApprovedUser("Deepika","deepi@gmail.com",Role.LEAD, UserApprovalOptions.APPROVED) }
            };
            _allProjects = new Dictionary<int, Project>
            {
                {1,new Project("P1","UI",2,StatusType.OPEN,PriorityType.MEDIUM,new DateOnly(2023,06,05),new DateOnly(2023,07,05)) },
                {2,new Project("P2","Backend",2,StatusType.OPEN, PriorityType.LOW,new DateOnly(2023,10,05),new DateOnly(2023,11,05)) }
            };
            _userCredentials = new Dictionary<int, string>
            {
                {1,"123Q!a"},
                {2,"234#Qas"},
                {3,"Ik!2w34"}
            };
            _allTasks = new Dictionary<int, Tasks>
            {
                {1,new Tasks("T1","Task 1",2,StatusType.OPEN,PriorityType.MEDIUM,new DateOnly(2023,08,05),new DateOnly(2023,09,05),2) }
            };
            admin = new Admin("Admin", "admin@gmail.com");
            _temporaryUsers = new Dictionary<string, User>();
            _temporaryUserCredentials = new Dictionary<string, string>();
            _approvedUsers = new List<string>();
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
        public Result AddUser(ApprovedUser user)
        {
            _approvedUsers!.Add(user.Email);
            _temporaryUsers!.Remove(user.Email);
            string password = _temporaryUserCredentials![user.Email];
            _temporaryUserCredentials!.Remove(user.Email);
            _allUsers!.Add(user.UserId, user);
            _userCredentials!.Add(user.UserId, password);
            return Result.SUCCESS;
        }
        public Result AddTemporaryUser(User user, string password)
        {
            _temporaryUsers!.Add(user.Email, user);
            _temporaryUserCredentials!.Add(user.Email, password);
            Notification notification = new("A new signup request from the mail id :" + user.Email);
            admin.Notifications.Add(notification.Id,notification);
            return Result.SUCCESS;
        }
        public ApprovedUser GetUser(int userId)
        {
            return _allUsers![userId];
        }
        public User GetUser(string email)
        {
            return _temporaryUsers![email];
        }
        public bool IsUserAvailable(int userId)
        {
            if (_allUsers!.ContainsKey(userId))
                return true;
            return false;
        }
        public bool IsUserAvailable(string email)
        {
            foreach(ApprovedUser u in _allUsers!.Values)
            {
                if(u.Email == email) 
                    return true;
            }
            return false;
        }
        public bool IsTemporaryUserAvailable(string email)
        {
            if(_temporaryUsers!.ContainsKey(email))
                return true;
            else return false;
        }
        public Result DeleteApprovedUser()
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

        public Result CheckApprovedUser(int userId, string password)
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
        public Result LogOutApprovedUser()
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

        public Result CheckTemporaryUser(string email,string password)
        {
            if(_temporaryUsers!.ContainsKey(email))
            {
                if (_temporaryUserCredentials![email] == password)
                {
                   CurrentTemporaryUser = email;
                    return Result.SUCCESS;
                }
                else return Result.FAILURE;
            }
            else if(_approvedUsers!.Contains(email))
            {
                CurrentTemporaryUser = email;
                return Result.PARTIAL;
            }
            else return Result.FAILURE;
        }
        public Result LogOutTemporaryUser()
        {
            if (CurrentTemporaryUser != null)
            {
                CurrentTemporaryUser = null ?? "";
                return Result.SUCCESS;
            }
            else
            {
                return Result.FAILURE;
            }
        }
        //Admin section
        public Result LogInAdmin(int userId,string password) 
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
