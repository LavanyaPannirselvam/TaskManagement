using TaskManagementApplication.Enumerations;

namespace TaskManagementApplication.Model
{
    public class SubTask
    {
        private static int id = 1;

        public SubTask(string name, string desc, string createdBy, StatusType status, PriorityType type, DateOnly startDate, DateOnly endDate, int projectId, int taskId)
        {
            Id = id++;
            ProjectId = projectId;
            TaskId = taskId;
            Name = name;
            Desc = desc;
            CreatedBy = createdBy;
            Status = status;
            Priority = type;
            StartDate = startDate;
            EndDate = endDate;
            AssignedUsers = new List<User>();
            Subtask = new List<SmallSubTask>();
        }
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string CreatedBy { get; set; }
        public StatusType Status { get; set; }
        public PriorityType Priority { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public ICollection<User> AssignedUsers { get; set; }
        public ICollection<SmallSubTask> Subtask { get; set; }
        public override string ToString()
        {
            string result = "\nName".PadRight(20) + " : " + this.Name + "\n" +
                "Id".PadRight(19)+" : "+this.Id + "\n" +
                "Project Id".PadRight(19) + " : " + this.ProjectId + "\n" +
                "Task Id".PadRight(19) + " : " + this.TaskId + "\n" +
                "Description".PadRight(19) + " : " + this.Desc + "\n" +
                "Created by".PadRight(19) + " : " + this.CreatedBy + "\n" +
                "Status".PadRight(19) + " : " + this.Status + "\n" +
                "Priority".PadRight(19) + " : " + this.Priority + "\n" +
                "Start date".PadRight(19) + " : " + this.StartDate + "\n" +
                "End date".PadRight(19) + " : " + this.EndDate + "\n" +
                "Assigned users".PadRight(19) + " : " + this.ShowUsers() + "\n"+
                "Subtask of Subtask".PadRight(19) + " : " + this.SubTaskofSubTasksList() + "\n";
            return result;
        }
        public string ShowUsers()
        {
            string result = "-";
            if (AssignedUsers.Count > 0)
            {
                result = "";
                foreach (User user in AssignedUsers)
                {
                    result += user.Name + ", ";
                }
            }
            return result;
        }
        private string SubTaskofSubTasksList()
        {
            string result = "-";
            if (Subtask.Count > 0)
            {
                result = "";
                foreach (SmallSubTask sst in Subtask)
                {
                    result += sst.Id + ", ";
                }
            }
            return result;
        }
    }
}

