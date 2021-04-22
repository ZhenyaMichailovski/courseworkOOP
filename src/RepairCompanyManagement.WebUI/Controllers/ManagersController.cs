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
    public class ManagersController : Controller
    {
        private IBrigadeService _brigadeService { get; set; }

        private IMapper _mapper { get; set; }

        public ManagersController(IBrigadeService brigadeService, IMapper mapper)
        {
            _brigadeService = brigadeService;
            _mapper = mapper;
        }

        [HttpGet]
        [ExceptionFilter()]
        public ActionResult Index()
        {
            var specs = _mapper.Map<IReadOnlyCollection<ManagerViewModel>>(_brigadeService.GetAllManagers());
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
        public ActionResult Create(ManagerViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                _brigadeService.CreateManager(_mapper.Map<ManagerDto>(model));

                return RedirectToAction("Index", "Managers", null);
            }

            return View(model);
        }

        [HttpGet]
        [ExceptionFilter("Index")]
        public ActionResult Update(int id)
        {
            var model = _mapper.Map<ManagerViewModel>(_brigadeService.GetManagerById(id));
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ManagerViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                _brigadeService.UpdateManager(_mapper.Map<ManagerDto>(model));

                return RedirectToAction("Index", "Managers", null);
            }

            return View(model);
        }

        [HttpGet]
        [ExceptionFilter("Index")]
        public ActionResult Delete(int id)
        {
            var model = _mapper.Map<ManagerViewModel>(_brigadeService.GetManagerById(id));

            return View(model);
        }

        [HttpGet]
        [ExceptionFilter("Index")]
        public ActionResult ConfirmDelete(int id)
        {
            var model = _mapper.Map<ManagerViewModel>(_brigadeService.GetManagerById(id));
            _brigadeService.DeleteManager(id);

            return RedirectToAction("Index", "Managers", null);
        }
    }
}