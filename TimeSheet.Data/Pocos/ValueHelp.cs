﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeSheet.Data.Pocos
{
    public partial class ValueHelp
    {
        [StringLength(100)]
        public string ValueType { get; set; }
        [StringLength(100)]
        public string ValueKey { get; set; }
        [StringLength(100)]
        public string ValueText { get; set; }
    }
}
