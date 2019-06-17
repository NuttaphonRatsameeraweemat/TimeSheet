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

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [RegularExpression(ConstantValue.REGEX_DATE_FORMAT, ErrorMessage = ConstantValue.ERR_DATE_INCORRECT_FORMAT)]
        public string StartWorkingDayText { get; set; }

        [RegularExpression(@"^[0-9]{1}$|^[0-9]{1}[0-9\s]*[0-9]+$", ErrorMessage = "Input Only Number")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Telephone number is incorrect format.")]
        public string TelNo { get; set; }

        [Required]
        public int RoleId { get; set; }

    }
}
