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
        private IBrigadeService _brigadeService { get; set; }

        private IMapper _mapper { get; set; }

        public EmployeesController(IBrigadeService brigadeService, IMapper mapper)
        {
            _brigadeService = brigadeService;
            _mapper = mapper;
        }

        [HttpGet]
        [ExceptionFilter()]
        public ActionResult Index()
        {
            var specs = _mapper.Map<IReadOnlyCollection<EmployeeViewModel>>(_brigadeService.GetAllEmployees());
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
                _brigadeService.CreateEmployee(_mapper.Map<EmployeeDto>(model));

                return RedirectToAction("Index", "Employees", null);
            }

            return View(model);
        }

        [HttpGet]
        [ExceptionFilter("Index")]
        public ActionResult Update(int id)
        {
            var model = _mapper.Map<EmployeeViewModel>(_brigadeService.GetEmployeeById(id));
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(EmployeeViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                _brigadeService.UpdateEmployee(_mapper.Map<EmployeeDto>(model));

                return RedirectToAction("Index", "Employees", null);
            }

            return View(model);
        }

        [HttpGet]
        [ExceptionFilter("Index")]
        public ActionResult Delete(int id)
        {
            var model = _mapper.Map<EmployeeViewModel>(_brigadeService.GetEmployeeById(id));

            return View(model);
        }

        [HttpGet]
        [ExceptionFilter("Index")]
        public ActionResult ConfirmDelete(int id)
        {
            var model = _mapper.Map<EmployeeViewModel>(_brigadeService.GetEmployeeById(id));
            _brigadeService.DeleteEmployee(id);

            return RedirectToAction("Index", "Employees", null);
        }
    }
}