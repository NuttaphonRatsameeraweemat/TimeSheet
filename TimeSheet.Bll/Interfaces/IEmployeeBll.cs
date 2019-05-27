using System;
using System.Collections.Generic;
using System.Text;
using TimeSheet.Bll.Models;

namespace TimeSheet.Bll.Interfaces
{
    public interface IEmployeeBll
    {
        IEnumerable<EmployeeViewModel> GetAll();
    }
}
