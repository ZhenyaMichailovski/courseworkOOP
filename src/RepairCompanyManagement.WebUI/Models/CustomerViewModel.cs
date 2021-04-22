using RepairCompanyManagement.BusinessLogic;
using System.ComponentModel.DataAnnotations;

namespace RepairCompanyManagement.WebUI.Models
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = Constants.EmptyJobPositionTitleMessage)]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = Constants.EmptyJobPositionTitleMessage)]
        public string IdentityUserID { get; set; }
    }
}