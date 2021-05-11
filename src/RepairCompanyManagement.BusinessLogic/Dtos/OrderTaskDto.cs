using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCompanyManagement.BusinessLogic.Dtos
{
    public class OrderTaskDto
    {
        public int Id { get; set; }
        public int IdTask { get; set; }
        public int IdOrder { get; set; }
        public DateTimeOffset TaskCompletionDate { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
    }
}
