using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementApplication.Model
{
    public class Admin
    {
        private static int _id=1001;
        public Admin(string name,string email) 
        {
            Id = _id++;
            Name = name;
            Email = email;
            RequestForSignIn = new List<User>();
            RequestForSignOut = new List<User>();
            Notifications = new Dictionary<int,Notification>();
        }
        public string Name { get; set; }
        public int Id { get; set; }
        public string Email { get; set; }
        public ICollection<User> RequestForSignIn { get; set; }
        public ICollection<User> RequestForSignOut { get; set; }
        public Dictionary<int, Notification> Notifications { get; set; }
    }
}
