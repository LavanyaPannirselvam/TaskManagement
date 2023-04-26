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

        public Tasks(string name, string desc, int createdBy, StatusType status, PriorityType type, DateOnly startDate, DateOnly endDate,int projectId)
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
            AssignedUsers = new List<ApprovedUser>();
        }
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }

        public string Desc { get; set; }

        public int CreatedBy { get; set; }

        public StatusType Status { get; set; }

        public PriorityType Priority { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }
        public ICollection<ApprovedUser> AssignedUsers { get; set; }
        public override string ToString()
        {
            return string.Format($"Name : {this.Name}\nId : {this.Id}\nProject Id : {this.ProjectId}\nDescription : {this.Desc}\n " +
                $"Created by : {this.CreatedBy}\nStatus : {this.Status}\nPriority : {this.Priority} \nStart date : {this.StartDate} " +
                $"\nEnd date : {this.EndDate}\nAssigned users : " + ShowUsers());
        }
        public string ShowUsers()
        {
            string result = "";
            foreach (ApprovedUser s in AssignedUsers)
                result += s.UserId + " ";
            return result;
        }

    }
}

