using System;
using System.Collections.Generic;
using System.Text;
using TimeSheet.Bll.Models;
using TimeSheet.Helper.Models;

namespace TimeSheet.Bll.Interfaces
{
    public interface ITimeSheetBll
    {

        /// <summary>
        /// Get TimeSheet information by employee email.
        /// </summary>
        /// <param name="email">The employee email.</param>
        /// <param name="date">The target month.</param>
        /// <returns></returns>
        TimeSheetViewModel Get(string email, string date);
        /// <summary>
        /// Insert new timesheet and tasklist information to database.
        /// </summary>
        /// <param name="formData">The information of timesheet and tasklist.</param>
        /// <param name="email">The owner timesheet and tasklist.</param>
        /// <returns></returns>
        ResultViewModel Save(TimeSheetViewModel formData, string email);
        /// <summary>
        /// Update TimeSheet and tasklist information to database.
        /// </summary>
        /// <param name="timeSheetList">The information of timesheet and tasklist.</param>
        /// <param name="email">The owner timesheet and tasklist.</param>
        /// <returns></returns>
        ResultViewModel Update(TimeSheetViewModel formData, string email);
        /// <summary>
        /// Delete TimeSheet and Tasklist.
        /// </summary>
        /// <param name="formData">The TimeSheet and Tasklist information.</param>
        /// <returns></returns>
        ResultViewModel Delete(TimeSheetViewModel formData);
        /// <summary>
        /// Validate date null or empty and regex is match or not.
        /// </summary>
        /// <param name="date">The date value.</param>
        /// <returns></returns>
        bool IsDateMatchRegex(string date);

    }
}
