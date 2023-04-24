using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementApplication.Model
{
    public class Admin
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Email { get; set; }
        public ICollection<User> RequestForSignIn { get; set; }
        public ICollection<User> RequestForSignOut { get; set; }

    }
}
