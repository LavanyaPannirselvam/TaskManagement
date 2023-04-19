using ConsoleApp1.Enumeration;
using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.DataBase
{
    public class Database
    {
        private Dictionary<int, User> allUsers;
        private Dictionary<int, string> userCredentials;
        private Dictionary<int, Project> allProjects;
        private User? currentUser = null;
        private Database() {
            allUsers = new Dictionary<int, User>();
            allProjects = new Dictionary<int, Project>();
            userCredentials = new Dictionary<int, string>();
        }

        private static readonly Database? instance = null;
        public static Database GetInstance() 
        {
            if(instance == null)
                return new Database();
            return instance;
        }
        //Projects section
        public Result AddProject(Project project)
        {
            if (allProjects.ContainsKey(project.Id))
                return Result.FAILURE;
            allProjects.Add(project.Id, project);
            return Result.SUCCESS;
        }

        public Project GetProject(int projectId)
        {
            if (allProjects.ContainsKey(projectId))
                return allProjects[projectId];
            else return null;
        }

        public Result DeleteProject(int projectId) 
        {
            if (allProjects.ContainsKey(projectId))
            {
                if (GetProject(projectId).AssignedUsers.Count == 0)
                {
                    allProjects.Remove(projectId);
                    return Result.SUCCESS;
                }
                else return Result.PARTIAL;
            }
            return Result.FAILURE;             
        }
        //User section
        public Result AddUser(User user,string password)
        {
            if(allUsers.ContainsKey(user.UserId))
                return Result.FAILURE;
            allUsers.Add(user.UserId, user);
            userCredentials.Add(user.UserId, password);
            return Result.SUCCESS;
        }

        public User GetUser(int userId)
        {
            if (allUsers.ContainsKey(userId))
                return allUsers[userId];
            return null;
        }

        public Result DeleteUser()
        {
            if(currentUser != null)
            {
                if (currentUser.AssignedProjects.Count == 0)
                {
                    allUsers.Remove(currentUser.UserId);
                    userCredentials.Remove(currentUser.UserId);
                    currentUser = null;
                    return Result.SUCCESS;
                }
                else return Result.PARTIAL;
            }
            return Result.FAILURE;
        }

        internal Result CheckUser(int userId,string password)
        {
            if (allUsers.ContainsKey(userId))
            {
                if (userCredentials[userId] == password)
                    return Result.SUCCESS;
                else return Result.PARTIAL;
            }
            else return Result.FAILURE;
        }

        /*internal string GetPassword(int userId)
        {
            if (userCredentials.ContainsKey(userId))
                return userCredentials[userId];
            else return null;
        }*/
        public void SetCurrentUser(User user){ this.currentUser = user; }

        public User GetCurrentUser() { return currentUser;}

    }
}
