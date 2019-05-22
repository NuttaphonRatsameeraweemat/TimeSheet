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

        #endregion

        #region Constructors

        /// <summary>
        ///  Initializes a new instance of the <see cref="TimeSheetController" /> class.
        /// </summary>
        /// <param name="login"></param>
        public TimeSheetController(ITimeSheetBll timeSheet)
        {
            _timeSheet = timeSheet;
        }

        #endregion

        #region Methods

        [HttpGet]
        public IActionResult Get(int month)
        {
            return Ok(_timeSheet.Get("", month));
        }

        [HttpPost]
        public IActionResult Submit(TimeSheetViewModel fromData)
        {
            return Ok();
        }

        #endregion

    }
}