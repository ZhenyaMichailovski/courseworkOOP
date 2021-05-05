using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RepairCompanyManagement.DataAccess.Enums;

namespace RepairCompanyManagement.DataAccess.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int IdSpecialization { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTimeOffset TaskCompletionDate { get; set; }
        public int IdBrigade { get; set; }
        public TaskStatus Status { get; set; }
    }
}
