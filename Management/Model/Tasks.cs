using TaskManagementApplication.Enumerations;

namespace TaskManagementApplication.Model
{
    public class Tasks
    {
        private static int id = 1;

        public Tasks(string name, string desc, string createdBy, StatusType status, PriorityType type, DateOnly startDate, DateOnly endDate,int projectId)
        {
            Id = id++;
            ProjectId = projectId;
            Name = name;
            Desc = desc;
            CreatedBy = createdBy;
            Status = status;
            Priority = type;
            StartDate = startDate;
            EndDate = endDate;
            AssignedUsers = new List<string>();
            SubTasks = new List<SubTask>();
            SubtaskofSubtask = new List<SmallSubTask>();
        }
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string CreatedBy { get; set; }
        public StatusType Status { get; set; }
        public PriorityType Priority { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public ICollection<string> AssignedUsers { get; set; }
        public ICollection<SubTask> SubTasks { get; set; }
        public ICollection<SmallSubTask> SubtaskofSubtask { get; set; }

        public override string ToString()
        {
            string result = "\nName".PadRight(21) + " : " + this.Name + "\n" +
                "Id".PadRight(20) + " : " + this.Id + "\n" +
                "Project Id".PadRight(20) + " : " + this.ProjectId + "\n" +
                "Description".PadRight(20) + " : " + this.Desc + "\n" +
                "Created by".PadRight(20) + " : " + this.CreatedBy + "\n" +
                "Status".PadRight(20) + " : " + this.Status + "\n" +
                "Priority".PadRight(20) + " : " + this.Priority + "\n" +
                "Start date".PadRight(20) + " : " + this.StartDate + "\n" +
                "End date".PadRight(20) + " : " + this.EndDate + "\n" +
                "Assigned users".PadRight(20) + " : " + this.ShowUsers() + "\n" +
                "Subtasks".PadRight(20) + " : " + this.SubTasksList()+"\n" +
                "Subtask of Subtask".PadRight(20) + " : " + this.SubtaskofSubtasksList() + "\n";
            return result;
        }
        public string ShowUsers()
        {
            string result = "-";
            if (AssignedUsers.Count > 0)
            {
                result = "";
                foreach (string s in AssignedUsers)
                {
                    result += s + ", ";
                }
            }
            return result;
        }
        private string SubTasksList()
        {
            string result = "-";
            if (SubTasks.Count > 0)
            {
                result = "";
                foreach (SubTask st in SubTasks)
                {
                    result += st.Id + ", ";
                }
            }
            return result;
        }
        private string SubtaskofSubtasksList()
        {
            string result = "-";
            if (SubtaskofSubtask.Count > 0)
            {
                result = "";
                foreach (SmallSubTask sst in SubtaskofSubtask)
                {
                    result += sst.Id + ", ";
                }
            }
            return result;
        }

    }
}

