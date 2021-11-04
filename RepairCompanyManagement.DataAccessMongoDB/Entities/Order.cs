using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCompanyManagement.DataAccessMongoDB.Entities
{
    public class Order
    {
        public string Title { get; set; }
        public Customer Customer { get; set; }
        public List<OrderTask> OrderTask { get; set; }
        public string Requirements { get; set; }
        public string Review { get; set; }
    }
}
