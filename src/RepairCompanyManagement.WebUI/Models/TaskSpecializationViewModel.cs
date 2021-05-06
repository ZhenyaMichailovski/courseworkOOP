using System;
using RepairCompanyManagement.BusinessLogic;
using System.ComponentModel.DataAnnotations;
using RepairCompanyManagement.WebUI.Enums;
using System.Collections.Generic;

namespace RepairCompanyManagement.WebUI.Models
{
    public class TaskSpecializationViewModel
    {

        public int OrderId { get; set; }
        [Required()]
        public int IdSpecialization { get; set; }
        public IEnumerable<SpecializationItem> SpecializationItems { get; set; }
    }
}