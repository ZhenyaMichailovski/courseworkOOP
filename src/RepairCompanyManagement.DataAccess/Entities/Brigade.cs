using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCompanyManagement.DataAccess.Entities
{
    public class Brigade
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int IdSpecialization { get; set; }
    }
}
