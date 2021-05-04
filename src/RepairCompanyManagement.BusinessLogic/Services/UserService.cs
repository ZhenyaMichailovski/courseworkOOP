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
    public class UserService : IUserService
    {
        private readonly IRepository<Manager> _managerRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Brigade> _brigadeRepository;
        private readonly IRepository<JobPosition> _jobPositionRepository;
        private readonly IMapper _mapper;
        public UserService(IRepository<Manager> managerRepository, IRepository<Employee> employeeRepository, IRepository<JobPosition> jobPositionRepositiory,
           IRepository<Customer> customerRepository, IMapper mapper, IRepository<Brigade> brigadeRepository)

        {
            _customerRepository = customerRepository;
            _managerRepository = managerRepository;
            _employeeRepository = employeeRepository;
            _brigadeRepository = brigadeRepository;
            _jobPositionRepository = jobPositionRepositiory;
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
                throw new ValidationException();
        }

        ///////////////////////////////
        //
        public (string, string) GetEmployeeFullInfo(string id)
        {
            var employee = _employeeRepository.GetAll().FirstOrDefault(x => x.IdentityUserID == id);
            var brigade = _brigadeRepository.GetById(employee.IdBrigade);
            var jobPosition = _jobPositionRepository.GetById(employee.IdJobPosition);
            return (brigade.Title, jobPosition.Title);
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


        public void RemoveFromRoles(string id)
        {
            var manager = _managerRepository.GetAll().FirstOrDefault(x => x.IdentityUserID == id);
            var customers = _customerRepository.GetAll().FirstOrDefault(x => x.IdentityUserID == id);
            var employee = _employeeRepository.GetAll().FirstOrDefault(x => x.IdentityUserID == id);
            if (manager != null)
                _managerRepository.Delete(manager.Id);
            else if (customers != null)
                _customerRepository.Delete(customers.Id);
            else if (employee != null)
                _employeeRepository.Delete(employee.Id);
        }
    }
}
