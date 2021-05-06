using System;
using RepairCompanyManagement.BusinessLogic;
using System.ComponentModel.DataAnnotations;
using RepairCompanyManagement.WebUI.Enums;
using System.Collections.Generic;

namespace RepairCompanyManagement.WebUI.Models
{
    public class TaskItem
    {
        public int TaskId { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}