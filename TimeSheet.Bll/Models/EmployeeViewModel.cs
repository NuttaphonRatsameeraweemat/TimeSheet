using System;
using System.Collections.Generic;
using System.Text;

namespace TimeSheet.Bll.Models
{
    public class EmployeeViewModel
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public DateTime? StartWorkingDay { get; set; }
        public string Token { get; set; }
    }
}
