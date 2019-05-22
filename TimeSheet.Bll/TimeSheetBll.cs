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
                TaskList = new List<TaskModel>
                {
                    new TaskModel
                    {
                        Project = "DS",
                        Type = new List<TypeModel>
                        {
                            new TypeModel
                            {
                                TypeName = "Support",
                                TimeStamp = new List<TimeStampModel>
                                {
                                    new TimeStampModel{ DateTimeStamp = new DateTime(2019,5,1), WorkingHours = 2 },
                                    new TimeStampModel{ DateTimeStamp = new DateTime(2019,5,2), WorkingHours = 2 },
                                    new TimeStampModel{ DateTimeStamp = new DateTime(2019,5,3), WorkingHours = 2 },
                                    new TimeStampModel{ DateTimeStamp = new DateTime(2019,5,4), WorkingHours = 2 },
                                    new TimeStampModel{ DateTimeStamp = new DateTime(2019,5,5), WorkingHours = 2 }
                                }
                            }
                        }
                    },
                    new TaskModel
                    {
                        Project = "SmartForm",
                        Type = new List<TypeModel>
                        {
                            new TypeModel
                            {
                                TypeName = "POC",
                                TimeStamp = new List<TimeStampModel>
                                {
                                    new TimeStampModel{ DateTimeStamp = new DateTime(2019,5,1), WorkingHours = 6 },
                                    new TimeStampModel{ DateTimeStamp = new DateTime(2019,5,2), WorkingHours = 6 },
                                    new TimeStampModel{ DateTimeStamp = new DateTime(2019,5,3), WorkingHours = 6 },
                                    new TimeStampModel{ DateTimeStamp = new DateTime(2019,5,4), WorkingHours = 6 },
                                    new TimeStampModel{ DateTimeStamp = new DateTime(2019,5,5), WorkingHours = 6 }
                                }
                            }
                        }
                    }
                }
            };
        }

        #endregion

    }
}
