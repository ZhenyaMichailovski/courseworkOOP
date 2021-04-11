using RepairCompanyManagement.BusinessLogic;
using System.ComponentModel.DataAnnotations;

namespace RepairCompanyManagement.WebUI.Models
{
    public class SpecializationViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = Constants.EmptySpecializationNameMessage)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}