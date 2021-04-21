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
    public class BrigadesController : Controller
    {
        private IBrigadeService _brigadeService { get; set; }

        private IMapper _mapper { get; set; }

        public BrigadesController(IBrigadeService brigadeService, IMapper mapper)
        {
            _brigadeService = brigadeService;
            _mapper = mapper;
        }

        [HttpGet]
        [ExceptionFilter()]
        public ActionResult Index()
        {
            var specs = _mapper.Map<IReadOnlyCollection<BrigadeViewModel>>(_brigadeService.GetAllSpecializations());
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
        public ActionResult Create(BrigadeViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                _brigadeService.CreateBrigade(_mapper.Map<BrigadeDto>(model));

                return RedirectToAction("Index", "Brigades", null);
            }

            return View(model);
        }

        [HttpGet]
        [ExceptionFilter("Index")]
        public ActionResult Update(int id)
        {
            var model = _mapper.Map<BrigadeViewModel>(_brigadeService.GetBrigadeById(id));
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(BrigadeViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                _brigadeService.UpdateBrigade(_mapper.Map<BrigadeDto>(model));

                return RedirectToAction("Index", "Brigades", null);
            }

            return View(model);
        }

        [HttpGet]
        [ExceptionFilter("Index")]
        public ActionResult Delete(int id)
        {
            var model = _mapper.Map<BrigadeViewModel>(_brigadeService.GetBrigadeById(id));

            return View(model);
        }

        [HttpGet]
        [ExceptionFilter("Index")]
        public ActionResult ConfirmDelete(int id)
        {
            var model = _mapper.Map<BrigadeViewModel>(_brigadeService.GetBrigadeById(id));
            _brigadeService.DeleteBrigade(id);

            return RedirectToAction("Index", "Brigades", null);
        }
    }
}