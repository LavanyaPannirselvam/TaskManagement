using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApplication.Controller;
using TaskManagementApplication.Enumerations;
using TaskManagementApplication.Utils;

namespace TaskManagementApplication.Presentation
{
    public class CollectProjectInput
    {
        private readonly ProjectManagement projectManager = new();
        private readonly TaskManagement taskManager = new();
        int id;
        int userId;
        PriorityType priority;
        StatusType status;
        string name;
        string desc;
        DateTime startDate;
        DateTime endDate;
        public string CollectAssignUserInput(int choice)
        {
            GetAndSetId();
            GetAndSetUserId();
            if(choice==1)
                return projectManager.AssignUser(id, userId);
            else return taskManager.AssignUser(id, userId);
        }

        public string CollectDeassignUserInput(int choice)
        {
            GetAndSetId();
            GetAndSetUserId();
            if(choice==1)
                return projectManager.DeassignUser(id, userId);
            else return taskManager.DeassignUser(id,userId);
        }
        public string CollectViewProjectInput(int choice)
        {
            GetAndSetId();
            if(choice==1)
                return projectManager.View(id);
            else return taskManager.View(id);
        }

        public string CollectChangePriorityInput(int choice)
        {
            GetAndSetId();
            GetAndSetPriority();
            if( choice==1)
                return projectManager.ChangePriority(id, priority);
            else return taskManager.ChangePriority(id,priority);
        }

        public string CollectChangeStatusInput(int choice)
        {
            GetAndSetId();
            GetAndSetStatus();
            if(choice==1)
                return projectManager.ChangeStatus(id, status);
            else return taskManager.ChangeStatus(id,status);
        }
        public string CollectCreateInput(int choice)
        {
            GetAndSetName();
            GetAndSetDescription();
            GetAndSetPriority();
            GetAndSetStatus();
            GetAndSetStartdate();
            GetAndSetEnddate();
            if (!CheckDates(startDate, endDate))
            {
                ColorCode.FailureCode("End date should be greater than start date\\nTry again entering the end date");
                GetAndSetEnddate();
            }
            if (choice == 1)
                return projectManager.Create(name, desc, status, priority, startDate, endDate, 0);
            else
            {
                GetAndSetId();
                return taskManager.Create(name, desc, status, priority, startDate, endDate, id);
            }
        }
        public string CollectDeleteInput(int choice)
        {
            GetAndSetId();
            if(choice==1)
                return projectManager.Remove(id);
            else return taskManager.Remove(id);
        }
        private void GetAndSetId()
        {
            ColorCode.GetInputCode("Enter id : ");
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                ColorCode.FailureCode("Id should be in number format and cannot be empty");
                GetAndSetId();
            }
        }
        private void GetAndSetUserId()
        {
            ColorCode.GetInputCode("Enter user id : ");
            if (!int.TryParse(Console.ReadLine(), out userId) || userId.ToString().Trim().Length == 0)
            {
                ColorCode.FailureCode("Project id should be in number format and cannot be empty");
                GetAndSetUserId();
            }
        }

        private void GetAndSetPriority()
        {
            ColorCode.GetInputCode("Choose priority : ");
            foreach (PriorityType priorityType in Enum.GetValues(typeof(PriorityType)))
                Console.WriteLine((int)priorityType + 1 + " . " + priorityType.ToString());
            int choice = Validation.getIntInRange(Enum.GetValues(typeof(PriorityType)).Length);
            PriorityType option = (PriorityType)(choice - 1);
            switch (option)
            {
                case PriorityType.HIGH: priority = option; break;
                case PriorityType.MEDIUM: priority = option; break;
                case PriorityType.LOW: priority = option; break;
                case PriorityType.NONE: priority = option; break;
            }
        }
        private void GetAndSetStatus()
        {
            ColorCode.GetInputCode("Choose status : ");
            foreach (StatusType status in Enum.GetValues(typeof(StatusType)))
                Console.WriteLine((int)status + 1 + " . " + status.ToString());
            int choice = Validation.getIntInRange(Enum.GetValues(typeof(StatusType)).Length);
            StatusType option = (StatusType)(choice - 1);
            switch (option)
            {
                case StatusType.OPEN: status = option; break;
                case StatusType.CLOSED: status = option; break;
                case StatusType.ONHOLD: status = option; break;
                case StatusType.INPROGRESS: status = option; break;
            }
        }

        private void GetAndSetName()
        {
            ColorCode.GetInputCode("Enter name : ");
            name = Console.ReadLine();
            if (string.IsNullOrEmpty(name) || name.Trim().Length == 0 || Validation.ContainsSpecialOrNumericCharacters(name))
            {
                ColorCode.FailureCode("Name should not be empty or should contain any special character");
                GetAndSetName();
            }
        }
        private void GetAndSetDescription()
        {
            ColorCode.GetInputCode("Enter description : ");
            desc = Console.ReadLine();
            if (string.IsNullOrEmpty(desc) || desc.Trim().Length == 0 || Validation.ContainsSpecialOrNumericCharacters(desc))
            {
                ColorCode.FailureCode("Description should not be empty or should contain any special character");
                GetAndSetName();
            }
        }
        private void GetAndSetStartdate()
        {
            string? date;
            ColorCode.GetInputCode("Enter start date (dd/mm/yyyy): ");
            date = Console.ReadLine();
            startDate = DateTime.ParseExact(date, "dd/mm/yyyy", CultureInfo.InvariantCulture);
        }
        private void GetAndSetEnddate()
        {
            string? date;
            ColorCode.GetInputCode("Enter end date (dd/mm/yyyy): ");
            date = Console.ReadLine();
            endDate = DateTime.ParseExact(date, "dd/mm/yyyy", CultureInfo.InvariantCulture);
        }
        private static bool CheckDates(DateTime startDate, DateTime endDate)
        {
            if (endDate.Day - startDate.Day >= 0 && endDate.Month - startDate.Month >= 0 && endDate.Year - startDate.Year >= 0)
                return true;
            return false;
        }
    }
}
