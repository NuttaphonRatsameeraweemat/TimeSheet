using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeSheet.Bll.Interfaces;
using TimeSheet.Data.Pocos;
using TimeSheet.Data.Repository.Interfaces;

namespace TimeSheet.GraphQL
{
    public class AppQuery : ObjectGraphType
    {
        public AppQuery(IUnitOfWork unitOfWork, ITimeSheetBll timeSheet)
        {
            Field<ListGraphType<EmployeeType>>(
               "employees",
               resolve: context => unitOfWork.GetRepository<Employee>().Get()
            );
            Field<ListGraphType<EmployeeType>>(
               "employee",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "employeeEmail" }),
                resolve: context =>
                {
                    var email = context.GetArgument<string>("employeeEmail");
                    return unitOfWork.GetRepository<Employee>().Get(x => x.Email == email);
                }
            );


            Field<TimeSheetType>(
               "timesheet",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "employeeEmail" },
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "date" }
                    ),
                resolve: context =>
                {
                    var email = context.GetArgument<string>("employeeEmail");
                    var date = context.GetArgument<string>("date");
                    return timeSheet.Get(email, date);
                }
            );

        }
    }
}
