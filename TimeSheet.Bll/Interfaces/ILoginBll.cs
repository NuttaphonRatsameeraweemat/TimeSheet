using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using TimeSheet.Bll.Models;

namespace TimeSheet.Bll.Interfaces
{
    public interface ILoginBll
    {
        /// <summary>
        /// Create and setting payload on token.
        /// </summary>
        /// <param name="principal">The ClaimsPrincipal.</param>
        /// <returns></returns>
        string BuildToken(ClaimsPrincipal principal = null);
        /// <summary>
        /// Validate username and password is valid.
        /// </summary>
        /// <param name="login">The login value.</param>
        /// <returns></returns>
        bool Authenticate(LoginViewModel login, EmployeeViewModel model);
        /// <summary>
        /// Setup response cookie and cookie options token.
        /// </summary>
        /// <param name="httpContext">The HttpContext.</param>
        /// <param name="token">The token value.</param>
        void SetupCookie(HttpContext httpContext, string token);
    }
}
