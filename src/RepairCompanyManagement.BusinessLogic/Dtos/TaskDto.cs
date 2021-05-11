using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RepairCompanyManagement.DataAccess.Enums;

namespace RepairCompanyManagement.BusinessLogic.Dtos
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int IdSpecialization { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int IdBrigade { get; set; }
    }
}
