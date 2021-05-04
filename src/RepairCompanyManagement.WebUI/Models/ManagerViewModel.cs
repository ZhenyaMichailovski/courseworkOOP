using System;
using RepairCompanyManagement.BusinessLogic;
using System.ComponentModel.DataAnnotations;

namespace RepairCompanyManagement.WebUI.Models
{
    public class ManagerViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = Constants.EmptyManagerTitleMessage)]
        public DateTimeOffset DateOfBirth { get; set; }

        [Required(ErrorMessage = Constants.EmptyManagerTitleMessage)]
        public string Address { get; set; }

        [Required(ErrorMessage = Constants.EmptyManagerTitleMessage)]
        public double Salary { get; set; }

        [Required(ErrorMessage = Constants.EmptyManagerTitleMessage)]
        public string IdentityUserID { get; set; }
    }
}