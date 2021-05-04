using RepairCompanyManagement.BusinessLogic;
using System.ComponentModel.DataAnnotations;

namespace RepairCompanyManagement.WebUI.Models
{
    public class MyOrdersViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = Constants.EmptyOrderTitleMessage)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = Constants.EmptyOrderTitleMessage)]
        [Display(Name = "Order Status")]
        public string OrderStatus { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Requirements")]
        public string Requirements { get; set; }
    }
}
