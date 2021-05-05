using RepairCompanyManagement.BusinessLogic;
using RepairCompanyManagement.WebUI.Enums;
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

    
        [Display(Name = "Customer name")]
        public string CustomerName { get; set; }
        public int CustomersId { get; set; }

       
        [Display(Name = "Order status")]
        public OrderStatus OrderStatus { get; set; }
        [Display(Name = "Requirements")]
        public string Requirements { get; set; }
    }
}