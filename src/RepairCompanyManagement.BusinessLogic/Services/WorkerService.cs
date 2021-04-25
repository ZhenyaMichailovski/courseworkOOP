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
    public class WorkerService : IWorkerService
    {
        

        private readonly IRepository<JobPosition> _jobPositionRepository;
        private readonly IRepository<Brigade> _brigadeRepository;
        private readonly IRepository<Employee> _employeeRepository;

        private readonly IMapper _mapper;

        public WorkerService(IRepository<Brigade> brigadeRepository, IRepository<Employee> employeeRepository, IRepository<JobPosition> jobPositionRepository, IMapper mapper)
        {
            _brigadeRepository = brigadeRepository;
            _employeeRepository = employeeRepository;
            _jobPositionRepository = jobPositionRepository;
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
        ///////////////////////////////
        ///
    }
}
