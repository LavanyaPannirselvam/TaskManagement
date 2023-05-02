using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public override string ToString()
        {
            string result = "\nName".PadRight(15) + " : " + this.Name + "\n" +
                "Id".PadRight(15) + " : " + this.Id + "\n" +
                "Project Id".PadRight(15) + " : " + this.ProjectId + "\n" +
                "Description".PadRight(15) + " : " + this.Desc + "\n" +
                "Created by".PadRight(15) + " : " + this.CreatedBy + "\n" +
                "Status".PadRight(15) + " : " + this.Status + "\n" +
                "Priority".PadRight(15) + " : " + this.Priority + "\n" +
                "Start date".PadRight(15) + " : " + this.StartDate + "\n" +
                "End date".PadRight(15) + " : " + this.EndDate + "\n" +
                "Assigned users".PadRight(15) + " : " + this.ShowUsers() + "\n" +
                "Subtasks".PadRight(15) + " : " + this.SubTasksList()+"\n";
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

    }
}

