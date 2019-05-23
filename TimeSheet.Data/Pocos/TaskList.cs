using System;
using System.Collections.Generic;
using System.Text;

namespace TimeSheet.Data.Pocos
{
    public class TaskList
    {
        public int Id { get; set; }
        public int? TimeSheetId { get; set; }
        public string ProjectCode { get; set; }
        public string TypeCode { get; set; }
        public string Description { get; set; }
        public int? WorkingHours { get; set; }
    }
}
