using System;
using System.Collections.Generic;
using System.Text;

namespace TimeSheet.Bll.Components
{
    public static class ConstantValue
    {
        //Template format.
        public const string EMP_TEMPLATE = "{0} {1}";
        //DateTime format
        public const string DATE_TIME_FORMAT = "yyyy-MM-dd";
        //Claims Type
        public const string CLAMIS_NAME = "EmpName";
        public const string CLAMIS_ROLE = "EmpRole";
        //ValueHelp Type
        public const string VALUE_PROJECT_TYPE = "TYPE_PROJECT";
        public const string VALUE_PROJECT_STATUS = "PROJECT_STATUS";
        //Project Status
        public const string PROJECT_STATUS_ACTIVE = "ACTIVE";
        public const string PROJECT_STATUS_COMPLETE = "COMPLETE";
        //Regular expresstion format date
        public const string REGEX_DATE_FORMAT = @"^[0-9]{4}-[0-9]{2}-[0-9]{2}";
        //Error Message
        public const string ERR_DATE_INCORRECT_FORMAT = "The date value can't be empty and support only 'yyyy-MM-dd' format.";
        public const string ERR_EMAIL_IS_ALREADY_EXITS = "This email is already.";

    }
}
