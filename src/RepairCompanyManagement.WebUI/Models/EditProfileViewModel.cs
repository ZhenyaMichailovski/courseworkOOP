using RepairCompanyManagement.BusinessLogic;
using System.ComponentModel.DataAnnotations;

namespace RepairCompanyManagement.WebUI.Models
{
    public class EditProfileViewModel
    {
        [Required(ErrorMessage = "Surname can't be empty")]
        [DataType(DataType.Text)]
         public string Surname { get; set; }

        [Required(ErrorMessage = "First name can't be empty")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Balance { get; set; }

        [Required(ErrorMessage = "Email can't be empty")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}