using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCompanyManagement.DataAccessMongoDB.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        
        public string Description { get; set; }
        public Specialization Specialization { get; set; }
        public Brigade Brigade { get; set; }
    }
}
