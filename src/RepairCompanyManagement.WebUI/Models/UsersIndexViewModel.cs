using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RepairCompanyManagement.WebUI.Identity;

namespace RepairCompanyManagement.WebUI.Models
{
    public class UsersIndexViewModel
    {
        public IList<User> Managers { get; set; }
        public IList<User> Customers { get; set; }
        public IList<EmployeeFullInfoViewModel> Employee { get; set;}
    }
}