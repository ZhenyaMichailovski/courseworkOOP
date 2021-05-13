using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairCompanyManagement.WebUI.Models
{
    public class FeedbackViewModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Review { get; set; }
    }
}