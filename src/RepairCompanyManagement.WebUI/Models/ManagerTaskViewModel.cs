using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RepairCompanyManagement.WebUI.Models
{
    public class ManagerTaskViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [Display(Name = "Specialization")]
        public int SpecializationId { get; set; }

        [Display(Name = "Specialization")]
        public string Specialization { get; set; }

        [Display(Name = "Specialization")]
        public IList<SpecializationItem> SpecializationItems { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        [Display(Name = "Brigade")]
        public int BrigadeId { get; set; }

        [Display(Name = "Brigade")]
        public string Brigade { get; set; }

        [Display(Name = "Brigade")]
        public IList<BrigadeItem> BrigadeItems { get; set; }
    }
}