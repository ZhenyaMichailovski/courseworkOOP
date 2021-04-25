using AutoMapper;
using RepairCompanyManagement.BusinessLogic.Dtos;
using RepairCompanyManagement.BusinessLogic.Exceptions;
using RepairCompanyManagement.BusinessLogic.Interfaces;
using RepairCompanyManagement.DataAccess.Entities;
using RepairCompanyManagement.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepairCompanyManagement.BusinessLogic.Services
{
    public class OrderService : IOrderService
    {
       
        private readonly IRepository<Manager> _managerRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Brigade> _brigadeRepository;

        private readonly IMapper _mapper;
        public OrderService(IRepository<Order> orderRepository, IRepository<Manager> managerRepository,
           IRepository<Customer> customerRepository, IRepository<Brigade> brigadeRepository, IMapper mapper)

        {
            _customerRepository = customerRepository;
            _managerRepository = managerRepository;
            _orderRepository = orderRepository;
            _brigadeRepository = brigadeRepository;
            _mapper = mapper;
        }
        public int CreateCustomer(CustomerDto item)
        {
            ValidateCustomer(item);

            return _customerRepository.Create(_mapper.Map<Customer>(item));
        }

        public void DeleteCustomer(int id)
        {
            if (_customerRepository.GetById(id) is null)
                throw new BusinessLogicException(Constants.CustomerNotFoundMessage);

            _customerRepository.Delete(id);
        }

        public IReadOnlyCollection<CustomerDto> GetAllCustomers()
        {
            return _mapper.Map<IEnumerable<CustomerDto>>(_customerRepository.GetAll())
                .ToList().AsReadOnly();
        }

        public CustomerDto GetCustomerById(int id)
        {
            var response = _customerRepository.GetById(id);

            if (response is null)
            {
                throw new BusinessLogicException(Constants.CustomerNotFoundMessage);
            }

            return _mapper.Map<CustomerDto>(response);
        }

        public void UpdateCustomer(CustomerDto item)
        {

            ValidateCustomer(item);

            _customerRepository.Update(_mapper.Map<Customer>(item));
        }

        public void ValidateCustomer(CustomerDto item)
        {
            if (string.IsNullOrEmpty(item.Gender))
                throw new ValidationException(Constants.EmptyCustomerTitleMessage);
            if (string.IsNullOrEmpty(item.IdentityUserID))
                throw new ValidationException(Constants.EmptyCustomerTitleMessage);
        }

        ///////////////////////////////
        ///
        public int CreateManager(ManagerDto item)
        {
            ValidateManager(item);

            return _managerRepository.Create(_mapper.Map<Manager>(item));
        }

        public void DeleteManager(int id)
        {
            if (_managerRepository.GetById(id) is null)
                throw new BusinessLogicException(Constants.ManagerNotFoundMessage);

            _managerRepository.Delete(id);
        }

        public IReadOnlyCollection<ManagerDto> GetAllManagers()
        {
            return _mapper.Map<IEnumerable<ManagerDto>>(_managerRepository.GetAll())
                .ToList().AsReadOnly();
        }

        public ManagerDto GetManagerById(int id)
        {
            var response = _managerRepository.GetById(id);

            if (response is null)
            {
                throw new BusinessLogicException(Constants.ManagerNotFoundMessage);
            }

            return _mapper.Map<ManagerDto>(response);
        }

        public void UpdateManager(ManagerDto item)
        {
            //ValidateSpecialization(item);
            ValidateManager(item);

            _managerRepository.Update(_mapper.Map<Manager>(item));
        }

        public void ValidateManager(ManagerDto item)
        {
            if (string.IsNullOrEmpty(item.Address))
                throw new ValidationException(Constants.EmptyManagerTitleMessage);
            if (item.DateOfBirth == default(DateTimeOffset))
                throw new ValidationException(Constants.EmptyManagerTitleMessage);
            if (string.IsNullOrEmpty(item.Address))
                throw new ValidationException(Constants.EmptyJobPositionTitleMessage);
            if (string.IsNullOrEmpty(item.IdentituUserID))
                throw new ValidationException(Constants.EmptyJobPositionTitleMessage);
        }

        ///////////////////////////////
        ///
        public int CreateOrder(OrderDto item)
        {
            ValidateOrder(item);

            return _orderRepository.Create(_mapper.Map<Order>(item));
        }

        public void DeleteOrder(int id)
        {
            if (_orderRepository.GetById(id) is null)
                throw new BusinessLogicException(Constants.OrderNotFoundMessage);

            _orderRepository.Delete(id);
        }

        public IReadOnlyCollection<OrderDto> GetAllOrders()
        {
            return _mapper.Map<IEnumerable<OrderDto>>(_orderRepository.GetAll())
                .ToList().AsReadOnly();
        }

        public OrderDto GetOrderById(int id)
        {
            var response = _orderRepository.GetById(id);

            if (response is null)
            {
                throw new BusinessLogicException(Constants.OrderNotFoundMessage);
            }

            return _mapper.Map<OrderDto>(response);
        }

        public void UpdateOrder(OrderDto item)
        {

            ValidateOrder(item);

            _orderRepository.Update(_mapper.Map<Order>(item));
        }

        public void ValidateOrder(OrderDto item)
        {
            if (string.IsNullOrEmpty(item.OrderStatus))
                throw new ValidationException(Constants.EmptyOrderTitleMessage);
            if (string.IsNullOrEmpty(item.Requirements))
                item.Requirements = "";
            if (string.IsNullOrEmpty(item.Title))
                throw new ValidationException(Constants.EmptyOrderTitleMessage);
        }
        public int CreateBrigade(BrigadeDto item)
        {
            ValidateBrigade(item);

            return _brigadeRepository.Create(_mapper.Map<Brigade>(item));
        }

        public void DeleteBrigade(int id)
        {
            if (_brigadeRepository.GetById(id) is null)
                throw new BusinessLogicException(Constants.BrigadeNotFoundMessage);

            _brigadeRepository.Delete(id);
        }

        public IReadOnlyCollection<BrigadeDto> GetAllBrigades()
        {
            return _mapper.Map<IEnumerable<BrigadeDto>>(_brigadeRepository.GetAll())
                .ToList().AsReadOnly();
        }


        public BrigadeDto GetBrigadeById(int id)
        {
            var response = _brigadeRepository.GetById(id);

            if (response is null)
            {
                throw new BusinessLogicException(Constants.BrigadeNotFoundMessage);
            }

            return _mapper.Map<BrigadeDto>(response);
        }

        public void UpdateBrigade(BrigadeDto item)
        {

            ValidateBrigade(item);

            _brigadeRepository.Update(_mapper.Map<Brigade>(item));
        }

        public void ValidateBrigade(BrigadeDto item)
        {
            if (string.IsNullOrEmpty(item.Title))
                throw new ValidationException(Constants.BrigadeEmptyTitleMessage);

        }
    }
}
