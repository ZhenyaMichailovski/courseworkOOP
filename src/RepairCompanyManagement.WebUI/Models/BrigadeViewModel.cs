using RepairCompanyManagement.BusinessLogic;
using System.ComponentModel.DataAnnotations;

namespace RepairCompanyManagement.WebUI.Models
{
    public class BrigadeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = Constants.BrigadeEmptyTitleMessage)]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = Constants.BrigadeEmptyTitleMessage)]
        public int IdSpecialization { get; set; }
    }
}