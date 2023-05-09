using System.Collections.Generic;
using System.Globalization;
using TaskManagementApplication.Controller;
using TaskManagementApplication.Enumerations;
using TaskManagementApplication.Model;
using TaskManagementApplication.Utils;

namespace TaskManagementApplication.Presentation
{
    public class ProjectInput
    {
        private readonly ProjectManagement _projectManager = new();
        private readonly TaskManagement _taskManager = new();
        private readonly SubTaskManagement _subTaskManager = new();
        private readonly SmallSubTaskManagement _smallSubTaskManager = new();
        private readonly IssueManagement _issueManager = new();
        private readonly ActivityList _lists = new();
        int activityId;
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
            if (choice == 1)
                return _projectManager.AssignUser(activityId, userId);
            else if (choice == 2)
                return _taskManager.AssignUser(activityId, userId);
            else if (choice == 3)
                return _subTaskManager.AssignUser(activityId, userId);
            else if (choice == 4)
                return _smallSubTaskManager.AssignUser(activityId, userId);
            else return _issueManager.AssignUser(activityId, userId);
        }
        public string CollectDeassignUserInput(int choice)
        {
            GetAndSetActivityId(choice);
            GetAndSetUserId();
            if (choice == 1)
                return _projectManager.DeassignUser(activityId, userId);
            else if (choice == 2)
                return _taskManager.DeassignUser(activityId, userId);
            else if (choice == 3)
                return _subTaskManager.DeassignUser(activityId, userId);
            else if (choice == 4)
                return _smallSubTaskManager.DeassignUser(activityId, userId);
            else return _issueManager.DeassignUser(activityId, userId);
        }
        public string CollectViewActivityInput(int choice)
        {
            GetAndSetActivityId(choice);
            if (choice == 1)
                return _projectManager.ViewActivity(activityId);
            else if (choice == 2)
                return _taskManager.ViewActivity(activityId);
            else if (choice == 3)
                return _subTaskManager.ViewActivity(activityId);
            else if(choice == 4)
                return _smallSubTaskManager.ViewActivity(activityId);
            else return _issueManager.ViewActivity(activityId);
        }
        public string CollectChangePriorityInput(int choice)
        {
            GetAndSetActivityId(choice);
            GetAndSetPriority(choice);
            if (choice == 1)
                return _projectManager.ChangePriorityOfActivity(activityId, priority);
            else if (choice == 2)
                return _taskManager.ChangePriorityOfActivity(activityId, priority);
            else if (choice == 3)
                return _subTaskManager.ChangePriorityOfActivity(activityId, priority);
            else if (choice == 4)
                return _smallSubTaskManager.ChangePriorityOfActivity(activityId, priority);
            else
                return _issueManager.ChangePriorityOfActivity(activityId, priority);
        }
        public string CollectChangeStatusInput(int choice)
        {
            GetAndSetActivityId(choice);
            GetAndSetStatus(choice);
            if (choice == 1)
                return _projectManager.ChangeStatusOfActivity(activityId, status);
            else if (choice == 2)
                return _taskManager.ChangeStatusOfActivity(activityId, status);
            else if (choice == 3)
                return _subTaskManager.ChangeStatusOfActivity(activityId, status);
            else if (choice == 4)
                return _smallSubTaskManager.ChangeStatusOfActivity(activityId, status);
            else
                return _issueManager.ChangeStatusOfActivity(activityId, status);
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
                ColorCode.FailureCode("End date should be greater than start date");
                ColorCode.DefaultCode("Try again entering the end date\n");
                GetAndSetEnddate();
            }
            if (choice == 1)
                return _projectManager.CreateActivity(name!, desc!, status, priority, startDate, endDate, 0, 0, 0);
            else if (choice == 2)
            {
                GetAndSetActivityId(1);
                return _taskManager.CreateActivity(name!, desc!, status, priority, startDate, endDate, activityId, 0, 0);
            }
            else if (choice == 3)
            {
                GetAndSetActivityId(1);
                int projectId = activityId;
                GetAndSetActivityId(2);
                return _subTaskManager.CreateActivity(name!, desc!, status, priority, startDate, endDate, projectId, activityId, 0);
            }
            else if(choice == 4)
            {
                GetAndSetActivityId(1);
                int projectId = activityId;
                GetAndSetActivityId(2);
                int taskId = activityId;
                GetAndSetActivityId(3);
                return _smallSubTaskManager.CreateActivity(name!, desc!, status, priority, startDate, endDate, projectId, taskId, activityId);
            }
            else
            {
                GetAndSetActivityId(1);
                return _issueManager.CreateActivity(name!, desc!, status, priority, startDate, endDate, activityId, 0, 0);
            }
        }
        public string CollectDeleteInput(int choice)
        {
            GetAndSetActivityId(choice);
            if (choice == 1)
                return _projectManager.RemoveActivity(activityId);
            else if (choice == 2)
                return _taskManager.RemoveActivity(activityId);
            else if (choice == 3)
                return _subTaskManager.RemoveActivity(activityId);
            else if (choice == 4)
                return _smallSubTaskManager.RemoveActivity(activityId);
            else
                return _issueManager.RemoveActivity(activityId);
        }
        private void GetAndSetActivityId(int choice)
        {
            if (choice == 1)
                DisplayProjectsList();
            else if (choice == 2)
                DisplayTasksList();
            else if (choice == 3)
                DisplaySubTasksList();
            else if(choice == 4)
                DisplaySmallSubTasksList();
            else 
                DisplayIssuesList();
            ColorCode.DefaultCode("\n");
            ColorCode.DefaultCode($"Choose {((ActivityOptions)(choice - 1)).ToString().Replace("_", " ").ToLowerInvariant()} id".PadRight(20) + " : ");
            if (!int.TryParse(Console.ReadLine(), out int tempId))
            {
                ColorCode.FailureCode($"{((ActivityOptions)(choice - 1)).ToString().Replace("_", " ").ToLowerInvariant()} id should be only in number format");
                GetAndSetActivityId(choice);
            }
            if (choice == 1)
            {
                if (Validation.IsChoiceAvailable(tempId, _lists.ProjectsList()))
                    activityId = tempId;
                else
                {
                    ColorCode.FailureCode("Id you choosen is not listed in the list,choose one from the listed options");
                    GetAndSetActivityId(choice);
                }
            }
            else if (choice == 2)
            {
                if (Validation.IsChoiceAvailable(tempId, _lists.TasksList()))
                    activityId = tempId;
                else
                {
                    ColorCode.FailureCode("Id you choosen is not listed in the list,choose one from the listed options");
                    GetAndSetActivityId(choice);
                }
            }
            else if (choice == 3)
            {
                if (Validation.IsChoiceAvailable(tempId, _lists.SubTasksList()))
                    activityId = tempId;
                else
                {
                    ColorCode.FailureCode("Id you choosen is not listed in the list,choose one from the listed options");
                    GetAndSetActivityId(choice);
                }
            }
            else if(choice == 4)
            {
                if (Validation.IsChoiceAvailable(tempId, _lists.SmallSubTasksList()))
                    activityId = tempId;
                else
                {
                    ColorCode.FailureCode("Id you choosen is not listed in the list,choose one from the listed options");
                    GetAndSetActivityId(choice);
                }
            }
            else
            {
                if (Validation.IsChoiceAvailable(tempId, _lists.IssuesList()))
                    activityId = tempId;
                else
                {
                    ColorCode.FailureCode("Id you choosen is not listed in the list,choose one from the listed options");
                    GetAndSetActivityId(choice);
                }
            }
        }
        private void GetAndSetUserId()
        {
            DisplayUsersList();
            ColorCode.DefaultCode($"Choose user id".PadRight(20) + " : ");
            if (!int.TryParse(Console.ReadLine(), out int tempId))
            {
                ColorCode.FailureCode("User id should be only in number format");
                GetAndSetUserId();
            }
            if (Validation.IsChoiceAvailable(tempId, _lists.UsersList()))
                userId = tempId;
            else
            {
                ColorCode.FailureCode("Id you choosen is not listed in the list,choose one from the listed options");
                GetAndSetUserId();
            }
        }
        private void GetAndSetPriority(int activity)//ok
        {
            foreach (PriorityType priorityType in Enum.GetValues(typeof(PriorityType)))
                Console.WriteLine((int)priorityType + 1 + " . " + priorityType.ToString());
            ColorCode.DefaultCode($"Choose {((ActivityOptions)(activity - 1)).ToString().Replace("_", " ").ToLowerInvariant()} priority".PadRight(30) + " : ");
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
            ColorCode.DefaultCode($"Choose {((ActivityOptions)(activity - 1)).ToString().Replace("_", " ").ToLowerInvariant()} status".PadRight(30) + " : ");
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
            ColorCode.DefaultCode($"Enter {((ActivityOptions)(choice - 1)).ToString().Replace("_", " ").ToLowerInvariant()} name".PadRight(30) + " : ");
            name = Console.ReadLine();
            if (string.IsNullOrEmpty(name) || name.Trim().Length == 0 || Validation.ContainsSpecialOrNumericCharacters(name))
            {
                ColorCode.FailureCode("Name should not be empty or should not contain any special character");
                GetAndSetName(choice);
            }
        }
        private void GetAndSetDescription(int choice)//ok
        {
            ColorCode.DefaultCode($"Enter {((ActivityOptions)(choice - 1)).ToString().Replace("_", " ").ToLowerInvariant()} description".PadRight(30) + " : ");
            desc = Console.ReadLine();
            if (string.IsNullOrEmpty(desc) || desc.Trim().Length == 0 || Validation.ContainsSpecialOrNumericCharacters(desc))
            {
                ColorCode.FailureCode("Description should not be empty or should not contain any special character");
                GetAndSetDescription(choice);
            }
        }
        private void GetAndSetStartdate()
        {
            string? date;
            ColorCode.DefaultCode("Enter start date (dd/mm/yyyy)".PadRight(30) + " : ");
            date = Console.ReadLine();
            string[] splits = date!.Split("/");
            startDate = new(Int16.Parse(splits[2]), Int16.Parse(splits[1]), Int16.Parse(splits[0]));
        }
        private void GetAndSetEnddate()
        {
            string? date;
            ColorCode.DefaultCode("Enter end date (dd/mm/yyyy)".PadRight(30) + " : ");
            date = Console.ReadLine();
            string[] splits = date!.Split("/");
            endDate = new(Int16.Parse(splits[2]),Int16.Parse(splits[1]), Int16.Parse(splits[0]));
        }
        private static bool CheckDates(DateOnly startDate, DateOnly endDate)
        {
            if (endDate <= startDate)
                return true;
            else return false;
        }
        private void DisplayProjectsList()
        {
            Dictionary<int, string> projectsList = _lists.ProjectsList();
            if (projectsList.Count == 0)
                ColorCode.FailureCode("No project available");
            else
            {
                ColorCode.DefaultCode("ID".PadRight(6) + "PROJECT NAME".PadRight(15) + "\n");
                foreach (int id in projectsList.Keys)
                {
                    ColorCode.DefaultCode(id + "".PadRight(6) + projectsList[id] + "".PadRight(15));
                    ColorCode.DefaultCode("\n");
                }
            }
        }
        private void DisplayTasksList()
        {
            Dictionary<int, string> tasksList = _lists.TasksList();
            if (tasksList.Count == 0)
                ColorCode.FailureCode("No tasks available");
            else
            {
                ColorCode.DefaultCode("ID".PadRight(6) + "TASK NAME".PadRight(15) + "\n");
                foreach (int id in tasksList.Keys)
                {
                    ColorCode.DefaultCode(id + "".PadRight(6) + tasksList[id] + "".PadRight(15) + "\n");
                }
            }
        }
        private void DisplaySubTasksList()
        {
            Dictionary<int, string> subTasksList = _lists.SubTasksList();
            if (subTasksList.Count == 0)
                ColorCode.FailureCode("No subtask available");
            else 
            {
                ColorCode.DefaultCode("ID".PadRight(6) + "SUBTASK NAME".PadRight(15) + "\n");
                foreach (int id in subTasksList.Keys)
                {
                    ColorCode.DefaultCode(id + "".PadRight(6) + subTasksList[id] + "".PadRight(15) + "\n");
                }
            }
        }
        private void DisplaySmallSubTasksList()
        {
            Dictionary<int, string> smallSubTasksList = _lists.SmallSubTasksList();
            if (smallSubTasksList.Count == 0)
                ColorCode.FailureCode("No subtask of subtask available");
            else
            {
                ColorCode.DefaultCode("ID".PadRight(6) + "SUBTASK OF SUBTASK NAME".PadRight(15) + "\n");
                foreach (int id in smallSubTasksList.Keys)
                {
                    ColorCode.DefaultCode(id + "".PadRight(6) + smallSubTasksList[id] + "".PadRight(15) + "\n");
                }
            }
        }
        private void DisplayIssuesList()
        {
            Dictionary<int, string> issuesList = _lists.IssuesList();
            if (issuesList.Count == 0)
                ColorCode.FailureCode("No issue available");
            else
            {
                ColorCode.DefaultCode("ID".PadRight(6) + "ISSUE NAME".PadRight(15) + "\n");
                foreach (int id in issuesList.Keys)
                {
                    ColorCode.DefaultCode(id + "".PadRight(6) + issuesList[id] + "".PadRight(15) + "\n");
                }
            }
        }
        private void DisplayUsersList()
        {
            Dictionary<int, string> usersList = _lists.UsersList();
            if (usersList.Count == 0)
                ColorCode.FailureCode("No user available");
            else
            {
                ColorCode.DefaultCode("ID".PadRight(6) + "USER NAME".PadRight(15) + "\n");
                foreach (int listId in usersList.Keys)
                {
                    ColorCode.DefaultCode(listId + "".PadRight(6) + usersList[listId] + "".PadRight(15) + "\n");
                }
            }
        }

    }
}
//list of available tasks with their project id and id should be mentioned also for userid -> done
//choosen option will be sent to a function to check for its availability and then it returns boolean -> done
//based on boolean value operation continues -> done(90%)
