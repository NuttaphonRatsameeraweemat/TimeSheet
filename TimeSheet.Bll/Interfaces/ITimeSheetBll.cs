using System;
using System.Collections.Generic;
using System.Text;
using TimeSheet.Bll.Models;

namespace TimeSheet.Bll.Interfaces
{
    public interface ITimeSheetBll
    {
        TimeSheetViewModel Get(string email, string date);
        ResultViewModel Save(TimeSheetViewModel formData, string email);
        ResultViewModel Update(TimeSheetViewModel formData, string email);
        ResultViewModel Delete(TimeSheetViewModel formData);
    }
}
