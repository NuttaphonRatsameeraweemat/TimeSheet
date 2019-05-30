using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TimeSheet.Bll.Models
{
    public class RoleViewModel
    {
        public int RoleId { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}
