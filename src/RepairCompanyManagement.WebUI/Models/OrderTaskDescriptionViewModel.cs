﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairCompanyManagement.WebUI.Models
{
    public class OrderTaskDescriptionViewModel
    {
        public int OrderId { get; set; }
        public int TaskId { get; set; }
        public int SpecId { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Description { get; set; }
    }
}