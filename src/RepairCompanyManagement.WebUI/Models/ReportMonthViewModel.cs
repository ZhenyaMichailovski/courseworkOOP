using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RepairCompanyManagement.WebUI.Models
{
    public class ReportMonthViewModel
    {
        [Display(Name = "Month")]
        public string MonthName { get; set; }

        [Display(Name = "Month")]
        public int MonthId { get; set; }

        public IList<Month> Months { get; set; }
    }
}