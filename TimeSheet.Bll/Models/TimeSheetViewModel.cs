using System;
using System.Collections.Generic;
using System.Text;

namespace TimeSheet.Bll.Models
{
    public class TimeSheetViewModel
    {
        public bool IsSubmit { get; set; }
        public List<TaskModel> TaskList { get; set; }
    }

    public class TaskModel
    {
        public string Project { get; set; }
        public List<TypeModel> Type { get; set; }
    }

    public class TypeModel
    {
        public string TypeName { get; set; }
        public List<TimeStampModel> TimeStamp { get; set; }
    }
    
    public class TimeStampModel
    {
        public DateTime DateTimeStamp { get; set; }
        public int WorkingHours { get; set; }
        public string Description { get; set; }
    }

}
