using System;
using System.Collections.Generic;
using System.Text;
using TimeSheet.Bll.Components;

namespace TimeSheet.Bll.Models
{
    public class EmployeeViewModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string StartWorkingDay { get; set; }
        public int TotalWorking
        {
            get
            {
                var startWorking = DateTime.TryParseExact(StartWorkingDay, ConstantValue.DATE_TIME_FORMAT,
                   System.Globalization.CultureInfo.InvariantCulture,
                   System.Globalization.DateTimeStyles.None, out DateTime temp) ? temp : throw new ArgumentException($"DateTime incorrect format : {StartWorkingDay}");
                return ((DateTime.Now.Year - startWorking.Year) * 12) + DateTime.Now.Month - startWorking.Month;
            }
        }
        public string TelNo { get; set; }
    }
}
