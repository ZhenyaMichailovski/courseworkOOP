using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairCompanyManagement.WebUI.Models
{
    public class IndexProfileViewModel
    {
        public string Surname { get; set; }

        public string FirstName { get; set; }

        public string Email { get; set; }

        public decimal Balance { get; set; }

        public decimal Salary { get; set; }
    }
}