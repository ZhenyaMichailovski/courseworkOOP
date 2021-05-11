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
    [Authorize(Roles = RepairCompanyManagement.WebUI.Identity.IdentityConstants.EmployeeRole)]
    public class WorkloadsController : IdentityBaseController
    {
        private IOrderService _orderService { get; set; }
        private IBrigadeService _brigadeService { get; set; }
        private IUserService _userService { get; set; }
        private IMapper _mapper { get; set; }

        public WorkloadsController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IOrderService orderService, IBrigadeService brigadeService, IUserService userService, IMapper mapper)
            : base(userManager, signInManager)
        {
            _orderService = orderService;
            _brigadeService = brigadeService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        
        public ActionResult Index()
        {
            var user = UserManager.FindByNameAsync(User.Identity.Name).Result;
            var employee = _brigadeService.GetAllEmployees().FirstOrDefault(x => x.IdentityUserID == user.Id);
            var task = _orderService.GetAllTasks().Where(x => x.IdBrigade == employee.IdBrigade).Select(x => x.Id).ToList();
           
            var item = _orderService.GetAllOrderTasks().Where(x => task.Any(y => y == x.IdTask))
                .Select(x => new OrderTaskItem {
                    IdOrderTask = x.Id,
                    Date = x.TaskCompletionDate, 
                    Status = (OrderTaskStatus)x.Status,
                    NameCustomers = GetCumtomerNameByOrderId(x.IdOrder),
                    NameTask = _orderService.GetTaskById(x.IdTask).Title,
                    PhoneNumber = user.PhoneNumber,
                    Description = x.Description,
                }).ToList();
            var model = new WorkloadViewModel
            {
                IdBrigade = employee.IdBrigade,
                NameBrigade = _brigadeService.GetBrigadeById(employee.IdBrigade).Title,
                Item = item,
            };
            return View(model);
        }
        public string GetCumtomerNameByOrderId(int id)
        {
            var idCus = _orderService.GetOrderById(id).IdCustomers;
            var customerIdentity = _orderService.GetCustomerById(idCus).IdentityUserID;
            var customer = UserManager.FindByIdAsync(customerIdentity).Result;
            return customer.FirstName + " " + customer.Surname + " " + customer.LastName;
        }
        [HttpGet]
        public ActionResult ConfirmTask(int idOrderTask)
        {
            _orderService.ChangeOrderTaskStatus(idOrderTask);
            _orderService.ChangeOrderStatus(idOrderTask);
            
            return RedirectToAction("Index", "Workloads");
        }
    }
}