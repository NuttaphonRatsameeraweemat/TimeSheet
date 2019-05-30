using System;
using System.Collections.Generic;
using System.Text;
using TimeSheet.Bll.Models;
using TimeSheet.Data.Pocos;

namespace TimeSheet.Bll.Interfaces
{
    public interface IDashBoardBll
    {
        /// <summary>
        /// Get Summary stat of project type total working.
        /// </summary>
        /// <param name="email">The employee email.</param>
        /// <returns></returns>
        List<DashBoardViewModel.ProjectTypeWorkingStat> GetProjectTypeStat(string email);
    }
}
