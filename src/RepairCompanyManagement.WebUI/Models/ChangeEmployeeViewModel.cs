using RepairCompanyManagement.BusinessLogic;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RepairCompanyManagement.WebUI.Models
{
    public class ChangeEmployeeViewModel
    {
        public List<BrigadeItem> Brigades { get; set; } 
        public List<JobPositionItem> JobPositions { get; set; }

        [Display(Description = "Brigade")]
        public int BrigadeId { get; set; }
        [Display(Description = "Job Postition")]
        public int JobPositionId { get; set; }

        public string IdentityUserId { get; set; }
    }
}