using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCompanyManagement.DataAccessMongoDB.Entities
{
    public class OrderTask
    {
        public int IdTask { get; set; }
        public DateTime TaskCompletionDate { get; set; }
        public string Status { get; set; }
        public string Decsription { get; set; }
    }
}
