﻿using TaskManagementApplication.DataBase;
using TaskManagementApplication.Enumerations;

namespace TaskManagementApplication.Model
{
    public class Project
    {
        private static int id = 1;
        public Project(string name, string desc, string createdBy, StatusType status, PriorityType type, DateOnly startDate, DateOnly endDate)
        {
            Id = id++;
            Name = name;
            Desc = desc;
            CreatedBy = createdBy;
            Status = status;
            Priority = type;
            StartDate = startDate;
            EndDate = endDate;
            AssignedUsers = new List<User>();
            CreatedTasks = new List<Tasks>();
            SubTasks = new List<SubTask>();
            SubtaskofSubtask = new List<SmallSubTask>();
            Issues = new List<Issue>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string CreatedBy { get; set; }
        public StatusType Status { get; set; }
        public PriorityType Priority { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public ICollection<User> AssignedUsers { get; set; }
        public ICollection<Tasks> CreatedTasks { get; set; }
        public ICollection<SubTask> SubTasks { get; set; }
        public ICollection<SmallSubTask> SubtaskofSubtask { get; set; }
        public ICollection<Issue> Issues { get; set; }

        public override string ToString()
        {
            string result = "\nName".PadRight(21) + " : " + this.Name + "\n" +
                "Id".PadRight(20) + " : " + this.Id + "\n" +
                "Description".PadRight(20) + " : " + this.Desc + "\n" +
                "Created by".PadRight(20) + " : " + this.CreatedBy + "\n" +
                "Status".PadRight(20) + " : " + this.Status + "\n" +
                "Priority".PadRight(20) + " : " + this.Priority + "\n" +
                "Start date".PadRight(20) + " : " + this.StartDate + "\n" +
                "End date".PadRight(20) + " : " + this.EndDate + "\n" +
                "Assigned users".PadRight(20) + " : " + this.ShowUsers() + "\n" +
                "Tasks".PadRight(20) + " : " + this.TasksList() + "\n" +
                "Subtasks".PadRight(20) + " : " + this.SubTasksList() + "\n" +
                "Subtask of Subtask".PadRight(20) + " : " + this.SubtaskofSubtasksList() + "\n" +
                "Issues".PadRight(20) + " : " + this.IssuesList() + "\n";
            return result;
        }
        private string ShowUsers()
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
        private string TasksList()
        {
            string result = "-";
            if (CreatedTasks.Count > 0)
            {
                result = "";
                foreach (Tasks t in CreatedTasks)
                {
                    result += t.Id + ", ";
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
        private string IssuesList()
        {
            string result = "-";
            if (Issues.Count > 0)
            {
                result = "";
                foreach (Issue i in Issues)
                {
                    result += i.Id + ", ";
                }
            }
            return result;
        }
    }
}
