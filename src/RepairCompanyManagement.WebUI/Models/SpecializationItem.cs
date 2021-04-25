using RepairCompanyManagement.BusinessLogic;
using System.ComponentModel.DataAnnotations;

namespace RepairCompanyManagement.WebUI.Models
{
    public class SpecializationItem
    {
        public int SpecializationId { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}