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
        private readonly IRepository<OrderTask> _orderTaskRepository;
        private readonly IRepository<Specialization> _specializationRepository;
        private readonly IRepository<Task> _taskRepository;
        private readonly IRepository<Feedback> _feedbackRepository;

        private readonly IMapper _mapper;
        public OrderService(IRepository<Order> orderRepository, IRepository<Task> taskRepository, IRepository<Manager> managerRepository, IRepository<Specialization> specializationRepository,
        IRepository<Customer> customerRepository, IRepository<Brigade> brigadeRepository, IRepository<OrderTask> orderTaskRepository, IRepository<Feedback> feedbackRepository, IMapper mapper)

        {
            _feedbackRepository = feedbackRepository;
            _customerRepository = customerRepository;
            _managerRepository = managerRepository;
            _orderRepository = orderRepository;
            _orderTaskRepository = orderTaskRepository;
            _brigadeRepository = brigadeRepository;
            _specializationRepository = specializationRepository;
            _taskRepository = taskRepository;
            _mapper = mapper;
        }
        public int CreateSpecialization(SpecializationDto item)
        {
            ValidateSpecialization(item);

            return _specializationRepository.Create(_mapper.Map<Specialization>(item));
        }

        public void DeleteSpecialization(int id)
        {
            if (_specializationRepository.GetById(id) is null)
                throw new BusinessLogicException(Constants.SpecializationNotFoundMessage);

            _specializationRepository.Delete(id);
        }

        public IReadOnlyCollection<SpecializationDto> GetAllSpecializations()
        {
            return _mapper.Map<IEnumerable<SpecializationDto>>(_specializationRepository.GetAll())
                .ToList().AsReadOnly();
        }

        public SpecializationDto GetSpecializationById(int id)
        {
            var response = _specializationRepository.GetById(id);

            if (response is null)
            {
                throw new BusinessLogicException(Constants.SpecializationNotFoundMessage);
            }

            return _mapper.Map<SpecializationDto>(response);
        }

        public void UpdateSpecialization(SpecializationDto item)
        {
            ValidateSpecialization(item);

            _specializationRepository.Update(_mapper.Map<Specialization>(item));
        }

        public void ValidateSpecialization(SpecializationDto item)
        {
            if (string.IsNullOrEmpty(item.Name))
                throw new ValidationException(Constants.EmptySpecializationNameMessage);
            if (item.Description is null)
                item.Description = "";
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
            if (string.IsNullOrEmpty(item.IdentityUserID))
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
        public int CreateOrderTask(OrderTaskDto item)
        {
            ValidateOrderTask(item);

            return _orderTaskRepository.Create(_mapper.Map<OrderTask>(item));
        }

        public void DeleteOrderTask(int id)
        {
            if (_orderTaskRepository.GetById(id) is null)
                throw new BusinessLogicException(Constants.OrderTaskNotFoundMessage);

            _orderTaskRepository.Delete(id);
        }

        public IReadOnlyCollection<OrderTaskDto> GetAllOrderTasks()
        {
            return _mapper.Map<IEnumerable<OrderTaskDto>>(_orderTaskRepository.GetAll())
                .ToList().AsReadOnly();
        }

        public OrderTaskDto GetOrderTaskById(int id)
        {
            var response = _orderTaskRepository.GetById(id);

            if (response is null)
            {
                throw new BusinessLogicException(Constants.OrderTaskNotFoundMessage);
            }

            return _mapper.Map<OrderTaskDto>(response);
        }

        public void UpdateOrderTask(OrderTaskDto item)
        {

            ValidateOrderTask(item);

            _orderTaskRepository.Update(_mapper.Map<OrderTask>(item));
        }

        public void ValidateOrderTask(OrderTaskDto item)
        {

        }
        ///////////////////////
        public int CreateTask(TaskDto item)
        {
            ValidateTask(item);

            return _taskRepository.Create(_mapper.Map<Task>(item));
        }

        public void DeleteTask(int id)
        {
            if (_taskRepository.GetById(id) is null)
                throw new BusinessLogicException(Constants.TaskNotFoundMessage);

            _taskRepository.Delete(id);
        }

        public IReadOnlyCollection<TaskDto> GetAllTasks()
        {
            return _mapper.Map<IEnumerable<TaskDto>>(_taskRepository.GetAll())
                .ToList().AsReadOnly();
        }

        public TaskDto GetTaskById(int id)
        {
            var response = _taskRepository.GetById(id);

            if (response is null)
            {
                throw new BusinessLogicException(Constants.TaskNotFoundMessage);
            }

            return _mapper.Map<TaskDto>(response);
        }

        public void UpdateTask(TaskDto item)
        {

            ValidateTask(item);

            _taskRepository.Update(_mapper.Map<Task>(item));
        }

        public void ValidateTask(TaskDto item)
        {

        }
        public decimal GetOrderPrice(int id)
        {
            var orderTasks = _orderTaskRepository.GetAll().Where(x => x.IdOrder == id);
            var tasks = _taskRepository.GetAll();


            var result = from orderTask in orderTasks
                         join t in tasks on orderTask.IdTask equals t.Id
                         select new { value = t.Price };

            return result.Sum(x => x.value);
        }
        public IReadOnlyCollection<TaskDto> GetTasksByOrderId(int id)
        {
            var taskOrders = _orderTaskRepository.GetAll().Where(x => x.IdOrder == id).ToList();
            return _mapper.Map<IReadOnlyCollection<TaskDto>>(taskOrders.Select(x => _taskRepository.GetById(x.IdTask)).ToList().AsReadOnly());
        }
        public BrigadeDto FindFreeBrigadeForDate(DateTimeOffset date, int idSpecialization)
        {
            var brigade = _brigadeRepository.GetAll().Where(x => x.IdSpecialization == idSpecialization).ToList();
            var taskIds = _orderTaskRepository.GetAll().Where(x => x.TaskCompletionDate.Day == date.Day && x.TaskCompletionDate.Month == date.Month && x.TaskCompletionDate.Year == date.Year)
                .Select(x => x.IdTask).ToList();

            var tasks = _taskRepository.GetAll().Where(x => taskIds.Any(y => y == x.Id)).ToList();
            brigade.RemoveAll(x => tasks.Any(y => y.IdBrigade == x.Id));

            return brigade.Any() ? _mapper.Map<BrigadeDto>(brigade.First()) : null;
        }

        public int FindOrderTaskByOrderAndTaskIds(int orderId, int taskId)
        {
            return _orderTaskRepository.GetAll().FirstOrDefault(x => x.IdOrder == orderId && x.IdTask == taskId).Id;
        }
        public void ChangeOrderTaskStatus(int idOrderTask)
        {
            var orderTask = _orderTaskRepository.GetById(idOrderTask);
            var order = _orderRepository.GetAll().FirstOrDefault(x => x.Id == orderTask.IdOrder);

            if ((int)orderTask.Status == (int)DataAccess.Enums.OrderTaskStatus.NotCompleted)
            {
                orderTask.Status = DataAccess.Enums.OrderTaskStatus.Completed;
                _orderTaskRepository.Update(orderTask);
            }
            else
            {
                throw new BusinessLogic.Exceptions.BusinessLogicException("The task has already been completed");
            }
        }
        public void ChangeOrderStatus(int idOrderTask)
        {
            var orderTask = _orderTaskRepository.GetById(idOrderTask);
            var order = _orderRepository.GetAll().FirstOrDefault(x => x.Id == orderTask.IdOrder);
            var allOrderTaskByOrderId = _orderTaskRepository.GetAll().Where(x => x.IdOrder == order.Id).ToList();

            if (allOrderTaskByOrderId.All(x => x.Status == DataAccess.Enums.OrderTaskStatus.Completed))
            {
                order.OrderStatus = DataAccess.Enums.OrderStatus.Complited;
                _orderRepository.Update(order);
            }
        }

        public int CreateFeedback(FeedbackDto item)
        {

            return _feedbackRepository.Create(_mapper.Map<Feedback>(item));
        }

        public FeedbackDto GetFeedbackById(int id)
        {
            var response = _feedbackRepository.GetById(id);

            if (response is null)
            {
                throw new BusinessLogicException("Feedback not found");
            }

            return _mapper.Map<FeedbackDto>(response);
        }
        public IReadOnlyCollection<FeedbackDto> GetAllFeedbacks()
        {
            return _mapper.Map<IEnumerable<FeedbackDto>>(_feedbackRepository.GetAll())
                .ToList().AsReadOnly();
        }
        public void UpdateFeedback(FeedbackDto item)
        {

            _feedbackRepository.Update(_mapper.Map<Feedback>(item));
        }
        public void DeleteFeedback(int id)
        {
            if (_feedbackRepository.GetById(id) is null)
                throw new BusinessLogicException("Feedback not found");

            _feedbackRepository.Delete(id);
        }
    }
}
