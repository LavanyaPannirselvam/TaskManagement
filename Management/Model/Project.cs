using ConsoleApp1.Enumeration;

namespace ConsoleApp1.Models
{
    public class Project
    {
        private static int id = 1;

        public Project(string name,string desc,int createdBy,StatusType status,PriorityType type,DateOnly startDate,DateOnly endDate)
        {
           Id = id++;
           Name = name; 
           Desc = desc;
           CreatedBy = createdBy;
           Status = status;
           Priority = type;
           StartDate = startDate;
           EndDate = endDate;

        }
        public int Id { get; set; }
      
        public string Name { get; set;}
        
        public string Desc { get; set;}
        
        public int CreatedBy { get; set; }
        
        public StatusType Status { get; set; }
        
        public PriorityType Priority { get; set; }
        
        public DateOnly StartDate { get; set; }
        
        public DateOnly EndDate { get; set; }
        public ICollection<User> AssignedUsers { get; set; }

        public override string ToString()
        {
            return string.Format($"Name : {this.Name} \n Id : {this.Id} \n Description : {this.Desc} \n " +
                $"Created by : {this.CreatedBy} \n Status {this.Status} \n Priority : {this.Priority} \n Start date : {this.StartDate} " +
                $"\n End date : {this.EndDate}");
        }

    }
}