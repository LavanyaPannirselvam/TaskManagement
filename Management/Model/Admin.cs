using TaskManagementApplication.Enumerations;

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
            Role = Role.ADMIN;
            Notifications = new Dictionary<int,Notification>();
        }
        public string Name { get; set; }
        public int Id { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public Dictionary<int, Notification> Notifications { get; set; }
    }
}
