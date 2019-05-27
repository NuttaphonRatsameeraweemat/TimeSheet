using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeSheet.Bll.Interfaces;
using TimeSheet.Bll.Models;

namespace TimeSheet.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TimeSheetController : ControllerBase
    {

        #region Fields

        /// <summary>
        /// The Login manager provides Login functionality.
        /// </summary>
        private ITimeSheetBll _timeSheet;
        /// <summary>
        /// The ClaimsIdentity in token management.
        /// </summary>
        private IManageToken _token;

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
            return Ok(_timeSheet.Get(_token.Email, date));
        }

        [HttpPost]
        [Route("Save")]
        public IActionResult Save(TimeSheetViewModel formData)
        {
            return Ok(_timeSheet.Save(formData, _token.Email));
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