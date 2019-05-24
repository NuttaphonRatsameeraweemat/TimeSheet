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
    public class RegisterController : ControllerBase
    {

        #region Fields

        /// <summary>
        /// The Register manager provides Register functionality.
        /// </summary>
        private IRegisterBll _register;

        #endregion

        #region Constructors

        /// <summary>
        ///  Initializes a new instance of the <see cref="RegisterController" /> class.
        /// </summary>
        /// <param name="register"></param>
        public RegisterController(IRegisterBll register)
        {
            _register = register;
        }

        #endregion

        #region Methods

        [HttpPost]
        public IActionResult Register([FromBody]RegisterViewModel formData)
        {
            return Ok(_register.Register(formData));
        }

        #endregion

    }
}