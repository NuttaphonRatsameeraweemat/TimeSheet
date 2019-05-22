using System;
using System.Collections.Generic;
using System.Text;
using TimeSheet.Bll.Models;

namespace TimeSheet.Bll.Interfaces
{
    public interface ILoginBll
    {
        string BuildToken(string aduser);
        bool Authenticate(LoginViewModel login);
    }
}
