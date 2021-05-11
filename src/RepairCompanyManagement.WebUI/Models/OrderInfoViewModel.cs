using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairCompanyManagement.WebUI.Models
{
    public class OrderInfoViewModel : OrderViewModel
    {
        public decimal TotalPrice { get; set; }
        public decimal MoneyOfUser { get; set; }
        public IList<TaskViewModel> Tasks { get; set; }
    }
}