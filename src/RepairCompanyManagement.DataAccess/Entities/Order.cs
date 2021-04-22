using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCompanyManagement.DataAccess.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int IdBrigade { get; set; }
        public int IdCustomers { get; set; }
        public int IdManager { get; set; }
        public int IdTask { get; set; }
        public string OrderStatus { get; set; }
        public string Requirements { get; set; }
    }
}
