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
    public class CustomersController : Controller
    {
        private IUserService _orderService { get; set; }

        private IMapper _mapper { get; set; }

        public CustomersController(IUserService brigadeService, IMapper mapper)
        {
            _orderService = brigadeService;
            _mapper = mapper;
        }

        [HttpGet]
        [ExceptionFilter()]
        public ActionResult Index()
        {
            var specs = _mapper.Map<IReadOnlyCollection<CustomerViewModel>>(_orderService.GetAllCustomers());
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
                _orderService.CreateCustomer(_mapper.Map<CustomerDto>(model));

                return RedirectToAction("Index", "Customers", null);
            }

            return View(model);
        }

        [HttpGet]
        [ExceptionFilter("Index")]
        public ActionResult Update(int id)
        {
            var model = _mapper.Map<CustomerViewModel>(_orderService.GetCustomerById(id));
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CustomerViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                _orderService.UpdateCustomer(_mapper.Map<CustomerDto>(model));

                return RedirectToAction("Index", "Customers", null);
            }

            return View(model);
        }

        [HttpGet]
        [ExceptionFilter("Index")]
        public ActionResult Delete(int id)
        {
            var model = _mapper.Map<CustomerViewModel>(_orderService.GetCustomerById(id));

            return View(model);
        }

        [HttpGet]
        [ExceptionFilter("Index")]
        public ActionResult ConfirmDelete(int id)
        {
            var model = _mapper.Map<CustomerViewModel>(_orderService.GetCustomerById(id));
            _orderService.DeleteCustomer(id);

            return RedirectToAction("Index", "Customers", null);
        }
    }
}