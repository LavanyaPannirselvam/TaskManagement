using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApplication.Enumerations;

namespace TaskManagementApplication.Model
{
    public class ApprovedUser : User
    {
        private static int userId = 1;

        public ApprovedUser(string name, string email, Role role,UserApprovalOptions userApprovalOptions) : base(name, email, role,UserApprovalOptions.APPROVED)
        {
            UserId = userId++;
            AssignedProjects = new List<Project>();
            AssignedTasks = new List<Tasks>();
        }
        public int UserId { get; set; }
        public ICollection<Project> AssignedProjects { get; set; }
        public ICollection<Tasks> AssignedTasks { get; set; }
        public override string ToString()
        {
            return string.Format($"Name : {this.Name} \nUserId : {this.UserId} \nEmail : {this.Email} \nRole : {this.Role}");
        }
    }
}
