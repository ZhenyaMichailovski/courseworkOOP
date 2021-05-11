using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RepairCompanyManagement.WebUI.Enums;

namespace RepairCompanyManagement.WebUI.Models
{
    public class OrderTaskItem
    {
        public int IdOrderTask { get; set; }
        public string NameCustomers { get; set; }
        public string PhoneNumber { get; set; }
        public string NameTask { get; set; }
        public DateTimeOffset Date { get; set; }
        public OrderTaskStatus Status { get; set; }
        public string Description { get; set; }
    }
}