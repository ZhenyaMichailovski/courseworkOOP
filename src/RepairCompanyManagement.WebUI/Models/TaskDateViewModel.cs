using System;
using RepairCompanyManagement.BusinessLogic;
using System.ComponentModel.DataAnnotations;
using RepairCompanyManagement.WebUI.Enums;
using System.Collections.Generic;

namespace RepairCompanyManagement.WebUI.Models
{
    public class TaskDateViewModel
    {
        public int OrderId { get; set; }
        public int TaskId { get; set; }
        public int SpecializationId { get; set; }
        public List<List<(DateTimeOffset, bool)>> AllowedDays { get; set; }
        public string Description { get; set; }
    }
}