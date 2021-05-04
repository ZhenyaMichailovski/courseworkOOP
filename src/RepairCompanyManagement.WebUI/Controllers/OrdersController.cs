using AutoMapper;
using RepairCompanyManagement.BusinessLogic.Dtos;
using RepairCompanyManagement.BusinessLogic.Interfaces;
using RepairCompanyManagement.WebUI.Filters;
using RepairCompanyManagement.WebUI.Identity;
using RepairCompanyManagement.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RepairCompanyManagement.WebUI.Controllers
{
    public class OrdersController : IdentityBaseController
    {
        private IOrderService _orderService { get; set; }

        private IMapper _mapper { get; set; }


        public OrdersController(IOrderService orderService, IMapper mapper, ApplicationUserManager userManager, ApplicationSignInManager signInManager)
            : base(userManager, signInManager)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpGet]
        [ExceptionFilter()]
        [Authorize(Roles = Identity.IdentityConstants.ManagerRole)]
        public ActionResult Index()
        {
            var specs = _mapper.Map<IReadOnlyCollection<OrderViewModel>>(_orderService.GetAllOrders());
            return View(specs);
        }

        [HttpGet]
        [Authorize]
        public ActionResult My()
        {
            var userId = UserManager.FindByNameAsync(User.Identity.Name).Result.Id;
            var customer = _orderService.GetAllCustomers().FirstOrDefault(x => x.IdentityUserID == userId);
            var model = _orderService.GetAllOrders().Where(x => x.IdCustomers == customer.Id).Select(x => new MyOrdersViewModel 
            { Id = x.Id , OrderStatus = x.OrderStatus, Requirements = x.Requirements, Title = x.Title, Price = _orderService.GetOrderPrice(x.Id) }).ToList();
            return View(model);
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
                _orderService.CreateOrder(_mapper.Map<OrderDto>(model));

                return RedirectToAction("Index", "Orders", null);
            }

            return View(model);
        }

        [HttpGet]
        [ExceptionFilter("Index")]
        public ActionResult Update(int id)
        {
            var model = _mapper.Map<OrderViewModel>(_orderService.GetOrderById(id));
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(OrderViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                _orderService.UpdateOrder(_mapper.Map<OrderDto>(model));

                return RedirectToAction("Index", "Orders", null);
            }

            return View(model);
        }

        [HttpGet]
        [ExceptionFilter("Index")]
        public ActionResult Delete(int id)
        {
            var model = _mapper.Map<OrderViewModel>(_orderService.GetOrderById(id));

            return View(model);
        }

        [HttpGet]
        [ExceptionFilter("Index")]
        public ActionResult ConfirmDelete(int id)
        {
            var model = _mapper.Map<SpecializationViewModel>(_orderService.GetOrderById(id));
            _orderService.DeleteOrder(id);

            return RedirectToAction("Index", "Orders", null);
        }
    }
}