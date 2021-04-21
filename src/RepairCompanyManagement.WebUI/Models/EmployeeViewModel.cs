using RepairCompanyManagement.BusinessLogic;
using System.ComponentModel.DataAnnotations;

namespace RepairCompanyManagement.WebUI.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = Constants.EmptyEmployeeTitleMessage)]
        public int IdBrigade { get; set; }

        [Required(ErrorMessage = Constants.EmptyEmployeeTitleMessage)]
        public double Salary { get; set; }

        [Required(ErrorMessage = Constants.EmptyEmployeeTitleMessage)]
        public int IdJobPosition { get; set; }

        [Required(ErrorMessage = Constants.EmptyEmployeeTitleMessage)]
        public string IdentityUserID { get; set; }
    }
}