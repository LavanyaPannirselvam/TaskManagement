﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            AssignedUsers = new List<string>();
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
        public ICollection<string> AssignedUsers { get; set; }
        public override string ToString()
        {
            string result = "\nName".PadRight(15) + " : " + this.Name + "\n" +
                "Id".PadRight(15)+" : "+this.Id + "\n" +
                "Project Id".PadRight(15) + " : " + this.ProjectId + "\n" +
                "Task Id".PadRight(15) + " : " + this.TaskId + "\n" +
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
                foreach (string s in AssignedUsers)
                {
                    result += s + ", ";
                }
            }
            return result;
        }
    }
}
