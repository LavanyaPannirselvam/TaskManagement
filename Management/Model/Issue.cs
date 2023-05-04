using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApplication.Enumerations;

namespace TaskManagementApplication.Model
{
    public class Issue
    {
        private static int id = 1;

        public Issue(string name, string desc, string createdBy, StatusType status, PriorityType type, DateOnly startDate, DateOnly endDate, int projectId)
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
                "Assigned users".PadRight(20) + " : " + this.ShowUsers() + "\n";               
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
    }
}
