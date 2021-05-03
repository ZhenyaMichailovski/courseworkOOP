using RepairCompanyManagement.BusinessLogic;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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

        public IEnumerable<SpecializationItem> SpecializationItems { get; set; }
        [Display(Name = "Specialization")]
        public string SpecializationName { get; set; }
    }
}