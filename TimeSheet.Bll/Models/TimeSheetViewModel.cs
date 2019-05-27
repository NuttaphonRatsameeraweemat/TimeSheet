using System;
using System.Collections.Generic;
using System.Text;

namespace TimeSheet.Bll.Models
{
    public class TimeSheetViewModel
    {
        public TimeSheetViewModel()
        {
            TimeSheet = new List<TimeSheetModel>();
        }

        public bool IsSave { get; set; }
        public List<TimeSheetModel> TimeSheet { get; set; }
    }

    public class TimeSheetModel
    {
        public TimeSheetModel()
        {
            TaskList = new List<TaskListModel>();
        }

        public int TimeSheetId { get; set; }
        public string DateTimeStamp { get; set; }
        public List<TaskListModel> TaskList { get; set; }
    }

    public class TaskListModel
    {
        public int TaskId { get; set; }
        public int TimeSheetId { get; set; }
        public string ProjectCode { get; set; }
        public string TypeCode { get; set; }
        public int WorkingHours { get; set; }
        public string Description { get; set; }
    }

}
