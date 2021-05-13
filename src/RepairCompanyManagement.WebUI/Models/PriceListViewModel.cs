using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairCompanyManagement.WebUI.Models
{
    public class PriceListViewModel
    {
        public int IdTask { get; set; }
        public string Task { get; set; }
        public string Price { get; set; } 
    }
}