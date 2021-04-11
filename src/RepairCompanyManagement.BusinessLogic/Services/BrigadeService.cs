using AutoMapper;
using RepairCompanyManagement.BusinessLogic.Dtos;
using RepairCompanyManagement.BusinessLogic.Exceptions;
using RepairCompanyManagement.BusinessLogic.Interfaces;
using RepairCompanyManagement.DataAccess.Entities;
using RepairCompanyManagement.DataAccess.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace RepairCompanyManagement.BusinessLogic.Services
{
    public class BrigadeService : IBrigadeService
    {
        private readonly IRepository<Specialization> _specializationRepository;
        // private readonly IRepository<Brigade> _brigadeRepository;
        // private readonly IRepository<Employee> _employeeRepository;
        // private readonly IRepository<JobPosition> _jobPositionRepository;

        private readonly IMapper _mapper;

        public BrigadeService(IRepository<Specialization> specializationRepository, IMapper mapper)
        {
            _specializationRepository = specializationRepository;
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
        }
    }
}
