using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TimeSheet.Bll.Models
{
    public class ProjectViewModel
    {
        [Required]
        public string ProjectCode { get; set; }
        [Required]
        public string ProjectName { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
