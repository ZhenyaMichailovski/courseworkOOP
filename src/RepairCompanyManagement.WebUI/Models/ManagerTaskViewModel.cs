using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairCompanyManagement.WebUI.Models
{
    public class ManagerTaskViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int SpecializationId { get; set; }
        public string Specialization { get; set; }
        public IList<SpecializationItem> SpecializationItems { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int BrigadeId { get; set; }
        public string Brigade { get; set; }
        public IList<BrigadeItem> BrigadeItems { get; set; }
    }
}