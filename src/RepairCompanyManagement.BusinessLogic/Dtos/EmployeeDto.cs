﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCompanyManagement.BusinessLogic.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public int IdBrigade { get; set; }
        public double Salary { get; set; }
        public int IdJobPosition { get; set; }
        public string IdentityUserID { get; set; }
    }
}
