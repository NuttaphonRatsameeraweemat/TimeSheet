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
    public class ProjectController : ControllerBase
    {

        #region Fields

        /// <summary>
        /// The Project manager provides project functionality.
        /// </summary>
        private IProjectBll _project;

        #endregion

        #region Constructors

        /// <summary>
        ///  Initializes a new instance of the <see cref="ProjectController" /> class.
        /// </summary>
        /// <param name="project"></param>
        public ProjectController(IProjectBll project)
        {
            _project = project;
        }

        #endregion

        #region Methods

        [HttpGet]
        public IActionResult Get(string projectCode)
        {
            return Ok(_project.Get(projectCode));
        }

        [HttpGet]
        [Route("GetList")]
        public IActionResult GetList()
        {
            return Ok(_project.GetList());
        }

        [HttpGet]
        [Route("GetListActive")]
        public IActionResult GetListActive()
        {
            return Ok(_project.GetListActive());
        }

        [HttpPost]
        [Route("Save")]
        public IActionResult Save(ProjectViewModel formData)
        {
            return Ok(_project.Save(formData));
        }

        [HttpPost]
        [Route("Edit")]
        public IActionResult Edit(ProjectViewModel formData)
        {
            return Ok(_project.Update(formData));
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(ProjectViewModel formData)
        {
            return Ok(_project.Delete(formData));
        }

        #endregion

    }
}