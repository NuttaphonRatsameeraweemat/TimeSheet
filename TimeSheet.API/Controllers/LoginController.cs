﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeSheet.Bll.Interfaces;
using TimeSheet.Bll.Models;

namespace TimeSheet.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        #region Fields

        /// <summary>
        /// The Login manager provides Login functionality.
        /// </summary>
        private readonly ILoginBll _login;
        /// <summary>
        /// The refresh token manager provides refresh token functionality.
        /// </summary>
        private readonly IRefreshTokenBll _refreshToken;

        #endregion

        #region Constructors

        /// <summary>
        ///  Initializes a new instance of the <see cref="LoginController" /> class.
        /// </summary>
        /// <param name="login"></param>
        public LoginController(ILoginBll login, IRefreshTokenBll refreshToken)
        {
            _login = login;
            _refreshToken = refreshToken;
        }

        #endregion

        #region Methods

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody]LoginViewModel auth)
        {
            IActionResult response = Unauthorized();
            var model = new EmployeeViewModel();
            if (_login.Authenticate(auth, model))
            {
                string token = _login.BuildToken();
                var responseMessage = new
                {
                    Employee = model,
                    Token = token,
                    RefreshToken = _refreshToken.GenerateRefreshToken(auth.Username)
                };
                _login.SetupCookie(HttpContext, token);

                response = Ok(responseMessage);
            }

            return response;
        }

        #endregion

    }
}