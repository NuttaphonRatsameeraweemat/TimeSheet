﻿using System;
using System.Collections.Generic;
using System.Text;
using TimeSheet.Bll.Models;

namespace TimeSheet.Bll.Interfaces
{
    public interface ITimeSheetBll
    {
        TimeSheetViewModel Get(string empNo, int month);
    }
}