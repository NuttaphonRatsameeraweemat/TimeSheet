using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeSheet.GraphQL
{
    public class TimeSheetType : ObjectGraphType<TimeSheet.Bll.Models.TimeSheetViewModel>
    {
        public TimeSheetType()
        {
            Field(x => x.TimeSheet, type: typeof(ListGraphType<TimeSheetModelType>)).Description("Enumeration for the timesheet object.");
        }
    }

    public class TimeSheetModelType : ObjectGraphType<TimeSheet.Bll.Models.TimeSheetModel>
    {
        public TimeSheetModelType()
        {
            Field(x => x.TimeSheetId, type: typeof(IdGraphType)).Description("TimeSheetId property from the timesheet object.");
            Field(x => x.DateTimeStamp).Description("DateTimeStamp property from the timesheet object.");
            Field(x => x.TaskList, type: typeof(ListGraphType<TaskListModelType>)).Description("Enumeration for the tasklist object.");
        }
    }

    public class TaskListModelType : ObjectGraphType<TimeSheet.Bll.Models.TaskListModel>
    {
        public TaskListModelType()
        {
            Field(x => x.Id, type: typeof(IdGraphType)).Description("Id property from the tasklist object.");
            Field(x => x.TimeSheetId, type: typeof(IdGraphType)).Description("TimeSheetId property from the tasklist object.");
            Field(x => x.ProjectCode).Description("ProjectCode property from the tasklist object.");
            Field(x => x.TypeCode).Description("TypeCode property from the tasklist object.");
            Field(x => x.WorkingHours).Description("WorkingHours property from the tasklist object.");
            Field(x => x.Description).Description("Description property from the tasklist object.");
        }
    }

}
