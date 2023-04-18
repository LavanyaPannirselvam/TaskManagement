using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Enumeration;
using ConsoleApp1.Models;

namespace ConsoleApp1.Models
{
    public class User
    {
        sealed static int userId = 1; 
        public User(string username, string name, string email, Role role)
        {
            UserId = userId++;
            Name = username;
            Email = email;
            Role = role;
        }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public string CurrentProject { get; set; }
        //public ICollection<Project> AssignedProjects { get; set; }
        public override string ToString()
        {
            return string.Format($"Name : {Name} \n UserId : {UserId} \n Email : {Email} \n Role : {Role} \n CurrentProject : {CurrentProject}");
        }
    }
}
