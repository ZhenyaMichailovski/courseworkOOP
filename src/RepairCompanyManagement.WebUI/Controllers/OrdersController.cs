using AutoMapper;
using RepairCompanyManagement.BusinessLogic.Dtos;
using RepairCompanyManagement.BusinessLogic.Interfaces;
using RepairCompanyManagement.WebUI.Enums;
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
                { Id = x.Id, OrderStatus = (OrderStatus)x.OrderStatus, Requirements = x.Requirements, 
                    Title = x.Title, Price = _orderService.GetOrderPrice(x.Id) }).ToList();
            }
            else
            {
                model = orders.Select(x => new OrderViewModel
                {
                    Title = x.Title,
                    CustomerName = GetCustomerNameById(x.IdCustomers),
                    OrderStatus = (OrderStatus)x.OrderStatus,
                    Requirements = x.Requirements
                }).ToList();
            }                                                   
            return View(model.AsReadOnly());                    
        }                         
        [HttpGet]
        [Authorize]
        public ActionResult Info(int id)
        {

            var order = _orderService.GetOrderById(id);
            var userId = UserManager.FindByNameAsync(User.Identity.Name).Result.Id;
            var customer = _orderService.GetAllCustomers().FirstOrDefault(x => x.IdentityUserID == userId);

            // проверить менеджер или владелец заказа
            if (User.IsInRole(Identity.IdentityConstants.ManagerRole) || order.IdCustomers == customer.Id)
            {
                var model = new OrderInfoViewModel
                {
                    Id = order.Id,
                    OrderStatus = (OrderStatus)order.OrderStatus,
                    Requirements = order.Requirements,
                    Title = order.Title,
                    TotalPrice = _orderService.GetOrderPrice(order.Id),
                    Tasks = GetTasksByOrder(order.Id),
                };
                return View(model);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        private List<TaskViewModel> GetTasksByOrder(int id)
        {
            var taskDtos = _orderService.GetTasksByOrderId(id);
            return taskDtos.Select(x => new TaskViewModel { BrigadeName = _brigadeService.GetAllBrigades().FirstOrDefault(y => y.Id == x.IdBrigade).Title,
                                SpecializationName = _brigadeService.GetAllSpecializations().FirstOrDefault(y => y.Id == x.IdSpecialization).Name,
                                Title = x.Title, 
                                Description = x.Description,
                                Price = x.Price,
                                TaskCompletionDate = x.TaskCompletionDate,
                                Status = (TaskStatus)x.Status}).ToList();
        }
        private string GetCustomerNameById(int id)
        {
            var user = UserManager.FindByIdAsync(_orderService.GetCustomerById(id).IdentityUserID).Result;
            return user.FirstName + " " + user.Surname + " " + user.LastName;
        }
        [HttpGet]
        public ActionResult Create()
        {

            return View(new OrderViewModel());
        }

        [HttpPost]
        [ExceptionFilter("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                _orderService.CreateOrder(new OrderDto
                {
                    OrderStatus = (int)Enums.OrderStatus.NotConfirmed,
                    Id = model.Id,
                    IdCustomers = _orderService.GetAllCustomers().FirstOrDefault(x => x.IdentityUserID == UserManager.FindByNameAsync(User.Identity.Name).Result.Id).Id,
                    Requirements = model.Requirements,
                    Title = model.Title,
                });

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