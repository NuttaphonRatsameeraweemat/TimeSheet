using System;
using System.Collections.Generic;
using System.Text;

namespace TimeSheet.Bll.Models
{
    public class RefreshTokenModel
    {
        public string RefreshToken { get; set; }
    }

    public class RefreshTokenViewModel
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }

}
