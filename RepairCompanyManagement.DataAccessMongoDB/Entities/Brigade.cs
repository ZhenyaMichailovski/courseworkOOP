using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCompanyManagement.DataAccessMongoDB.Entities
{
    public class Brigade
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Specialization Specialization { get; set; }
    }
}
