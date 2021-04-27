using RepairCompanyManagement.BusinessLogic.Dtos;
using RepairCompanyManagement.DataAccess.Entities;
using System.Collections.Generic;

namespace RepairCompanyManagement.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        int CreateBrigade(BrigadeDto item);

        BrigadeDto GetBrigadeById(int id);

        IReadOnlyCollection<BrigadeDto> GetAllBrigades();

        void UpdateBrigade(BrigadeDto item);

        void DeleteBrigade(int id);
          JobPositionDto GetJobPositionById(int id);

        IReadOnlyCollection<JobPositionDto> GetAllJobPositions();

        void UpdateJobPosition(JobPositionDto item);

        void DeleteJobPosition(int id);

        void ValidateJobPosition(JobPositionDto item);

        int CreateManager(ManagerDto item);

        ManagerDto GetManagerById(int id);

        IReadOnlyCollection<ManagerDto> GetAllManagers();

        void UpdateManager(ManagerDto item);

        void DeleteManager(int id);

        void ValidateManager(ManagerDto item);
        (string, string) GetEmployeeFullInfo(string id);
        int CreateEmployee(EmployeeDto item);

        EmployeeDto GetEmployeeById(int id);

        IReadOnlyCollection<EmployeeDto> GetAllEmployees();

        void UpdateEmployee(EmployeeDto item);

        void DeleteEmployee(int id);

        int CreateCustomer(CustomerDto item);

        CustomerDto GetCustomerById(int id);

        IReadOnlyCollection<CustomerDto> GetAllCustomers();

        void UpdateCustomer(CustomerDto item);

        void DeleteCustomer(int id);

        void ValidateCustomer(CustomerDto item);
    }
}
