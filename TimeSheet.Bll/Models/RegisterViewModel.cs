using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TimeSheet.Bll.Components;

namespace TimeSheet.Bll.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^\S*$", ErrorMessage = "Whitespace Not Allowed")]
        public string Password { get; set; }

        [RegularExpression(@"^\S*$", ErrorMessage = "Whitespace Not Allowed")]
        [Compare("Password", ErrorMessage = "Password Confirm not match.")]
        public string ConfirmPassword { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StartWorkingDayText { get; set; }
        public DateTime? StartWorkingDay
        {
            get
            {
                return DateTime.TryParseExact(StartWorkingDayText, ConstantValue.DATE_TIME_FORMAT,
                   System.Globalization.CultureInfo.InvariantCulture,
                   System.Globalization.DateTimeStyles.None, out DateTime temp) ? temp : new DateTime();
            }
            set
            {
                StartWorkingDay = value;
                StartWorkingDayText = StartWorkingDay.Value.ToString(ConstantValue.DATE_TIME_FORMAT);
            }
        }
    }
}
