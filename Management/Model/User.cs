using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApplication.Enumerations;

namespace TaskManagementApplication.Model
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
            AssignedProjects = new List<Project>();
        }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public ICollection<Project> AssignedProjects { get; set; }
        public override string ToString()
        {
            return string.Format($"Name : {this.Name} \n UserId : {this.UserId} \n Email : {this.Email} \n Role : {this.Role}");
        }
    }
}
