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
        private sealed Dictionary<int, User> allUsers;
        private sealed Dictionary<int, string> userCredentials;
        private sealed Dictionary<int, Project> allProjects;
        private sealed User currentUser;
        private Database() {
            allUsers = new Dictionary<int, User>();
            allProjects = new Dictionary<int, Project>();
            userCredentials = new Dictionary<int, string>();
        }

        private static Database? instance = null;
        public static Database GetInstance() 
        {
            if(instance == null)
                return new Database();
            return instance;
        }
        
        public Result AddProject(Project project)
        {
            if (allProjects.ContainsKey(project.ProjectId))
                return Result.FAILURE;
            allProjects.Add(project.ProjectId, project);
            return Result.SUCCESS;
        }

        public string GetProject(int projectId)
        {
            if (allProjects.ContainsKey(projectId))
                return allProjects[projectId].ToString();
            else return "Project not available";
        }

        public Result DeleteProject(int projectId) 
        {
            if (allProjects.ContainsKey(projectId))
            {
                allProjects.Remove(projectId);
                return Result.SUCCESS;
            }
            return Result.FAILURE;             
        }

        public Result AddUser(User user)
        {
            if(allUsers.ContainsKey(user.UserId))
                return Result.FAILURE;
            allUsers.Add(user.UserId, user);
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
            if (currentUser != null)
            {
                allUsers.Remove(currentUser.UserId);
                currentUser = null;
                return Result.SUCCESS;
            }
            return Result.FAILURE;
        }
        public void SetCurrentUser(User user){ this.currentUser = user; }

        public User GetCurrentUser() { return currentUser;}

    }
}
