using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCompanyManagement.BusinessLogic.Dtos
{
    public class BrigadeDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int IdSpecialization { get; set; }
    }
}
