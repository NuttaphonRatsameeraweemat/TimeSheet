using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeSheet.Data.Pocos
{
    public partial class Employee
    {
        [StringLength(255)]
        public string Email { get; set; }
        [StringLength(255)]
        public string FirstName { get; set; }
        [StringLength(255)]
        public string LastName { get; set; }
        [Column(TypeName = "date")]
        public DateTime? StartWorkingDay { get; set; }
    }
}
