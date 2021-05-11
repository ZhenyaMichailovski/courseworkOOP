using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RepairCompanyManagement.DataAccess.Enums;

namespace RepairCompanyManagement.DataAccess.Entities
{
    public class OrderTask
    {
        public int Id { get; set; }
        public int IdTask { get; set; }
        public int IdOrder { get; set; }
        public DateTimeOffset TaskCompletionDate { get; set; }
        public OrderTaskStatus Status { get; set; }
        public string Description { get; set; }
    }
}
