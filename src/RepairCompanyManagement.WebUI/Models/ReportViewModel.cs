using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairCompanyManagement.WebUI.Models
{
    public class ReportViewModel
    {
        public string Report { get; set; }
        public string CustomerName { get; set;}
        public int OrderId { get; set; }
    }
}