﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCompanyManagement.DataAccess.Entities
{
    public class Feedback
    {
        public int Id { get; set; }
        public int IdOrder { get; set; }
        public string Review { get; set; }
    }
}