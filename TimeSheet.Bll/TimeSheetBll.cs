using System;
using System.Collections.Generic;
using System.Text;
using TimeSheet.Bll.Interfaces;
using TimeSheet.Bll.Models;

namespace TimeSheet.Bll
{
    public class TimeSheetBll : ITimeSheetBll
    {

        #region [Fields]


        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSheetBll" /> class.
        /// </summary>
        public TimeSheetBll()
        {

        }

        #endregion

        #region [Methods]

        /// <summary>
        /// Get TimeSheet information by employee no.
        /// </summary>
        /// <param name="empNo">The employee no.</param>
        /// <returns></returns>
        public TimeSheetViewModel Get(string empNo, int month)
        {
            return new TimeSheetViewModel
            {
                TimeSheet = new List<TimeSheetModel>
                {
                    new TimeSheetModel{
                        DateTimeStamp = new DateTime(2019,5,1).ToString("yyyy-MM-dd"),
                        TaskList = new List<TaskListModel>{
                            new TaskListModel{ ProjectCode = "P001", TypeCode = "T001", Description = "Change Requirement Logic BS", WorkingHours = 2 },
                            new TaskListModel{ ProjectCode = "P002", TypeCode = "T002", Description = "Setup Project", WorkingHours = 6 }
                        }
                    },
                    new TimeSheetModel{
                        DateTimeStamp = new DateTime(2019,5,2).ToString("yyyy-MM-dd"),
                        TaskList = new List<TaskListModel>{
                            new TaskListModel{ ProjectCode = "P001", TypeCode = "T001", Description = "Change Requirement Logic CA", WorkingHours = 2 },
                            new TaskListModel{ ProjectCode = "P002", TypeCode = "T002", Description = "Setup SmartObject", WorkingHours = 6 }
                        }
                    },
                    new TimeSheetModel{
                        DateTimeStamp = new DateTime(2019,5,3).ToString("yyyy-MM-dd"),
                        TaskList = new List<TaskListModel>{
                            new TaskListModel{ ProjectCode = "P001", TypeCode = "T001", Description = "Change Requirement Logic PV", WorkingHours = 2 },
                            new TaskListModel{ ProjectCode = "P002", TypeCode = "T002", Description = "Setup SmartForm", WorkingHours = 6 }
                        }
                    },
                    new TimeSheetModel{
                        DateTimeStamp = new DateTime(2019,5,6).ToString("yyyy-MM-dd"),
                        TaskList = new List<TaskListModel>{
                            new TaskListModel{ ProjectCode = "P001", TypeCode = "T001", Description = "Fixed Issue", WorkingHours = 2 },
                            new TaskListModel{ ProjectCode = "P002", TypeCode = "T002", Description = "Setup Workflow", WorkingHours = 6 }
                        }
                    },
                    new TimeSheetModel{
                        DateTimeStamp = new DateTime(2019,5,7).ToString("yyyy-MM-dd"),
                        TaskList = new List<TaskListModel>{
                            new TaskListModel{ ProjectCode = "P001", TypeCode = "T001", Description = "Fixed Data Dup", WorkingHours = 2 },
                            new TaskListModel{ ProjectCode = "P002", TypeCode = "T002", Description = "Testing", WorkingHours = 6 }
                        }
                    }
                }
            };
        }

        #endregion

    }
}
