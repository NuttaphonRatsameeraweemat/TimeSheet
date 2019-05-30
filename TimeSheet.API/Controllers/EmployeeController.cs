using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeSheet.Bll.Interfaces;

namespace TimeSheet.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {

        #region Fields

        /// <summary>
        /// The Employee manager provides Login functionality.
        /// </summary>
        private readonly IEmployeeBll _employee;

        #endregion

        #region Constructors

        /// <summary>
        ///  Initializes a new instance of the <see cref="EmployeeController" /> class.
        /// </summary>
        /// <param name="employee"></param>
        public EmployeeController(IEmployeeBll employee)
        {
            _employee = employee;
        }

        #endregion

        #region Methods

        [HttpGet]
        [Route("GetEmployeeList")]
        public IActionResult GetEmployeeList()
        {
            return Ok(_employee.GetAll());
        }

        #endregion

    }
}