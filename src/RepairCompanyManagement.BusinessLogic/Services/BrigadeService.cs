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
    public class BrigadeService : IBrigadeService
    {
        private readonly IRepository<Specialization> _specializationRepository;
        private readonly IRepository<Brigade> _brigadeRepository;
        private readonly IRepository<Task> _taskRepository;
        private readonly IRepository<OrderTask> _orderTaskRepository;
        private readonly IRepository<JobPosition> _jobPositionRepository;
        private readonly IRepository<Employee> _employeeRepository;

        private readonly IMapper _mapper;

        public BrigadeService(IRepository<Specialization> specializationRepository, IRepository<Brigade> brigadeRepository, IRepository<Task> taskRepository,
            IRepository<Employee> employeeRepository, IRepository<JobPosition> jobPositionRepository, IRepository<OrderTask> orderTaskRepository, IMapper mapper)

        {
            _specializationRepository = specializationRepository;
            _brigadeRepository = brigadeRepository;
            _jobPositionRepository = jobPositionRepository;
            _employeeRepository = employeeRepository;
            _orderTaskRepository = orderTaskRepository;
            _taskRepository = taskRepository;
            _mapper = mapper;
        }
        public int CreateEmployee(EmployeeDto item)
        {
            ValidateEmployee(item);

            return _employeeRepository.Create(_mapper.Map<Employee>(item));
        }

        public void DeleteEmployee(int id)
        {
            if (_employeeRepository.GetById(id) is null)
                throw new BusinessLogicException(Constants.EmployeeNotFoundMessage);

            _employeeRepository.Delete(id);
        }

        public IReadOnlyCollection<EmployeeDto> GetAllEmployees()
        {
            return _mapper.Map<IEnumerable<EmployeeDto>>(_employeeRepository.GetAll())
                .ToList().AsReadOnly();
        }

        public EmployeeDto GetEmployeeById(int id)
        {
            var response = _employeeRepository.GetById(id);

            if (response is null)
            {
                throw new BusinessLogicException(Constants.EmployeeNotFoundMessage);
            }

            return _mapper.Map<EmployeeDto>(response);
        }

        public void UpdateEmployee(EmployeeDto item)
        {

            ValidateEmployee(item);

            _employeeRepository.Update(_mapper.Map<Employee>(item));
        }

        public void ValidateEmployee(EmployeeDto item)
        {
            if (string.IsNullOrEmpty(item.IdentityUserID))
                throw new ValidationException(Constants.EmptyEmployeeTitleMessage);
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

        ///////////////////////////////

        public int CreateJobPosition(JobPositionDto item)
        {
            ValidateJobPosition(item);

            return _jobPositionRepository.Create(_mapper.Map<JobPosition>(item));
        }

        public void DeleteJobPosition(int id)
        {
            if (_jobPositionRepository.GetById(id) is null)
                throw new BusinessLogicException(Constants.JobPositionNotFoundMessage);

            _jobPositionRepository.Delete(id);
        }

        public IReadOnlyCollection<JobPositionDto> GetAllJobPositions()
        {
            return _mapper.Map<IEnumerable<JobPositionDto>>(_jobPositionRepository.GetAll())
                .ToList().AsReadOnly();
        }

        public JobPositionDto GetJobPositionById(int id)
        {
            var response = _jobPositionRepository.GetById(id);

            if (response is null)
            {
                throw new BusinessLogicException(Constants.JobPositionNotFoundMessage);
            }

            return _mapper.Map<JobPositionDto>(response);
        }

        public void UpdateJobPosition(JobPositionDto item)
        {
            //ValidateSpecialization(item);
            ValidateJobPosition(item);

            _jobPositionRepository.Update(_mapper.Map<JobPosition>(item));
        }

        public void ValidateJobPosition(JobPositionDto item)
        {
            if (string.IsNullOrEmpty(item.Title))
                throw new ValidationException(Constants.EmptyJobPositionTitleMessage);
            if (item.Purpose is null)
                item.Purpose = "";
        }



        ///////////////////////////////
        ///

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
        public IReadOnlyCollection<BrigadeDto> FindBrigadeBySpecialization(int specializationId)
        {
            return _mapper.Map<IEnumerable<BrigadeDto>>(_brigadeRepository.GetAll().Where(x => x.IdSpecialization == specializationId))
                .ToList().AsReadOnly();
        }
        public void ValidateBrigade(BrigadeDto item)
        {
            if (string.IsNullOrEmpty(item.Title))
                throw new ValidationException(Constants.BrigadeEmptyTitleMessage);

        }
        ///////////////////////////////
        ///
        
       
        ///////////////////////////////
        ///
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

        public List<TaskDto> FindTasksBySpecialization(int specializationId)
        {
            var tasks = _taskRepository.GetAll().Where(x => x.IdSpecialization == specializationId).ToList();
            return _mapper.Map<List<TaskDto>>(tasks);
        }

    }
}
