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
        
        public User(string name,string email,Role role,UserApprovalOptions approvalOptions)
        {
            Name = name;
            Email = email;
            Role = role;
            approvalOption = approvalOptions;
            Notifications = new List<Notification>();
        }
        private UserApprovalOptions approvalOption;
        public string Name { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public UserApprovalOptions ApprovalOptions { set { approvalOption = value; } }
        
        public ICollection<Notification> Notifications { get; set; }

        public override string ToString()
        {
            return string.Format($"Name : {this.Name} \nEmail : {this.Email} \nRole : {this.Role}");
        }
    }
}
/*public User(string name, string email, Role role)
        {
            UserId = userId++;
            Name = name;
            Email = email;
            Role = role;
            Notifications = new List<Notification>();
        }
*/