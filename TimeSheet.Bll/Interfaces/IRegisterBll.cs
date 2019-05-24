using System;
using System.Collections.Generic;
using System.Text;
using TimeSheet.Bll.Models;

namespace TimeSheet.Bll.Interfaces
{
    public interface IRegisterBll
    {
        /// <summary>
        /// The Register new employee function.
        /// </summary>
        /// <param name="formData">The employee data.</param>
        /// <returns></returns>
        ResultViewModel Register(RegisterViewModel formData);
    }
}
