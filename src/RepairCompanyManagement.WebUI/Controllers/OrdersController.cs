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
        private IBrigadeService _brigadeService { get; set; }
        private IMapper _mapper { get; set; }


        public OrdersController(IOrderService orderService, IBrigadeService brigadeService, IMapper mapper, ApplicationUserManager userManager, ApplicationSignInManager signInManager)
            : base(userManager, signInManager)
        {
            _orderService = orderService;
            _brigadeService = brigadeService;
            _mapper = mapper;
        }


        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            var orders = _orderService.GetAllOrders().ToList();
            var model = new List<OrderViewModel>();
            if (User.IsInRole(Identity.IdentityConstants.CustomerRole))
            {
                var userId = UserManager.FindByNameAsync(User.Identity.Name).Result.Id;
                var customer = _orderService.GetAllCustomers().FirstOrDefault(x => x.IdentityUserID == userId);
                model = orders.Where(x => x.IdCustomers == customer.Id).Select(x => new OrderViewModel
                { Id = x.Id, OrderStatus = x.OrderStatus, Requirements = x.Requirements, 
                    Title = x.Title, Price = _orderService.GetOrderPrice(x.Id) }).ToList();
            }
            else
            {
                model = orders.Select(x => new OrderViewModel
                {
                    Title = x.Title,
                    CustomerName = GetCustomerNameById(x.IdCustomers),
                    OrderStatus = x.OrderStatus,
                    Requirements = x.Requirements
                }).ToList();
            }                                                   
            return View(model.AsReadOnly());                    
        }                         
        [HttpGet]
        [Authorize]
        public ActionResult Info()
        {
            var orders = _orderService.GetAllOrders().ToList();
            var model = new List<OrderInfoViewModel>();
            if (User.IsInRole(Identity.IdentityConstants.CustomerRole))
            {
                var userId = UserManager.FindByNameAsync(User.Identity.Name).Result.Id;
                var customer = _orderService.GetAllCustomers().FirstOrDefault(x => x.IdentityUserID == userId);
                model = orders.Where(x => x.IdCustomers == customer.Id).Select(x => new OrderInfoViewModel
                {
                    Id = x.Id,
                    OrderStatus = x.OrderStatus,
                    Requirements = x.Requirements,
                    Title = x.Title,
                    TotalPrice = _orderService.GetOrderPrice(x.Id),
                    
                }).ToList();
            }
            else
            {
                
                model = orders.Select(x => new OrderInfoViewModel
                {
                    Title = x.Title,
                    CustomerName = GetCustomerNameById(x.IdCustomers),
                    OrderStatus = x.OrderStatus,
                    Requirements = x.Requirements,
                    TotalPrice = _orderService.GetOrderPrice(x.Id),
                    Tasks = SetTasksModel(x.Id),
                }).ToList();
            }
            return View(model.AsReadOnly());
        }
        private List<TaskViewModel> SetTasksModel(int id)
        {
            var orderTasks = _orderService.GetAllOrderTasks().FirstOrDefault(y => y.IdOrder == id);
            var tasks = _orderService.GetAllTasks().Select(x => x.Id == orderTasks.IdTask).ToList();
            var modelTasks = tasks.Select(x => new TaskViewModel { BrigadeName = x. }).ToList();

        }
        private string GetCustomerNameById(int id)
        {
            var user = UserManager.FindByIdAsync(_orderService.GetCustomerById(id).IdentityUserID).Result;
            return user.FirstName + " " + user.Surname + " " + user.LastName;
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