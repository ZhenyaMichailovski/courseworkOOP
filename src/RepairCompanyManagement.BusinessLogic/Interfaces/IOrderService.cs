using RepairCompanyManagement.BusinessLogic.Dtos;
using RepairCompanyManagement.DataAccess.Entities;
using System.Collections.Generic;

namespace RepairCompanyManagement.BusinessLogic.Interfaces
{
    public interface IOrderService
    {
        ManagerDto GetManagerById(int id);

        IReadOnlyCollection<ManagerDto> GetAllManagers();


        ////////////////////////
        

        /////////////////////
       
        CustomerDto GetCustomerById(int id);

        IReadOnlyCollection<CustomerDto> GetAllCustomers();

        int CreateTask(TaskDto item);

        TaskDto GetTaskById(int id);

        IReadOnlyCollection<TaskDto> GetAllTasks();

        void UpdateTask(TaskDto item);

        void DeleteTask(int id);

        void ValidateTask(TaskDto item);


        int CreateOrder(OrderDto item);

        OrderDto GetOrderById(int id);

        IReadOnlyCollection<OrderDto> GetAllOrders();

        void UpdateOrder(OrderDto item);

        void DeleteOrder(int id);

        void ValidateOrder(OrderDto item);

        int CreateOrderTask(OrderTaskDto item);

        OrderTaskDto GetOrderTaskById(int id);

        IReadOnlyCollection<OrderTaskDto> GetAllOrderTasks();

        void UpdateOrderTask(OrderTaskDto item);

        void DeleteOrderTask(int id);


        SpecializationDto GetSpecializationById(int id);

        IReadOnlyCollection<SpecializationDto> GetAllSpecializations();

    }
}
