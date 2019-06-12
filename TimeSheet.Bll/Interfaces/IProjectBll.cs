using System;
using System.Collections.Generic;
using System.Text;
using TimeSheet.Bll.Models;
using TimeSheet.Helper.Models;

namespace TimeSheet.Bll.Interfaces
{
    public interface IProjectBll
    {
        /// <summary>
        /// Insert new project to table project.
        /// </summary>
        /// <param name="formData">The project information.</param>
        /// <returns></returns>
        ResultViewModel Save(ProjectViewModel formData);
        /// <summary>
        /// Update exits project information.
        /// </summary>
        /// <param name="formData">The project information.</param>
        /// <returns></returns>
        ResultViewModel Update(ProjectViewModel formData);
        /// <summary>
        /// Delete project to table.
        /// </summary>
        /// <param name="formData">The project information.</param>
        /// <returns></returns>
        ResultViewModel Delete(ProjectViewModel formData);
        /// <summary>
        /// Get Project Detail and Information. 
        /// </summary>
        /// <param name="id">The Identity project.</param>
        /// <returns></returns>
        ProjectViewModel Get(string projectCode);
        /// <summary>
        /// Get Project List.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ProjectViewModel> GetList();
        /// <summary>
        /// Get Project List Status Active only.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ValueHelpViewModel> GetListActive();
    }
}
