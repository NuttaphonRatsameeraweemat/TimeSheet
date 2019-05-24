using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeSheet.Data.Pocos
{
    public partial class UserRole
    {
        [StringLength(255)]
        public string Email { get; set; }
        [Column("RoleID")]
        public int RoleId { get; set; }
    }
}
