using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCompanyManagement.BusinessLogic.Dtos
{
    class OrderTaskDto
    {
        public int Id { get; set; }
        public int IdTask { get; set; }
        public int IdOrder { get; set; }
    }
}
