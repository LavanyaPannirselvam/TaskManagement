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
            {
                if (toBeViewedProject.Issues.Count > 0)
                {
                    return "Need to view its task or issue ? (task/issue/no) : ";
                }
                else return "Need to view its tasks? : (yes/no) : ";
            }
            else if (toBeViewedProject.Issues.Count > 0)
                return "Need to view its issues? (yes/no) : ";              
            else return "Task and issue are not assigned for this project";
        }
        
        public string ViewAssignedTasks(List<int> taskIds,int projectId)
        {
            foreach(int taskId in taskIds)
            {
                ColorCode.DefaultCode(_database.GetTask(taskId).ToString() + "\n");
            }
            if (_database.GetProject(projectId).SubTasks.Count > 0)
                return "Need to view its subtasks? : (yes/no) : ";
            else return "Subtasks are not assigned for this task";
        }

        public string ViewAssignedSubTasks(List<int> subtaskIds, int projectId)
        {
            foreach (int subtaskId in subtaskIds)
            {
                ColorCode.DefaultCode(_database.GetSubTask(subtaskId).ToString() + "\n");
            }
            if (_database.GetProject(projectId).SubtaskofSubtask.Count > 0)
                return "Need to view it subtask of this subtask? : (yes/no) : ";
            else return "Subtasks are not assigned for this subtask";
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