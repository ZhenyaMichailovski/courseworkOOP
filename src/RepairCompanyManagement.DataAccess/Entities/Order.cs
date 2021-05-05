using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairCompanyManagement.DataAccess.Enums;

namespace RepairCompanyManagement.DataAccess.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int IdCustomers { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string Requirements { get; set; }

    }
}
