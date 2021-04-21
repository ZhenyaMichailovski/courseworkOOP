using RepairCompanyManagement.BusinessLogic;
using System.ComponentModel.DataAnnotations;

namespace RepairCompanyManagement.WebUI.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = Constants.EmptyOrderTitleMessage)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = Constants.EmptyOrderTitleMessage)]
        public int IdBrigade { get; set; }

        [Required(ErrorMessage = Constants.EmptyOrderTitleMessage)]
        public int IdCustomers { get; set; }

        [Required(ErrorMessage = Constants.EmptyOrderTitleMessage)]
        public int IdManager { get; set; }

        [Required(ErrorMessage = Constants.EmptyOrderTitleMessage)]
        public int IdTask { get; set; }

        [Required(ErrorMessage = Constants.EmptyOrderTitleMessage)]
        public string OrderStatus { get; set; }
        public string Requirements { get; set; }
    }
}