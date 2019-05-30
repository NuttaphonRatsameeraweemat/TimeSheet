using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeSheet.Bll.Interfaces;
using TimeSheet.Bll.Models;

namespace TimeSheet.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class RoleController : ControllerBase
    {

        #region Fields

        /// <summary>
        /// The Role manager provides role functionality.
        /// </summary>
        private IRoleBll _role;

        #endregion

        #region Constructors

        /// <summary>
        ///  Initializes a new instance of the <see cref="RoleController" /> class.
        /// </summary>
        /// <param name="role"></param>
        public RoleController(IRoleBll role)
        {
            _role = role;
        }

        #endregion

        #region Methods

        [HttpGet]
        public IActionResult Get(int id)
        {
            return Ok(_role.Get(id));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetList")]
        public IActionResult GetList()
        {
            return Ok(_role.GetList());
        }
        
        [HttpPost]
        [Route("Save")]
        public IActionResult Save(RoleViewModel formData)
        {
            return Ok(_role.Save(formData));
        }

        [HttpPost]
        [Route("Edit")]
        public IActionResult Edit(RoleViewModel formData)
        {
            return Ok(_role.Update(formData));
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(RoleViewModel formData)
        {
            return Ok(_role.Delete(formData));
        }

        #endregion

    }
}