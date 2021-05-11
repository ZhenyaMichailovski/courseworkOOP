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
            decimal Money = UserManager.FindByIdAsync(customer.IdentityUserID).Result.Balance;
            // проверить менеджер или владелец заказа
            if (User.IsInRole(Identity.IdentityConstants.ManagerRole) || order.IdCustomers == customer.Id)
            {
                var model = new OrderInfoViewModel
                {
                    MoneyOfUser = Money,
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
            var orderTasks = _orderService.GetAllOrderTasks().Where(x => x.IdOrder == id).ToList();
            var taskDtos = _orderService.GetTasksByOrderId(id);
            var order = _orderService.GetOrderById(id);
            var selectedOrderTask = _orderService.GetAllOrderTasks().Where(x => x.IdOrder == id).ToList();

            return taskDtos.Select(x => new TaskViewModel 
            { 
                Id = x.Id,
                BrigadeName = _brigadeService.GetAllBrigades().FirstOrDefault(y => y.Id == x.IdBrigade).Title,
                SpecializationName = _brigadeService.GetAllSpecializations()
                                        .FirstOrDefault(y => y.Id == x.IdSpecialization).Name,
                Title = x.Title, 
                Description = x.Description,
                Price = x.Price,
                TaskCompletionDate = orderTasks.FirstOrDefault(y => y.IdTask == x.Id).TaskCompletionDate,
                Status = (TaskStatus)x.Status
            }).ToList();
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

        [HttpGet]
        [Authorize(Roles = Identity.IdentityConstants.CustomerRole)]
        public ActionResult SelectTaskSpecialization(int orderId)
        {
            var specializations = _brigadeService.GetAllSpecializations();

            var specItem = specializations.Select(x => new SpecializationItem
            {
                SpecializationId = x.Id,
                Name = x.Name,
            });
            var model = new TaskViewModel
            {
                SpecializationItems = specItem,
                OrderId = orderId,
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult SelectTaskSpecialization(TaskSpecializationViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                return RedirectToAction("SelectTask", new { id = model.IdSpecialization, orderId = model.OrderId });
            }
            return RedirectToAction("SelectTaskSpecialization");
        }

        [HttpGet]
        public ActionResult SelectTask(int id, int orderId)
        {
            var taskOrders = _orderService.GetAllOrderTasks().Where(x => x.IdOrder == orderId).ToList();
            var specTasks = _brigadeService.FindTasksBySpecialization(id);
            var tasks = specTasks.Where(x => !taskOrders.Any(y => y.IdTask == x.Id)).ToList().AsReadOnly();
            var model = new TaskViewModel
            {
                OrderId = orderId,
                IdSpecialization = id,
                TaskItems = tasks.Select(x => new TaskItem { Name = x.Title + " - $" + x.Price, TaskId = x.Id }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult SelectTask(TaskViewModel model)
        {
            if(model != null && ModelState.IsValid)
            {
                return RedirectToAction("SelectDateTask", new { orderId = model.OrderId, taskId = model.Id, specId = model.IdSpecialization });
            }
            return RedirectToAction("SelectTask", new { id = model.Id, orderId = model.OrderId, specId = model.IdSpecialization });
        }
        private bool CheckDate(DateTimeOffset currentDay, int specId)
        {
            return !(currentDay <= DateTimeOffset.Now) && _orderService.FindFreeBrigadeForDate(currentDay, specId) != null;
        }

        [HttpGet]
        public ActionResult SelectDateTask(int orderId, int taskId, int specId)
        {
            int weeks = 2;
            var currentDayOfWeek = (int)DateTimeOffset.Now.DayOfWeek;
            var dateDiff = currentDayOfWeek - 1;
            var mondayDate = DateTimeOffset.Now.AddDays(-dateDiff);
            var AllowedDays = new List<List<(DateTimeOffset, bool)>>();
            for(int i = 0; i < weeks; i++)
            {
                AllowedDays.Add(new List<(DateTimeOffset, bool)>());
                for(int j = 0; j < 7; j++)
                {
                    var currentDay = mondayDate.AddDays(7 * i + j);

                    AllowedDays[i].Add((currentDay, CheckDate(currentDay, specId)));
                }
            }
            var model = new TaskDateViewModel
            {
                AllowedDays = AllowedDays,
                OrderId = orderId,
                TaskId = taskId,
                SpecializationId = specId,
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult ConfirmTask(int orderId, int taskId, DateTimeOffset date)
        {
            var task = _orderService.GetTaskById(taskId);
            var model = new TaskViewModel
            {
                Id = taskId,
                OrderId = orderId,
                Title = task.Title,
                TaskCompletionDate = date,
                Price = task.Price,
                SpecializationName = _brigadeService.GetSpecializationById(task.IdSpecialization).Name,
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult CreateOrderTask(int orderId, int taskId, DateTimeOffset date)
        {
            _orderService.CreateOrderTask(new OrderTaskDto { IdOrder = orderId, IdTask = taskId, TaskCompletionDate = date });
            return RedirectToAction("Info", new { id = orderId });
        }

        [HttpGet]
        public ActionResult DeleteTask(int idTask, int idOrder)
        {
            var orderTasks = _orderService.GetAllOrderTasks().Where(x => x.IdOrder == idOrder).ToList();
            var task = _orderService.GetTaskById(idTask);
            var model = new TaskViewModel
            {
                Id = idTask,
                OrderId = idOrder,
                SpecializationName = _orderService.GetSpecializationById(task.IdSpecialization).Name,
                Description = task.Description,
                Price = task.Price,
                TaskCompletionDate = orderTasks.FirstOrDefault(y => y.IdTask == idTask).TaskCompletionDate,
                /*<td>@Model.Item2.SpecializationName</td>
                <td>@Model.Item2.Description</td>
                <td>@Model.Item2.Price</td>
                <td>@Model.Item2.TaskCompletionDate.ToString("d")</td>*/
            };
            return View(model);
        }
        [HttpGet]
        public ActionResult ConfirmDeleteTask(int idTask, int idOrder)
        {
           _orderService.DeleteOrderTask(_orderService.FindOrderTaskByOrderAndTaskIds(idOrder, idTask));

            return RedirectToAction("Index", "Orders", null);
        }
        [HttpGet]
        [ExceptionFilter()]
        public ActionResult Pay(int idOrder)
        {
            var totalPrice = _orderService.GetOrderPrice(idOrder); 
            var user = UserManager.FindByNameAsync(User.Identity.Name).Result;
            var order = _orderService.GetOrderById(idOrder);
            if (totalPrice <= user.Balance)
            {
                user.Balance -= totalPrice;
                var item = UserManager.UpdateAsync(user).Result;
                order.OrderStatus = (int)OrderStatus.Paid;
                _orderService.UpdateOrder(order);
                return RedirectToAction("Info", "Orders", new { id = idOrder });
            }
            else
            {
                throw new BusinessLogic.Exceptions.BusinessLogicException("Not enough money to pay for order!");
            }
        }
        [HttpGet]
        public ActionResult Refuse(int idOrder)
        {
            var order = _orderService.GetOrderById(idOrder);
            order.OrderStatus = (int)OrderStatus.Сanceled;
            _orderService.UpdateOrder(order);
            var orderTasks = _orderService.GetAllOrderTasks().Where(x => x.IdOrder == idOrder).ToList();
            foreach(var item in orderTasks)
                _orderService.DeleteOrderTask(item.Id);
            return RedirectToAction("Info", "Orders", new { id = idOrder });
        }
    }
}