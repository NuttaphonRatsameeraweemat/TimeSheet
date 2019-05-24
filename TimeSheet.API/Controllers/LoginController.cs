using Microsoft.AspNetCore.Authorization;
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
        private ILoginBll _login;

        #endregion

        #region Constructors

        /// <summary>
        ///  Initializes a new instance of the <see cref="LoginController" /> class.
        /// </summary>
        /// <param name="config"></param>
        /// <param name="login"></param>
        public LoginController(ILoginBll login)
        {
            _login = login;
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
                model.Token = _login.BuildToken(auth.Username);

                //Generate Cookies for authenticate.
                HttpContext.Response.Cookies.Append("access_token", model.Token, new Microsoft.AspNetCore.Http.CookieOptions()
                {
                    Path = "/",
                    HttpOnly = true, // to prevent XSS
                    Secure = false, // set to true in production
                    Expires = System.DateTime.UtcNow.AddMinutes(5) // token life time
                });

                response = Ok(model);
            }

            return response;
        }

        #endregion

    }
}