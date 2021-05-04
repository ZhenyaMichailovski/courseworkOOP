using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCompanyManagement.DataAccess.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public int IdBrigade { get; set; }
        public decimal Salary { get; set; }
        public int IdJobPosition { get; set; }
        public string IdentityUserID { get; set; }
    }
}
