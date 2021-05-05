using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairCompanyManagement.DataAccess.Enums;

namespace RepairCompanyManagement.BusinessLogic.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int IdCustomers { get; set; }
        public int OrderStatus { get; set; }
        public string Requirements { get; set; }
    }
}
