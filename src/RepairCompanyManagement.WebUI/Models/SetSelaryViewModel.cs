using RepairCompanyManagement.BusinessLogic;
using System.ComponentModel.DataAnnotations;

namespace RepairCompanyManagement.WebUI.Models
{
    public class SetSelaryViewModel
    {
        [Display(Name = "Selary")]
        public decimal Selary { get; set; }
        public string IdentityUserId { get; set; }
    }
}