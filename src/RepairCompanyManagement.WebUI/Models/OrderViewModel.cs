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
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = Constants.EmptyOrderTitleMessage)]
        [Display(Name = "Customers")]
        public string CustomerName { get; set; }
        public int CustomersId { get; set; }

        [Required(ErrorMessage = Constants.EmptyOrderTitleMessage)]
        [Display(Name = "Order status")]
        public string OrderStatus { get; set; }
        [Display(Name = "Requirements")]
        public string Requirements { get; set; }
    }
}