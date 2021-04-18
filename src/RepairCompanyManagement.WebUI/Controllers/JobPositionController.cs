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
    public class JobPositionController : Controller
    {
        private IBrigadeService _brigadeService { get; set; }

        private IMapper _mapper { get; set; }

        public JobPositionController(IBrigadeService brigadeService, IMapper mapper)
        {
            _brigadeService = brigadeService;
            _mapper = mapper;
        }

        [HttpGet]
        [ExceptionFilter()]
        public ActionResult Index()
        {
            var specs = _mapper.Map<IReadOnlyCollection<JobPositionViewModel>>(_brigadeService.GetAllJobPositions());
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
        public ActionResult Create(JobPositionViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                _brigadeService.CreateJobPosition(_mapper.Map<JobPositionDto>(model));

                return RedirectToAction("Index", "JobPositions", null);
            }

            return View(model);
        }

        [HttpGet]
        [ExceptionFilter("Index")]
        public ActionResult Update(int id)
        {
            var model = _mapper.Map<JobPositionViewModel>(_brigadeService.GetJobPositionById(id));
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(JobPositionViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                _brigadeService.UpdateJobPosition(_mapper.Map<JobPositionDto>(model));

                return RedirectToAction("Index", "JobPositions", null);
            }

            return View(model);
        }

        [HttpGet]
        [ExceptionFilter("Index")]
        public ActionResult Delete(int id)
        {
            var model = _mapper.Map<JobPositionViewModel>(_brigadeService.GetJobPositionById(id));

            return View(model);
        }

        [HttpGet]
        [ExceptionFilter("Index")]
        public ActionResult ConfirmDelete(int id)
        {
            var model = _mapper.Map<JobPositionViewModel>(_brigadeService.GetJobPositionById(id));
            _brigadeService.DeleteJobPosition(id);

            return RedirectToAction("Index", "JobPositions", null);
        }
    }
}