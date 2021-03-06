﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeSheet.Data.Pocos
{
    public partial class TimeSheet
    {
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(255)]
        public string Email { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateTimeStamp { get; set; }
    }
}
