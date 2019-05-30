using System;
using System.Collections.Generic;
using System.Text;
using TimeSheet.Bll.Models;

namespace TimeSheet.Bll.Interfaces
{
    public interface IRoleBll
    {

        /// <summary>
        /// Insert new role to table role.
        /// </summary>
        /// <param name="formData">The role information.</param>
        /// <returns></returns>
        ResultViewModel Save(RoleViewModel formData);
        /// <summary>
        /// Update exits role information.
        /// </summary>
        /// <param name="formData">The role information.</param>
        /// <returns></returns>
        ResultViewModel Update(RoleViewModel formData);
        /// <summary>
        /// Delete role to table.
        /// </summary>
        /// <param name="formData">The role information.</param>
        /// <returns></returns>
        ResultViewModel Delete(RoleViewModel formData);
        /// <summary>
        /// Get Role Detail and Information. 
        /// </summary>
        /// <param name="id">The Identity role.</param>
        /// <returns></returns>
        RoleViewModel Get(int id);
        /// <summary>
        /// Get Role List.
        /// </summary>
        /// <returns></returns>
        IEnumerable<RoleViewModel> GetList();

    }
}
