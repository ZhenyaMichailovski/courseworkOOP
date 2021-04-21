using RepairCompanyManagement.BusinessLogic;
using System.ComponentModel.DataAnnotations;

namespace RepairCompanyManagement.WebUI.Models
{
    public class OrderTaskViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = Constants.EmptyOrderTaskTitleMessage)]
        public int IdTask { get; set; }

        [Required(ErrorMessage = Constants.EmptyOrderTaskTitleMessage)]
        public int IdOrder { get; set; }
    }
}