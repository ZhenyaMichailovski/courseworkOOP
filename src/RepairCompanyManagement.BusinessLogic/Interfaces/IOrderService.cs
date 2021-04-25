using RepairCompanyManagement.BusinessLogic.Dtos;
using RepairCompanyManagement.DataAccess.Entities;
using System.Collections.Generic;

namespace RepairCompanyManagement.BusinessLogic.Interfaces
{
    public interface IOrderService
    {
        int CreateManager(ManagerDto item);

        ManagerDto GetManagerById(int id);

        IReadOnlyCollection<ManagerDto> GetAllManagers();

        void UpdateManager(ManagerDto item);

        void DeleteManager(int id);

        void ValidateManager(ManagerDto item);

        ////////////////////////
        

        /////////////////////
        int CreateCustomer(CustomerDto item);

        CustomerDto GetCustomerById(int id);

        IReadOnlyCollection<CustomerDto> GetAllCustomers();

        void UpdateCustomer(CustomerDto item);

        void DeleteCustomer(int id);

        void ValidateCustomer(CustomerDto item);
        int CreateBrigade(BrigadeDto item);

        BrigadeDto GetBrigadeById(int id);

        IReadOnlyCollection<BrigadeDto> GetAllBrigades();

        void UpdateBrigade(BrigadeDto item);

        void DeleteBrigade(int id);

        int CreateOrder(OrderDto item);

        OrderDto GetOrderById(int id);

        IReadOnlyCollection<OrderDto> GetAllOrders();

        void UpdateOrder(OrderDto item);

        void DeleteOrder(int id);

        void ValidateOrder(OrderDto item);
    }
}
