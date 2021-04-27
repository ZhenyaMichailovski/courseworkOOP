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
    public class OrderTasksController : Controller
    {
        private IOrderService _brigadeService { get; set; }

        private IMapper _mapper { get; set; }

        public OrderTasksController(IOrderService brigadeService, IMapper mapper)
        {
            _brigadeService = brigadeService;
            _mapper = mapper;
        }

        [HttpGet]
        [ExceptionFilter()]
        public ActionResult Index()
        {
            var specs = _mapper.Map<IReadOnlyCollection<OrderTaskViewModel>>(_brigadeService.GetAllOrderTasks());
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
        public ActionResult Create(OrderTaskViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                _brigadeService.CreateOrderTask(_mapper.Map<OrderTaskDto>(model));

                return RedirectToAction("Index", "OrderTasks", null);
            }

            return View(model);
        }

        [HttpGet]
        [ExceptionFilter("Index")]
        public ActionResult Update(int id)
        {
            var model = _mapper.Map<OrderTaskViewModel>(_brigadeService.GetOrderTaskById(id));
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(OrderTaskViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                _brigadeService.UpdateOrderTask(_mapper.Map<OrderTaskDto>(model));

                return RedirectToAction("Index", "OrderTasks", null);
            }

            return View(model);
        }

        [HttpGet]
        [ExceptionFilter("Index")]
        public ActionResult Delete(int id)
        {
            var model = _mapper.Map<OrderTaskViewModel>(_brigadeService.GetOrderTaskById(id));

            return View(model);
        }

        [HttpGet]
        [ExceptionFilter("Index")]
        public ActionResult ConfirmDelete(int id)
        {
            var model = _mapper.Map<OrderTaskViewModel>(_brigadeService.GetOrderTaskById(id));
            _brigadeService.DeleteOrderTask(id);

            return RedirectToAction("Index", "OrderTasks", null);
        }
    }
}