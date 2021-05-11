using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RepairCompanyManagement.WebUI.Enums;

namespace RepairCompanyManagement.WebUI.Models
{
    public class WorkloadViewModel
    {
        public int IdBrigade { get; set; }
        public string NameBrigade { get; set; }
        public IList<OrderTaskItem> Item { get; set; }

    }
}