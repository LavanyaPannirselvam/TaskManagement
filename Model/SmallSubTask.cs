using TaskManagementApplication.Enumerations;

namespace TaskManagementApplication.Model
{
    public class SmallSubTask
    {
        private static int id = 1;

        public SmallSubTask(string name, string desc, string createdBy, StatusType status, PriorityType type, DateOnly startDate, DateOnly endDate, int projectId, int taskId, int subTaskId)
        {
            Id = id++;
            ProjectId = projectId;
            TaskId = taskId;
            SubTaskId = subTaskId;
            Name = name;
            Desc = desc;
            CreatedBy = createdBy;
            Status = status;
            Priority = type;
            StartDate = startDate;
            EndDate = endDate;
            AssignedUsers = new List<int>();
        }
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int TaskId { get; set; }
        public int SubTaskId { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string CreatedBy { get; set; }
        public StatusType Status { get; set; }
        public PriorityType Priority { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public ICollection<int> AssignedUsers { get; set; }
        public override string ToString()
        {
            string result = "\nName".PadRight(16) + " : " + this.Name + "\n" +
                "Id".PadRight(15) + " : " + this.Id + "\n" +
                "Project Id".PadRight(15) + " : " + this.ProjectId + "\n" +
                "Task Id".PadRight(15) + " : " + this.TaskId + "\n" +
                "Subtask Id".PadRight(15) + " : " + this.SubTaskId + "\n" +
                "Description".PadRight(15) + " : " + this.Desc + "\n" +
                "Created by".PadRight(15) + " : " + this.CreatedBy + "\n" +
                "Status".PadRight(15) + " : " + this.Status + "\n" +
                "Priority".PadRight(15) + " : " + this.Priority + "\n" +
                "Start date".PadRight(15) + " : " + this.StartDate + "\n" +
                "End date".PadRight(15) + " : " + this.EndDate + "\n" +
                "Assigned users".PadRight(15) + " : " + this.ShowUsers() + "\n";
            return result;
        }
        public string ShowUsers()
        {
            string result = "-";
            if (AssignedUsers.Count > 0)
            {
                result = "";
                foreach (int s in AssignedUsers)
                {
                    result += s + ", ";
                }
            }
            return result;
        }
    }
}

