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
    public class OrdersController : Controller
    {
        private IBrigadeService _brigadeService { get; set; }

        private IMapper _mapper { get; set; }

        public OrdersController(IBrigadeService brigadeService, IMapper mapper)
        {
            _brigadeService = brigadeService;
            _mapper = mapper;
        }

        [HttpGet]
        [ExceptionFilter()]
        public ActionResult Index()
        {
            var specs = _mapper.Map<IReadOnlyCollection<OrderViewModel>>(_brigadeService.GetAllOrders());
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
        public ActionResult Create(OrderViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                _brigadeService.CreateOrder(_mapper.Map<OrderDto>(model));

                return RedirectToAction("Index", "Orders", null);
            }

            return View(model);
        }

        [HttpGet]
        [ExceptionFilter("Index")]
        public ActionResult Update(int id)
        {
            var model = _mapper.Map<OrderViewModel>(_brigadeService.GetOrderById(id));
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(OrderViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                _brigadeService.UpdateOrder(_mapper.Map<OrderDto>(model));

                return RedirectToAction("Index", "Orders", null);
            }

            return View(model);
        }

        [HttpGet]
        [ExceptionFilter("Index")]
        public ActionResult Delete(int id)
        {
            var model = _mapper.Map<OrderViewModel>(_brigadeService.GetOrderById(id));

            return View(model);
        }

        [HttpGet]
        [ExceptionFilter("Index")]
        public ActionResult ConfirmDelete(int id)
        {
            var model = _mapper.Map<SpecializationViewModel>(_brigadeService.GetOrderById(id));
            _brigadeService.DeleteOrder(id);

            return RedirectToAction("Index", "Orders", null);
        }
    }
}