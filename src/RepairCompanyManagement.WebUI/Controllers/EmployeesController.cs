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
    public class EmployeesController : Controller
    {
        private IWorkerService _workerService { get; set; }

        private IMapper _mapper { get; set; }

        public EmployeesController(IWorkerService brigadeService, IMapper mapper)
        {
            _workerService = brigadeService;
            _mapper = mapper;
        }

        [HttpGet]
        [ExceptionFilter()]
        public ActionResult Index()
        {
            var specs = _mapper.Map<IReadOnlyCollection<EmployeeViewModel>>(_workerService.GetAllEmployees());
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
        public ActionResult Create(EmployeeViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                _workerService.CreateEmployee(_mapper.Map<EmployeeDto>(model));

                return RedirectToAction("Index", "Employees", null);
            }

            return View(model);
        }

        [HttpGet]
        [ExceptionFilter("Index")]
        public ActionResult Update(int id)
        {
            var model = _mapper.Map<EmployeeViewModel>(_workerService.GetEmployeeById(id));
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(EmployeeViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                _workerService.UpdateEmployee(_mapper.Map<EmployeeDto>(model));

                return RedirectToAction("Index", "Employees", null);
            }

            return View(model);
        }

        [HttpGet]
        [ExceptionFilter("Index")]
        public ActionResult Delete(int id)
        {
            var model = _mapper.Map<EmployeeViewModel>(_workerService.GetEmployeeById(id));

            return View(model);
        }

        [HttpGet]
        [ExceptionFilter("Index")]
        public ActionResult ConfirmDelete(int id)
        {
            var model = _mapper.Map<EmployeeViewModel>(_workerService.GetEmployeeById(id));
            _workerService.DeleteEmployee(id);

            return RedirectToAction("Index", "Employees", null);
        }
    }
}