using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private IValueHelpBll _valueHelp;

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
            return Ok(new List<ValueHelpViewModel>
            {
                new ValueHelpViewModel{ ValueKey = "T001", ValueText = "Support"},
                new ValueHelpViewModel{ ValueKey = "T002", ValueText = "Proof of Concept"},
                new ValueHelpViewModel{ ValueKey = "T003", ValueText = "Development"},
            });
        }

        [HttpGet]
        [Route("GetProject")]
        public IActionResult GetProject()
        {
            return Ok(new List<ValueHelpViewModel>
            {
                new ValueHelpViewModel{ ValueKey = "P001", ValueText = "Digital Signature"},
                new ValueHelpViewModel{ ValueKey = "P002", ValueText = "SmartForm"},
                new ValueHelpViewModel{ ValueKey = "P003", ValueText = "TimeSheet"},
            });
        }

        #endregion

    }
}