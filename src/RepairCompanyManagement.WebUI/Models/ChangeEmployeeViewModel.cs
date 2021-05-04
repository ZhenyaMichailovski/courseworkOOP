using RepairCompanyManagement.BusinessLogic;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RepairCompanyManagement.WebUI.Models
{
    public class ChangeEmployeeViewModel
    {
        public List<BrigadeItem> Brigades { get; set; } 
        public List<JobPositionItem> JobPositions { get; set; }
        public int BrigadeId { get; set; }
        public int JobPositionId { get; set; }

        public string IdentityUserId { get; set; }
    }
}