
namespace ConsoleApp1.Models
{
    public class Project
    {
        public int Id { get; set; }
      
        public string Name { get; set;}
        
        public string Desc { get; set;}
        
        public string CreatedBy { get; set; }
        
        public string Status { get; set; }
        
        public string Priority { get; set; }
        
        public DateOnly StartDate { get; set; }
        
        public DateOnly EndDate { get; set; }
        public ICollection<User> AssignedUsers { get; set; }

        public override string ToString(params object[] args)
        {
            return string.Format($"Name : {ProjectName} \n Id : {ProjectId} \n Description : {ProjectDesc} \n " +
                $"Created by : {ProjectCreatedBy} \n Status {ProjectStatus} \n Priority : {ProjectPriority} \n Start date : {ProjectStartDate} " +
                $"\n End date : {ProjectEndDate}");
        }

    }
}