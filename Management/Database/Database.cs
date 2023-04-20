using ConsoleApp1.Enumeration;
using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.DataBase
{
    public class Database
    {
        private readonly Dictionary<int, User> _allUsers;
        private readonly Dictionary<int, string> _userCredentials;
        private readonly Dictionary<int, Project> _allProjects;

        public static int CurrentUser { get; set; }      
        private Database()
        {
            _allUsers = new Dictionary<int, User>();
            _allProjects = new Dictionary<int, Project>();
            _userCredentials = new Dictionary<int, string>();
        }

        private static readonly Database? instance = null;
        public static Database GetInstance()
        {
            if (instance == null)
                return new Database();
            return instance;
        }
        //Projects section
        public Result AddProject(Project project)
        {
            if (_allProjects.ContainsKey(project.Id))
                return Result.FAILURE;
            _allProjects.Add(project.Id, project);
            return Result.SUCCESS;
        }

        public Project GetProject(int projectId)
        {
            if (_allProjects.ContainsKey(projectId))
                return _allProjects[projectId];
            else return null;
        }

        public Result DeleteProject(int projectId)
        {
            if (_allProjects.ContainsKey(projectId))
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
        //User section
        public Result AddUser(User user, string password)
        {
            if (_allUsers.ContainsKey(user.UserId))
                return Result.FAILURE;
            _allUsers.Add(user.UserId, user);
            _userCredentials.Add(user.UserId, password);
            return Result.SUCCESS;
        }

        public User GetUser(int userId)
        {
             return _allUsers[userId];
        }
        public bool IsUserAvailable(int userId)
        {
            if(_allUsers.ContainsKey(userId))
                return true;
            return false;
        }
        public Result DeleteUser()
        {
            if (CurrentUser != 0)
            {
                if (GetUser(CurrentUser).AssignedProjects.Count == 0)
                {
                    _allUsers.Remove(CurrentUser);
                    _userCredentials.Remove(CurrentUser);
                    CurrentUser = 0;
                    return Result.SUCCESS;
                }
                else return Result.PARTIAL;
            }
            else return Result.FAILURE;
        }

        internal Result CheckUser(int userId, string password)
        {
            if (_allUsers.ContainsKey(userId))
            {
                if (_userCredentials[userId] == password)
                {
                    CurrentUser=userId;
                    //Console.WriteLine(CurrentUser);
                    return Result.SUCCESS;
                }
                else return Result.PARTIAL;
            }
            else return Result.FAILURE;
        }
        public Result LogOut()
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
        /*internal string GetPassword(int userId)
        {
            if (userCredentials.ContainsKey(userId))
                return userCredentials[userId];
            else return null;
        }
        public void SetCurrentUser(User user){ this.CurrentUser = user; }

        public User GetCurrentUser() { return CurrentUser;}

    }*/
    }
}
