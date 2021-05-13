using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCompanyManagement.BusinessLogic.Dtos
{
    public class FeedbackDto
    {
        public int Id { get; set; }
        public int IdOrder { get; set; }
        public string Review { get; set; }

    }
}
