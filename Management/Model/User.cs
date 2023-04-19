using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Enumeration;

namespace ConsoleApp1.Models
{
    public class User
    {
        private static int userId = 1; 
        public User(string name, string email, Role role)
        {
            UserId = userId++;
            Name = name;
            Email = email;
            Role = role;
        }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public string CurrentProject { get; set; }
        public ICollection<Project> AssignedProjects { get; set; }
        public override string ToString()
        {
            return string.Format($"Name : {this.Name} \n UserId : {this.UserId} \n Email : {this.Email} \n Role : {this.Role} \n CurrentProject : {this.CurrentProject}");
        }
    }
}
