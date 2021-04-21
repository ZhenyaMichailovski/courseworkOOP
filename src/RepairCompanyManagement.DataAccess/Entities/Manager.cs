using System;

namespace RepairCompanyManagement.DataAccess.Entities
{
    class Manager
    {
        public int Id { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string Address { get; set; }
        public double Salary { get; set; }
        public int IdentituUserID { get; set; }
    }
}
