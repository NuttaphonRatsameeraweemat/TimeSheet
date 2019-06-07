using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeSheet.Bll.Interfaces;
using TimeSheet.Bll.Models;

namespace TimeSheet.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RefreshTokenController : ControllerBase
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
        ///  Initializes a new instance of the <see cref="RefreshTokenController" /> class.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="refreshToken"></param>
        public RefreshTokenController(ILoginBll login, IRefreshTokenBll refreshToken)
        {
            _refreshToken = refreshToken;
            _login = login;
        }

        #endregion

        #region Methods

        [HttpPost]
        public IActionResult RefreshToken(RefreshTokenViewModel model)
        {
            IActionResult response = Unauthorized();
            var principal = _refreshToken.GetPrincipalFromExpiredToken(model.Token);
            if (principal != null && _refreshToken.ValidateRefreshToken(principal.Identity.Name, model.RefreshToken))
            {
                var result = new RefreshTokenViewModel
                {
                    Token = _login.BuildToken(principal),
                    RefreshToken = _refreshToken.GenerateRefreshToken(principal.Identity.Name)
                };
                _login.SetupCookie(HttpContext, result.Token);
                response = Ok(result);
            }
            return response;
        }

        #endregion

    }
}