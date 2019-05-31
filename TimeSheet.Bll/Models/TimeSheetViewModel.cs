using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TimeSheet.Bll.Models
{
    public class TimeSheetViewModel
    {
        public TimeSheetViewModel()
        {
            TimeSheet = new List<TimeSheetModel>();
        }
        
        public List<TimeSheetModel> TimeSheet { get; set; }
    }

    public class TimeSheetModel
    {
        public TimeSheetModel()
        {
            TaskList = new List<TaskListModel>();
        }

        public int TimeSheetId { get; set; }
        [Required]
        public string DateTimeStamp { get; set; }
        public List<TaskListModel> TaskList { get; set; }
    }

    public class TaskListModel
    {
        public int Id { get; set; }
        public int TimeSheetId { get; set; }
        [Required]
        public string ProjectCode { get; set; }
        [Required]
        public string TypeCode { get; set; }
        [Required]
        public int WorkingHours { get; set; }
        public string Description { get; set; }
    }

}
