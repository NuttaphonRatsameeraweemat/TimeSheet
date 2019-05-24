using System;
using System.Collections.Generic;
using System.Text;

namespace TimeSheet.Bll.Models
{
    public class EmployeeViewModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string StartWorkingDay { get; set; }
        public int TotalWorking { get; set; }
        public string TelNo { get; set; }
        public string Token { get; set; }
    }
}
