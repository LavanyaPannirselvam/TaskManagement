using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApplication.DataBase;
using TaskManagementApplication.Model;
using TaskManagementApplication.Utils;

namespace TaskManagementApplication.Controller
{
    public class ActivityViewer
    {
        private readonly Database _database;
        public ActivityViewer()
        {
            _database = Database.GetInstance();
        }
        public string ViewProject(int projectId)
        {
            Project toBeViewedProject = _database.GetProject(projectId);
            ColorCode.DefaultCode(toBeViewedProject.ToString() + "\n");
            if (toBeViewedProject.CreatedTasks.Count > 0)
                return "Need to view its tasks? : (yes/no) : ";//return this
            else return "Tasks are not assigned for this project";//or this
        }
        public string ViewAssignedTasks(int taskId)
        {
            Tasks toBeViewedTask = _database.GetTask(taskId);
            ColorCode.DefaultCode(toBeViewedTask.ToString() + "\n");
            if (toBeViewedTask.SubTasks.Count > 0)
                return "Need to view its subtasks? : (yes/no) : ";//enum podanum
            else return "Subtasks are not assigned for this task";
        }

        public string ViewAssignedSubTasks(int subtaskId)
        {
            SubTask toBeViewedSubtask = _database.GetSubTask(subtaskId);
            ColorCode.DefaultCode(toBeViewedSubtask.ToString() + "\n");
            if (toBeViewedSubtask.Subtask.Count > 0)
                return "Need to view subtask of this subtask? : (yes/no) : ";//enum podanum      
            else return "Subtasks are not assigned for this task";
        }

        public void ViewAssignedSmallSubTasks(int smallSubtaskId)
        {
            SmallSubTask toBeViewedSmallSubtask = _database.GetSmallSubTask(smallSubtaskId);
            ColorCode.DefaultCode(toBeViewedSmallSubtask.ToString() + "\n");
        }

        public void ViewAssignedIssues(int issueId)
        {
            Issue issue = _database.GetIssue(issueId);
            ColorCode.DefaultCode(issue.ToString() + "\n");
        }

    }
}
/*public void ViewProject(int projectId)
        {
            Project toBeViewedProject = _database.GetProject(projectId);
            ColorCode.DefaultCode(toBeViewedProject.ToString() + "\n");
            if (toBeViewedProject.CreatedTasks.Count > 0)
            {
                ColorCode.DefaultCode("Need to view its tasks? : (yes/no) : ");//return this
                string answer = Console.ReadLine();
                if (answer == "yes")
                {
                    foreach (Tasks task in toBeViewedProject.CreatedTasks)
                    {
                        ColorCode.DefaultCode($"------Task Id : {task.Id}------");
                        ViewAssignedTasks(task.Id);
                    }
                }
            }
            else ColorCode.FailureCode("Tasks are not assigned for this project");//or this
        }
*/
