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
    public class ReportService : IReportService
    {
        private readonly IRepository<Manager> _managerRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Brigade> _brigadeRepository;
        private readonly IRepository<OrderTask> _orderTaskRepository;
        private readonly IRepository<Specialization> _specializationRepository;
        private readonly IRepository<Task> _taskRepository;

        private readonly IMapper _mapper;
        public ReportService(IRepository<Order> orderRepository, IRepository<Task> taskRepository, IRepository<Manager> managerRepository, IRepository<Specialization> specializationRepository,
        IRepository<Customer> customerRepository, IRepository<Brigade> brigadeRepository, IRepository<OrderTask> orderTaskRepository, IMapper mapper)

        {
            _customerRepository = customerRepository;
            _managerRepository = managerRepository;
            _orderRepository = orderRepository;
            _orderTaskRepository = orderTaskRepository;
            _brigadeRepository = brigadeRepository;
            _specializationRepository = specializationRepository;
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public IList<ReportYearDto> GetReportForYear()
        {
            var dateStart = new DateTimeOffset(DateTimeOffset.Now.Year, 1, 1, 0, 0, 0, DateTimeOffset.Now.Offset);
            var dateEnd = dateStart.AddYears(1).AddDays(-1);

            var taskIds = _orderTaskRepository.GetAll()
                .Where(x => x.TaskCompletionDate >= dateStart && x.TaskCompletionDate < dateEnd)
                .Select(x => x.IdTask)
                .ToList();
            var brigadeTask = taskIds.Select(x => new { _taskRepository.GetById(x).IdBrigade });
            var brigade = _brigadeRepository.GetAll().ToList();
            return brigade.Select(x => new ReportYearDto { Brigade = x.Title, OrderAmount = brigadeTask.Count(y => y.IdBrigade == x.Id) }).ToList();
        }
    }
}
