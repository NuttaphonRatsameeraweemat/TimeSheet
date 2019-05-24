using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeSheet.Data.Pocos
{
    public partial class Role
    {
        [Column("RoleID")]
        public int RoleId { get; set; }
        [StringLength(255)]
        public string RoleName { get; set; }
    }
}
