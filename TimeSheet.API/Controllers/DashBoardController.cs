using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeSheet.Bll.Interfaces;
using TimeSheet.Bll.Models;
using TimeSheet.Helper.Models;

namespace TimeSheet.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class DashBoardController : ControllerBase
    {

        #region Fields

        /// <summary>
        /// The Project manager provides project functionality.
        /// </summary>
        private readonly IDashBoardBll _dashboard;
        /// <summary>
        /// The ClaimsIdentity in token management.
        /// </summary>
        private readonly IManageToken _token;

        #endregion

        #region Constructors

        /// <summary>
        ///  Initializes a new instance of the <see cref="DashBoardController" /> class.
        /// </summary>
        /// <param name="project"></param>
        public DashBoardController(IDashBoardBll dashboard, IManageToken token)
        {
            _dashboard = dashboard;
            _token = token;
        }

        #endregion

        #region Methods

        [HttpGet]
        [Route("GetProjectTypeStat")]
        public IActionResult GetProjectTypeStat(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                email = _token.Email;
            }
            return Ok(_dashboard.GetProjectTypeStat(email));
        }

        [HttpGet]
        [Route("GetProjectStat")]
        public IActionResult GetProjectStat(string email, string year)
        {
            if (string.IsNullOrEmpty(email))
            {
                email = _token.Email;
            }
            if (string.IsNullOrEmpty(year))
            {
                return BadRequest(new ResultViewModel { IsError = true, Message = "Year can't be empty." });
            }
            return Ok(_dashboard.GetProjectStat(email, year));
        }

        #endregion

    }
}