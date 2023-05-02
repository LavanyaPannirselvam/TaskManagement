using System.Collections.Generic;
using System.Globalization;
using TaskManagementApplication.Controller;
using TaskManagementApplication.Enumerations;
using TaskManagementApplication.Utils;

namespace TaskManagementApplication.Presentation
{
    public class CollectProjectInput
    {
        private readonly ProjectManagement _projectManager = new();
        private readonly TaskManagement _taskManager = new();
        private readonly SubTaskManagement _subTaskManager = new();
        private readonly Lists _lists = new();
        int id;
        int userId;
        PriorityType priority;
        StatusType status;
        string? name;
        string? desc;
        DateOnly startDate;
        DateOnly endDate;
        public string CollectAssignUserInput(int choice)
        {
            GetAndSetActivityId(choice);
            GetAndSetUserId();
            if(choice==1)
                return _projectManager.AssignUser(id, userId);
            else if (choice ==2)
                return _taskManager.AssignUser(id, userId);
            else return _subTaskManager.AssignUser(id, userId);
        }
        public string CollectDeassignUserInput(int choice)
        {
            GetAndSetActivityId(choice);
            GetAndSetUserId();
            if (choice == 1)
                return _projectManager.DeassignUser(id, userId);
            else if (choice == 2)
                return _taskManager.DeassignUser(id, userId);
            else return _subTaskManager.DeassignUser(id, userId);
        }
        public string CollectViewProjectInput(int choice)
        {
            GetAndSetActivityId(choice); 
            if (choice==1)
                return _projectManager.View(id);
            else if (choice == 2) 
                return _taskManager.View(id);
            else return _subTaskManager.View(id);
        }
        public string CollectChangePriorityInput(int choice)
        {
            GetAndSetActivityId(choice);
            GetAndSetPriority(choice);
            if( choice==1)
                return _projectManager.ChangePriority(id, priority);
            else if (choice == 2)
                return _taskManager.ChangePriority(id, priority);
            else
                return _subTaskManager.ChangePriority(id, priority);
        }
        public string CollectChangeStatusInput(int choice)
        {
            GetAndSetActivityId(choice);
            GetAndSetStatus(choice);
            if(choice==1)
                return _projectManager.ChangeStatus(id, status);
            else if (choice == 2)
                return _taskManager.ChangeStatus(id, status);
            else
                return _subTaskManager.ChangeStatus(id, status);
        }
        public string CollectCreateInput(int choice)
        {
            GetAndSetName(choice);
            GetAndSetDescription(choice);
            GetAndSetPriority(choice);
            GetAndSetStatus(choice);
            GetAndSetStartdate();
            GetAndSetEnddate();
            if (CheckDates(startDate, endDate))
            {
                ColorCode.FailureCode("End date should be greater than start date\nTry again entering the end date");
                GetAndSetEnddate();
            }
            if (choice == 1)
                return _projectManager.Create(name!, desc!, status, priority, startDate, endDate, 0,0);
            else if(choice == 2)
            {
                GetAndSetActivityId(1);
                return _taskManager.Create(name!, desc!, status, priority, startDate, endDate, id,0);
            }
            else
            {
                GetAndSetActivityId(1);
                int projectId = id;
                GetAndSetActivityId(2);
                return _subTaskManager.Create(name!, desc!, status, priority, startDate, endDate, projectId,id);
            }
        }
        public string CollectDeleteInput(int choice)
        {
            GetAndSetActivityId(choice);
            if(choice==1)
                return _projectManager.Remove(id);
            else if(choice==2)
                return _taskManager.Remove(id);
            else
                return _subTaskManager.Remove(id);
        }
        private void GetAndSetActivityId(int choice)
        {
            //ColorCode.DefaultCode("\n");
            int max;
            if (choice == 1)
                max = DisplayProjectsList();
            else if (choice == 2) 
                max = DisplayTasksList();
            else 
                max = DisplaySubTasksList();
            ColorCode.DefaultCode("\n");
            ColorCode.DefaultCode($"Choose {((ActivityOptions)(choice - 1)).ToString().ToLowerInvariant()} id : ");
            //ColorCode.DefaultCode("\n");
            id = Validation.GetIntInRange(max);
        }
        private void GetAndSetUserId()
        {
            int max = DisplayUsersList();
            ColorCode.DefaultCode($"Choose user id : ");
            userId = Validation.GetIntInRange(max);
        }
        private void GetAndSetPriority(int activity)//ok
        {
            foreach (PriorityType priorityType in Enum.GetValues(typeof(PriorityType)))
                Console.WriteLine((int)priorityType + 1 + " . " + priorityType.ToString());
            ColorCode.DefaultCode($"Choose {((ActivityOptions)(activity-1)).ToString().ToLowerInvariant()} priority : ");
            int choice = Validation.GetIntInRange(Enum.GetValues(typeof(PriorityType)).Length);
            PriorityType option = (PriorityType)(choice - 1);
            switch (option)
            {
                case PriorityType.HIGH: priority = option; break;
                case PriorityType.MEDIUM: priority = option; break;
                case PriorityType.LOW: priority = option; break;
                case PriorityType.NONE: priority = option; break;
            }
        }
        private void GetAndSetStatus(int activity)//ok
        {
            foreach (StatusType status in Enum.GetValues(typeof(StatusType)))
                Console.WriteLine((int)status + 1 + " . " + status.ToString());
            ColorCode.DefaultCode($"Choose {((ActivityOptions)(activity - 1)).ToString().ToLowerInvariant()} status : ");
            int choice = Validation.GetIntInRange(Enum.GetValues(typeof(StatusType)).Length);
            StatusType option = (StatusType)(choice - 1);
            switch (option)
            {
                case StatusType.OPEN: status = option; break;
                case StatusType.CLOSED: status = option; break;
                case StatusType.ONHOLD: status = option; break;
                case StatusType.INPROGRESS: status = option; break;
            }
        }
        private void GetAndSetName(int choice)//ok
        {
            ColorCode.DefaultCode($"Enter {((ActivityOptions)(choice-1)).ToString().ToLowerInvariant()} name : ");
            name = Console.ReadLine();
            if (string.IsNullOrEmpty(name) || name.Trim().Length == 0 || Validation.ContainsSpecialOrNumericCharacters(name))
            {
                ColorCode.FailureCode("Name should not be empty or should contain any special character");
                GetAndSetName(choice);
            }
        }
        private void GetAndSetDescription(int choice)//ok
        {
            ColorCode.DefaultCode($"Enter {((ActivityOptions)(choice - 1)).ToString().ToLowerInvariant()} description : ");
            desc = Console.ReadLine();
            if (string.IsNullOrEmpty(desc) || desc.Trim().Length == 0 || Validation.ContainsSpecialOrNumericCharacters(desc))
            {
                ColorCode.FailureCode("Description should not be empty or should contain any special character");
                GetAndSetName(choice);
            }
        }
        private void GetAndSetStartdate()
        {
            string? date;
            ColorCode.DefaultCode("Enter start date (mm/dd/yyyy): ");
            date = Console.ReadLine();
            startDate = DateOnly.Parse(DateTime.ParseExact(date!, "mm/dd/yyyy", CultureInfo.InvariantCulture).ToShortDateString());
        }
        private void GetAndSetEnddate()
        {
            string? date;
            ColorCode.DefaultCode("Enter end date (mm/dd/yyyy): ");
            date = Console.ReadLine();
            endDate = DateOnly.Parse(DateTime.ParseExact(date!, "mm/dd/yyyy",CultureInfo.InvariantCulture).ToShortDateString());
        }
        private static bool CheckDates(DateOnly startDate, DateOnly endDate)
        {
            if (endDate <= startDate)
                return true;
            else return false;
        }
        private int DisplayProjectsList()
        {
            ColorCode.DefaultCode("ID".PadRight(6)+"PROJECT NAME".PadRight(15) + "\n");
            Dictionary<int, string> projectsList = _lists.ProjectsList();
            foreach (int id in projectsList.Keys)
            {
                ColorCode.DefaultCode(id+"".PadRight(6)+projectsList[id]+"".PadRight(15));
                ColorCode.DefaultCode("\n");
            }
            return projectsList.Count;
        }
        private int DisplayTasksList()
        {
            //ColorCode.DefaultCode("\n");
            ColorCode.DefaultCode("ID".PadRight(6)+"TASK NAME".PadRight(15)+"\n");
            Dictionary<int, string> tasksList = _lists.TasksList();
            foreach (int id in tasksList.Keys)
            {
                ColorCode.DefaultCode(id+"".PadRight(6)+tasksList[id]+"".PadRight(15)+"\n");
            }
            return tasksList.Count;
        }
        private int DisplaySubTasksList()
        {
           // ColorCode.DefaultCode("\n");
            ColorCode.DefaultCode("ID".PadRight(6)+"SUBTASK NAME".PadRight(15) + "\n");
            Dictionary<int, string> subTasksList = _lists.SubTasksList();
            foreach (int id in subTasksList.Keys)
            {
                ColorCode.DefaultCode(id+"".PadRight(6)+subTasksList[id]+"".PadRight(15)+"\n");
            }
            return subTasksList.Count;
        }
        private int DisplayUsersList()
        {
           // ColorCode.DefaultCode("\n");
            ColorCode.DefaultCode("ID".PadRight(6)+"USER NAME".PadRight(15)+ "\n");
            Dictionary<int, string> UsersList = _lists.UsersList();
            foreach (int id in UsersList.Keys)
            {
                ColorCode.DefaultCode(id + "".PadRight(6) + UsersList[id] + "".PadRight(15)+"\n");
            }
            return UsersList.Count;
        }

    }
}
//list of available tasks with their project id and id should be mentioned also for userid -> done

