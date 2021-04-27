using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using RepairCompanyManagement.WebUI.Identity;

namespace RepairCompanyManagement.WebUI.Models
{
    public class EmployeeFullInfoViewModel : IdentityUser
    {
        public string Surname { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Balance { get; set; }

        public string BrigadeName { get; set; }
        public string JobPositionName { get; set; }
    }
}