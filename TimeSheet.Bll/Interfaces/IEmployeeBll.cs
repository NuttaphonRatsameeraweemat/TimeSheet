using System;
using System.Collections.Generic;
using System.Text;
using TimeSheet.Bll.Models;

namespace TimeSheet.Bll.Interfaces
{
    public interface IEmployeeBll
    {
        /// <summary>
        /// Get All Employye Information.
        /// </summary>
        /// <returns></returns>
        IEnumerable<EmployeeViewModel> GetAll();
    }
}
