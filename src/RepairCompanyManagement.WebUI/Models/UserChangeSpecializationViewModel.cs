using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairCompanyManagement.WebUI.Models
{
    public class UserChangeSpecializationViewModel
    {
        public string UserId { get; set; }
        public IList<(int, string)> Specializations { get; set; }
    }
}