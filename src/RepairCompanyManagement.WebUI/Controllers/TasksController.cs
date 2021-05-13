using AutoMapper;
using RepairCompanyManagement.BusinessLogic.Dtos;
using RepairCompanyManagement.BusinessLogic.Interfaces;
using RepairCompanyManagement.WebUI.Filters;
using RepairCompanyManagement.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RepairCompanyManagement.WebUI.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private IOrderService _orderService { get; set; }
        private IBrigadeService _brigadeService { get; set; }
        private IMapper _mapper { get; set; }

        public TasksController(IOrderService orderService, IBrigadeService brigadeService, IMapper mapper)
        {
            _orderService = orderService;
            _brigadeService = brigadeService;
            _mapper = mapper;
        }

        [HttpGet]
        [ExceptionFilter()]
        
        public ActionResult Index()
        {
            var specItem = _brigadeService.GetAllSpecializations()
                                      .Select(x => new SpecializationItem
                                      { Name = x.Name, SpecializationId = x.Id }).ToList();
            var item = _orderService.GetAllTasks()
                .Select(x => new ManagerTaskViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    SpecializationId = x.IdSpecialization,
                    BrigadeId = x.IdBrigade,
                    Price = x.Price,
                    Description = x.Description,
                    Specialization = _orderService.GetSpecializationById(x.IdSpecialization).Name,
                    Brigade = _brigadeService.GetBrigadeById(x.IdBrigade).Title,
                    BrigadeItems = _brigadeService.GetAllBrigades().Where(y => y.IdSpecialization == x.IdSpecialization)
                                      .Select(y => new BrigadeItem
                                      { Id = y.Id, Name = y.Title }).ToList(),
                    SpecializationItems = specItem,
        }).ToList();
            return View(item);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var specializationItems = _brigadeService.GetAllSpecializations()
                                         .Select(x => new SpecializationItem
                                         { Name = x.Name, SpecializationId = x.Id }).ToList();
            var brigadeItem = _brigadeService.GetAllBrigades()
                                         .Select(x => new BrigadeItem
                                         {
                                             Name = x.Title, Id = x.Id,
                                         }).ToList();
            var model = new ManagerTaskViewModel()
            {
                SpecializationItems = specializationItems,
                Specialization = "",
                BrigadeItems = brigadeItem,
                Brigade = "",
            };
            return View(model);
        }

        [HttpPost]
        [ExceptionFilter("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ManagerTaskViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var item = new TaskDto
                {
                    Id = model.Id,
                    Description = model.Description,
                    IdBrigade = model.BrigadeId,
                    IdSpecialization = model.SpecializationId,
                    Price = model.Price,
                    Title = model.Title
                };
                _orderService.CreateTask(_mapper.Map<TaskDto>(item));

                return RedirectToAction("Index", "Tasks", null);
            }

            return View(model);
        }

        [HttpGet]
        [ExceptionFilter("Index")]
        public ActionResult Update(int id)
        {
            var specializationItems = _brigadeService.GetAllSpecializations()
                                         .Select(x => new SpecializationItem
                                         { Name = x.Name, SpecializationId = x.Id }).ToList();
            var brigadeItem = _brigadeService.GetAllBrigades()
                                         .Select(x => new BrigadeItem
                                         {
                                             Name = x.Title,
                                             Id = x.Id,
                                         }).ToList();
            var item = _orderService.GetTaskById(id);
            var task = new ManagerTaskViewModel
            {
                Id = item.Id,
                Title = item.Title,
                Price = item.Price,
                Description = item.Description,
                Specialization = _orderService.GetSpecializationById(item.IdSpecialization).Name,
                Brigade = _brigadeService.GetBrigadeById(item.IdBrigade).Title,
                BrigadeId = item.IdBrigade,
                SpecializationId = item.IdSpecialization,
                SpecializationItems = specializationItems,
                BrigadeItems = brigadeItem,
            };
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ManagerTaskViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var item = new TaskDto
                {
                    Id = model.Id,
                    Description = model.Description,
                    IdBrigade = model.BrigadeId,
                    IdSpecialization = model.SpecializationId,
                    Price = model.Price,
                    Title = model.Title
                };
                _orderService.UpdateTask(_mapper.Map<TaskDto>(item));

                return RedirectToAction("Index", "Tasks", null);
            }

            return View(model);
        }

        [HttpGet]
        [ExceptionFilter("Index")]
        public ActionResult Delete(int id)
        {
            var item = _orderService.GetTaskById(id);
            var task = new ManagerTaskViewModel
            {
                Id = item.Id,
                Title = item.Title,
                Price = item.Price,
                Description = item.Description,
                Specialization = _orderService.GetSpecializationById(item.IdSpecialization).Name,
                Brigade = _brigadeService.GetBrigadeById(item.IdBrigade).Title,
            };
            var model = _mapper.Map<ManagerTaskViewModel>(task);

            return View(model);
        }

        [HttpGet]
        [ExceptionFilter("Index")]
        public ActionResult ConfirmDelete(int id)
        {
            
           // var model = _mapper.Map<ManagerTaskViewModel>(_orderService.GetTaskById(id));
            _orderService.DeleteTask(id);

            return RedirectToAction("Index", "Tasks", null);
        }
    }
}