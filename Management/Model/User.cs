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
            Notifications = new List<Notification>();
            AssignedProjects = new List<Project>();
            AssignedTasks = new List<Tasks>();
            AssignedSubTasks = new List<SubTask>();
            AssignedSubtaskofSubtask = new List<SmallSubTask>();
        }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Project> AssignedProjects { get; set; }
        public ICollection<Tasks> AssignedTasks { get; set; }
        public ICollection<SubTask> AssignedSubTasks { get; set; }
        public ICollection<SmallSubTask> AssignedSubtaskofSubtask { get; set; }
        public override string ToString()
        {
            return string.Format($"Name : {this.Name} \nUserId : {this.UserId} \nEmail : {this.Email} \nRole : {this.Role}");
        }
        //todo 
    }
}
