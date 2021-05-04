﻿using RepairCompanyManagement.BusinessLogic;
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
        public string Brigade { get; set; }

        [Required(ErrorMessage = Constants.EmptyOrderTitleMessage)]
        public string Customers { get; set; }

        [Required(ErrorMessage = Constants.EmptyOrderTitleMessage)]
        public string OrderStatus { get; set; }
        public string Requirements { get; set; }
    }
}