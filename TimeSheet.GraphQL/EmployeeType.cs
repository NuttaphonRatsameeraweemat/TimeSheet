using GraphQL.Types;
using TimeSheet.Data.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeSheet.GraphQL
{
    public class EmployeeType : ObjectGraphType<Employee>
    {
        public EmployeeType()
        {
            Field(x => x.Email).Description("Id property from the owner object.");
            Field(x => x.FirstName).Description("FirstName property from the employee object.");
            Field(x => x.LastName).Description("LastName property from the employee object.");
            Field(x => x.TelNo).Description("TelNo property from the employee object.");
        }
    }
}
