using System;
using System.Collections.Generic;
using System.Text;
using TimeSheet.Bll.Models;

namespace TimeSheet.Bll.Interfaces
{
    public interface ILoginBll
    {
        /// <summary>
        /// Create and setting payload on token.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        string BuildToken(string username);
        /// <summary>
        /// Validate username and password is valid.
        /// </summary>
        /// <param name="login">The login value.</param>
        /// <returns></returns>
        bool Authenticate(LoginViewModel login, EmployeeViewModel model);
    }
}
