using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using TimeSheet.Bll.Interfaces;
using TimeSheet.Bll.Models;
using TimeSheet.Helper;
using TimeSheet.Bll.Components;

namespace TimeSheet.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class TimeSheetController : ControllerBase
    {

        #region Fields

        /// <summary>
        /// The timesheet manager provides Login functionality.
        /// </summary>
        private readonly ITimeSheetBll _timeSheet;
        /// <summary>
        /// The ClaimsIdentity in token management.
        /// </summary>
        private readonly IManageToken _token;

        #endregion

        #region Constructors

        /// <summary>
        ///  Initializes a new instance of the <see cref="TimeSheetController" /> class.
        /// </summary>
        /// <param name="login"></param>
        public TimeSheetController(ITimeSheetBll timeSheet, IManageToken token)
        {
            _timeSheet = timeSheet;
            _token = token;
        }

        #endregion

        #region Methods

        [HttpGet]
        public IActionResult Get(string date)
        {
            IActionResult response;
            if (!_timeSheet.IsDateMatchRegex(date))
            {
                response = BadRequest(UtilityService.InitialResultError(ConstantValue.ERR_DATE_INCORRECT_FORMAT));
            }
            else response = Ok(_timeSheet.Get(_token.Email, date));
            return response;
        }

        [HttpPost]
        [Route("Save")]
        public IActionResult Save(TimeSheetViewModel formData)
        {
            IActionResult response;
            var result = _timeSheet.Save(formData, _token.Email);
            if (result.IsError)
            {
                response = Conflict(result);
            }
            else response = Ok(result);
            return response;
        }

        [HttpPost]
        [Route("Edit")]
        public IActionResult Edit(TimeSheetViewModel formData)
        {
            return Ok(_timeSheet.Update(formData, _token.Email));
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(TimeSheetViewModel formData)
        {
            return Ok(_timeSheet.Delete(formData));
        }

        #endregion

    }
}