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

        public IList<ReportYearDto> GetReportForMonth(int month)
        {
            var dateStart = new DateTimeOffset(DateTimeOffset.Now.Year, month, 1, 1, 0, 0, 0, DateTimeOffset.Now.Offset);
            var dateEnd = dateStart.AddMonths(1).AddDays(-1);

            var taskIds = _orderTaskRepository.GetAll()
                .Where(x => x.TaskCompletionDate >= dateStart && x.TaskCompletionDate < dateEnd)
                .Select(x => x.IdTask)
                .ToList();
            var brigadeTask = taskIds.Select(x => new { _taskRepository.GetById(x).IdBrigade });
            var brigade = _brigadeRepository.GetAll().ToList();
            return brigade.Select(x => new ReportYearDto { Brigade = x.Title, OrderAmount = brigadeTask.Count(y => y.IdBrigade == x.Id) }).ToList();
        }

        public IList<MonthDto> GetAllMonth()
        {
            var model = new List<MonthDto>();

            model.Add(new MonthDto { MonthId = 1, MonthName = "Январь" });
            model.Add(new MonthDto { MonthId = 2, MonthName = "Февраль" });
            model.Add(new MonthDto { MonthId = 3, MonthName = "Март" });
            model.Add(new MonthDto { MonthId = 4, MonthName = "Апрель" });
            model.Add(new MonthDto { MonthId = 5, MonthName = "Май" });
            model.Add(new MonthDto { MonthId = 6, MonthName = "Июнь" });
            model.Add(new MonthDto { MonthId = 7, MonthName = "Июль" });
            model.Add(new MonthDto { MonthId = 8, MonthName = "Август" });
            model.Add(new MonthDto { MonthId = 9, MonthName = "Сентябрь" });
            model.Add(new MonthDto { MonthId = 10, MonthName = "Октябрь" });
            model.Add(new MonthDto { MonthId = 11, MonthName = "Ноябрь" });
            model.Add(new MonthDto { MonthId = 12, MonthName = "Декабрь" });

            return model;
            
        }
    }
}
