using RepairCompanyManagement.BusinessLogic.Dtos;
using RepairCompanyManagement.DataAccess.Entities;
using System.Collections.Generic;

namespace RepairCompanyManagement.BusinessLogic.Interfaces
{
    public interface IBrigadeService
    {
        int CreateSpecialization(SpecializationDto item);

        SpecializationDto GetSpecializationById(int id);

        IReadOnlyCollection<SpecializationDto> GetAllSpecializations();

        void UpdateSpecialization(SpecializationDto item);

        void DeleteSpecialization(int id);

        void ValidateSpecialization(SpecializationDto item);

        /////////////////////////////////
        int CreateManager(ManagerDto item);

        ManagerDto GetManagerById(int id);

        IReadOnlyCollection<ManagerDto> GetAllManagers();

        void UpdateManager(ManagerDto item);

        void DeleteManager(int id);

        void ValidateManager(ManagerDto item);
        // /// / / / / /// / / / / /  //  //  // 

        int CreateBrigade(BrigadeDto item);

        BrigadeDto GetBrigadeById(int id);

        IReadOnlyCollection<BrigadeDto> GetAllBrigades();

        void UpdateBrigade(BrigadeDto item);

        void DeleteBrigade(int id);

        // /// / / / / /// / / / / /  //  //  // 
        int CreateEmployee(EmployeeDto item);

        EmployeeDto GetEmployeeById(int id);

        IReadOnlyCollection<EmployeeDto> GetAllEmployees();

        void UpdateEmployee(EmployeeDto item);

        void DeleteEmployee(int id);
        // /// / / / / /// / / / / /  //  //  // 

        int CreateJobPosition(JobPositionDto item);

        JobPositionDto GetJobPositionById(int id);

        IReadOnlyCollection<JobPositionDto> GetAllJobPositions();

        void UpdateJobPosition(JobPositionDto item);

        void DeleteJobPosition(int id);

        void ValidateJobPosition(JobPositionDto item);
        /////////////////////////////////
        int CreateOrder(OrderDto item);

        OrderDto GetOrderById(int id);

        IReadOnlyCollection<OrderDto> GetAllOrders();

        void UpdateOrder(OrderDto item);

        void DeleteOrder(int id);

        void ValidateOrder(OrderDto item);
        /////////////////////////////////
        int CreateOrderTask(OrderTaskDto item);

        OrderTaskDto GetOrderTaskById(int id);

        IReadOnlyCollection<OrderTaskDto> GetAllOrderTasks();

        void UpdateOrderTask(OrderTaskDto item);

        void DeleteOrderTask(int id);

        void ValidateOrderTask(OrderTaskDto item);
        /////////////////////////////////
        int CreateTask(TaskDto item);

        TaskDto GetTaskById(int id);

        IReadOnlyCollection<TaskDto> GetAllTasks();

        void UpdateTask(TaskDto item);

        void DeleteTask(int id);

        void ValidateTask(TaskDto item);

        /////////////////////////////////
        int CreateCustomer(CustomerDto item);

        CustomerDto GetCustomerById(int id);

        IReadOnlyCollection<CustomerDto> GetAllCustomers();

        void UpdateCustomer(CustomerDto item);

        void DeleteCustomer(int id);

        void ValidateCustomer(CustomerDto item);
        // /// / / / / /// / / / / /  //  //  // 
    }
}
