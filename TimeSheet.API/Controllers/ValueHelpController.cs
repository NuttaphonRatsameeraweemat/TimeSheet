using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeSheet.Bll.Components;
using TimeSheet.Bll.Interfaces;
using TimeSheet.Bll.Models;

namespace TimeSheet.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ValueHelpController : ControllerBase
    {

        #region Fields

        /// <summary>
        /// The Login manager provides Login functionality.
        /// </summary>
        private readonly IValueHelpBll _valueHelp;

        #endregion

        #region Constructors

        /// <summary>
        ///  Initializes a new instance of the <see cref="ValueHelpController" /> class.
        /// </summary>
        /// <param name="valueHelp"></param>
        public ValueHelpController(IValueHelpBll valueHelp)
        {
            _valueHelp = valueHelp;
        }

        #endregion

        #region Methods

        [HttpGet]
        [Route("GetTypeProject")]
        public IActionResult GetTypeProject()
        {
            return Ok(_valueHelp.Get(ConstantValue.VALUE_PROJECT_TYPE));
        }

        [HttpGet]
        [Route("GetProjectStatus")]
        public IActionResult GetProjectStatus()
        {
            return Ok(_valueHelp.Get(ConstantValue.VALUE_PROJECT_STATUS));
        }

        #endregion

    }
}