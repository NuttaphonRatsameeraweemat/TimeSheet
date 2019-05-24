using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeSheet.Data.Pocos
{
    public partial class TaskList
    {
        [Column("ID")]
        public int Id { get; set; }
        [Column("TimeSheetID")]
        public int? TimeSheetId { get; set; }
        [StringLength(20)]
        public string ProjectCode { get; set; }
        [StringLength(20)]
        public string TypeCode { get; set; }
        public string Description { get; set; }
        public int? WorkingHours { get; set; }
    }
}
