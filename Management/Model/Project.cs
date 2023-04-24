﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApplication.Enumerations;

namespace TaskManagementApplication.Model
{
    public class Project
    {
        private static int id = 1;

        public Project(string name, string desc, int createdBy, StatusType status, PriorityType type, DateTime startDate, DateTime endDate)
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
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string Desc { get; set; }

        public int CreatedBy { get; set; }

        public StatusType Status { get; set; }

        public PriorityType Priority { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public ICollection<User> AssignedUsers { get; set; }
        public ICollection<Tasks> Tasks { get; set; }

        public override string ToString()
        {
            return string.Format($"Name : {this.Name}\nId : {this.Id}\nDescription : {this.Desc}\n " +
                $"Created by : {this.CreatedBy}\nStatus : {this.Status}\nPriority : {this.Priority} \nStart date : {this.StartDate} " +
                $"\nEnd date : {this.EndDate}\nAssigned users : " + ShowUsers());
        }
        public string ShowUsers()
        {
            string result = "";
            foreach (User s in AssignedUsers)
                result += s.UserId + " ";
            return result;
        }

    }
}
