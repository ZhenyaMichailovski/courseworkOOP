using AutoMapper;
using RepairCompanyManagement.BusinessLogic.Dtos;
using RepairCompanyManagement.BusinessLogic.Interfaces;
using RepairCompanyManagement.WebUI.Filters;
using RepairCompanyManagement.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace RepairCompanyManagement.WebUI.Controllers
{
    public class TasksController : Controller
    {
        private IOrderService _brigadeService { get; set; }

        private IMapper _mapper { get; set; }

        public TasksController(IOrderService brigadeService, IMapper mapper)
        {
            _brigadeService = brigadeService;
            _mapper = mapper;
        }

        [HttpGet]
        [ExceptionFilter()]
        public ActionResult Index()
        {
            var specs = _mapper.Map<IReadOnlyCollection<TaskViewModel>>(_brigadeService.GetAllTasks());
            return View(specs);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ExceptionFilter("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaskViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                _brigadeService.CreateTask(_mapper.Map<TaskDto>(model));

                return RedirectToAction("Index", "Tasks", null);
            }

            return View(model);
        }

        [HttpGet]
        [ExceptionFilter("Index")]
        public ActionResult Update(int id)
        {
            var model = _mapper.Map<TaskViewModel>(_brigadeService.GetTaskById(id));
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(TaskViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                _brigadeService.UpdateTask(_mapper.Map<TaskDto>(model));

                return RedirectToAction("Index", "Tasks", null);
            }

            return View(model);
        }

        [HttpGet]
        [ExceptionFilter("Index")]
        public ActionResult Delete(int id)
        {
            var model = _mapper.Map<TaskViewModel>(_brigadeService.GetTaskById(id));

            return View(model);
        }

        [HttpGet]
        [ExceptionFilter("Index")]
        public ActionResult ConfirmDelete(int id)
        {
            var model = _mapper.Map<TaskViewModel>(_brigadeService.GetTaskById(id));
            _brigadeService.DeleteTask(id);

            return RedirectToAction("Index", "Tasks", null);
        }
    }
}