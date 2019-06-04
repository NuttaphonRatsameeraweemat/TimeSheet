using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;
using TimeSheet.Data.Pocos;
using TimeSheet.Data.Repository.Interfaces;

namespace TimeSheet.GraphQL
{
    public class AppQuery : ObjectGraphType
    {
        public AppQuery(IUnitOfWork unitOfWork)
        {
            Field<ListGraphType<EmployeeType>>(
               "employee",
               resolve: context => unitOfWork.GetRepository<Employee>().Get()
           );
        }
    }
}
