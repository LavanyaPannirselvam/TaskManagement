using ConsoleApp1.Controller;
using ConsoleApp1.Enumeration;
using ConsoleApp1.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.PresentationLayer
{
    public class CollectProjectInput
    {
        private readonly ProjectManagement projectManagement = new();
        int projectId;
        int userId;
        PriorityType priority;
        StatusType status;
        string name;
        string desc;
        DateOnly startDate;
        DateOnly endDate;
        public string CollectAssignUserInput()
        {
            GetAndSetProjectId();
            GetAndSetUserId();
            return projectManagement.AssignUser(projectId, userId);
        }

        public string CollectDeassignUserInput()
        {
            GetAndSetProjectId();
            GetAndSetUserId();
            return projectManagement.DeassignUser(projectId, userId);
        }
        public string CollectViewProjectInput()
        {
            GetAndSetProjectId();
            return projectManagement.ViewProject(projectId);
        }

        public string CollectChangePriorityInput() 
        {
            GetAndSetProjectId() ;
            GetAndSetPriority();
            return projectManagement.ChangePriority(projectId, priority);
        }

        public string CollectChangeStatusInput()
        {
            GetAndSetProjectId();
            GetAndSetStatus();
            return projectManagement.ChangePriority(projectId, priority);
        }
        public string CollectCreateProjectInput()
        {
            GetAndSetPriority() ;
            GetAndSetStatus();
            GetAndSetName();
            GetAndSetDescription();
            GetAndSetDates();
            return projectManagement.CreateProject(name, desc, status, priority, startDate, endDate);
        }

        public string CollectDeleteProjectInput()
        {
            GetAndSetProjectId();
            return projectManagement.RemoveProject(projectId);
        }
        private void GetAndSetProjectId()
        {
            Console.WriteLine("Enter project id : ");
            if (!int.TryParse(Console.ReadLine(), out projectId))
            {
                Console.WriteLine("Project id should be in number format and cannot be empty");
                GetAndSetProjectId();
            }
        }
        private void GetAndSetUserId()
        {
            Console.WriteLine("Enter user id : ");
            if (!int.TryParse(Console.ReadLine(), out userId) || userId.ToString().Trim().Length == 0)
            {
                Console.WriteLine("User id should be in number format and cannot be empty");
                GetAndSetUserId();
            }
        }

        private void GetAndSetPriority()
        {
            Console.WriteLine("Choose priority : ");
            foreach (PriorityType priorityType in Enum.GetValues(typeof(PriorityType)))
                Console.WriteLine((int)priorityType + 1 + " . " + priorityType.ToString());
            int choice = Validator.getIntInRange(Enum.GetValues(typeof(PriorityType)).Length);
            PriorityType option = (PriorityType)(choice - 1);
            switch (option)
            {
                case PriorityType.HIGH: priority = option; break;
                case PriorityType.MEDIUM: priority = option; break;
                case PriorityType.LOW: priority = option; break;
                case PriorityType.NONE: priority = option; break;
                default: Console.WriteLine("Invalid option selected"); GetAndSetPriority(); break;
            }
        }
        private void GetAndSetStatus()
        {
            Console.WriteLine("Choose status : ");
            foreach (StatusType status in Enum.GetValues(typeof(StatusType)))
                Console.WriteLine((int)status + 1 + " . " + status.ToString());
            int choice = Validator.getIntInRange(Enum.GetValues(typeof(StatusType)).Length);
            StatusType option = (StatusType)(choice - 1);
            switch (option)
            {
                case StatusType.OPEN:  status=option; break;
                case StatusType.CLOSED: status = option; break;
                case StatusType.ONHOLD: status = option; break;
                case StatusType.INPROGRESS: status = option; break;
                default: Console.WriteLine("Invalid option selected");GetAndSetStatus(); break;
            }
        }

        private void GetAndSetName()
        {
            Console.WriteLine("Enter project name : ");
            name = Console.ReadLine();
            if (string.IsNullOrEmpty(name) || name.Trim().Length == 0 || Validator.ContainsSpecialOrNumericCharacters(name))
            {
                Console.WriteLine("Name should not be empty or should contain any special character");
                GetAndSetName();
            }
        }
        private void GetAndSetDescription()
        {
            Console.WriteLine("Enter project description : ");
            desc = Console.ReadLine();
            if (string.IsNullOrEmpty(desc) || desc.Trim().Length == 0 || Validator.ContainsSpecialOrNumericCharacters(desc))
            {
                Console.WriteLine("Description should not be empty or should contain any special character");
                GetAndSetName();
            }
        }
        private void GetAndSetDates()
        {
            Console.WriteLine("Enter start date (mm/dd/yyyy) : ");
            startDate = DateOnly.Parse(Console.ReadLine());
            Console.WriteLine("Enter end date (mm/dd/yyyy) : ");
            endDate = DateOnly.Parse(Console.ReadLine());
            if(startDate > endDate)
            {
                Console.WriteLine("End date should be greater than start date");
                GetAndSetDates();
            }
            
        }
    }
}
