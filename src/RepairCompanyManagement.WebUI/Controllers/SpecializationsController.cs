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
    public class SpecializationsController : Controller
    {
        private IBrigadeService _brigadeService { get; set; }

        private IMapper _mapper { get; set; }

        public SpecializationsController(IBrigadeService brigadeService, IMapper mapper)
        {
            _brigadeService = brigadeService;
            _mapper = mapper;
        }

        [HttpGet]
        [ExceptionFilter()]
        public ActionResult Index()
        {
            var specs = _mapper.Map<IReadOnlyCollection<SpecializationViewModel>>(_brigadeService.GetAllSpecializations());
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
        public ActionResult Create(SpecializationViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                _brigadeService.CreateSpecialization(_mapper.Map<SpecializationDto>(model));

                return RedirectToAction("Index", "Specializations", null);
            }

            return View(model);
        }

        [HttpGet]
        [ExceptionFilter("Index")]
        public ActionResult Update(int id)
        {
            var model = _mapper.Map<SpecializationViewModel>(_brigadeService.GetSpecializationById(id));
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(SpecializationViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                _brigadeService.UpdateSpecialization(_mapper.Map<SpecializationDto>(model));

                return RedirectToAction("Index", "Specializations", null);
            }

            return View(model);
        }

        [HttpGet]
        [ExceptionFilter("Index")]
        public ActionResult Delete(int id)
        {
            var model = _mapper.Map<SpecializationViewModel>(_brigadeService.GetSpecializationById(id));

            return View(model);
        }

        [HttpGet]
        [ExceptionFilter("Index")]
        public ActionResult ConfirmDelete(int id)
        {
            var model = _mapper.Map<SpecializationViewModel>(_brigadeService.GetSpecializationById(id));
            _brigadeService.DeleteSpecialization(id);
            
            return RedirectToAction("Index", "Specializations", null);
        }
    }
}