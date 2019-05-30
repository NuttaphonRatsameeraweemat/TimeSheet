using System;
using System.Collections.Generic;
using System.Text;

namespace TimeSheet.Bll.Models
{
    public class DashBoardViewModel
    {
        public class ProjectTypeWorkingStat
        {
            public string ProjectName { get; set; }
            public List<TypeStat> TypeStat { get; set; }
        }

        public class TypeStat
        {
            public string TypeName { get; set; }
            public int TotalWorking { get; set; }
        }

    }
}
