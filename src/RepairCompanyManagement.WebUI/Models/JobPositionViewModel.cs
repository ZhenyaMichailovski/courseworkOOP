using RepairCompanyManagement.BusinessLogic;
using System.ComponentModel.DataAnnotations;

namespace RepairCompanyManagement.WebUI.Models
{
    public class JobPositionViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = Constants.EmptyJobPositionTitleMessage)]
        [Display(Name = "Name")]
        public string Title { get; set; }

        [Display(Description = "Description")]
        public string Purpose { get; set; }
    }
}