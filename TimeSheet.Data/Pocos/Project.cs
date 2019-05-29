using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeSheet.Data.Pocos
{
    public partial class Project
    {
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(100)]
        public string ProjectCode { get; set; }
        [StringLength(100)]
        public string ProjectName { get; set; }
        [StringLength(100)]
        public string Status { get; set; }
    }
}
