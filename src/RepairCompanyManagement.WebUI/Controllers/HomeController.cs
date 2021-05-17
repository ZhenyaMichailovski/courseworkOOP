using AutoMapper;
using RepairCompanyManagement.BusinessLogic.Dtos;
using RepairCompanyManagement.BusinessLogic.Interfaces;
using RepairCompanyManagement.WebUI.Filters;
using RepairCompanyManagement.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RepairCompanyManagement.WebUI.Controllers
{
    public class HomeController : IdentityBaseController
    {
        private IOrderService _orderService { get; set; }
        private IBrigadeService _brigadeService { get; set; }
        private IUserService _userService { get; set; }
        private IMapper _mapper { get; set; }


        public HomeController(IOrderService orderService, IBrigadeService brigadeService, IUserService userService, IMapper mapper)
        {
            _orderService = orderService;
            _brigadeService = brigadeService;
            _userService = userService;
            _mapper = mapper;
        }
        public ActionResult Index()
        {
            var feedback = _orderService.GetAllFeedbacks();
            var model = feedback.Select(x => new ReportViewModel
            {
                Report = x.Review,
               // CustomerName = UserManager.FindByIdAsync(_orderService.GetCustomerIdentiryByOrder(x.IdOrder)).Result.FirstName,
            }).ToList().AsReadOnly();
            return View(model);
        }

        [HttpGet]
        public ActionResult PriceList()
        {
            var model = new List<PriceListViewModel>();

            var tasks = _orderService.GetAllTasks().ToList();

            var singleTasks = tasks.Select(x => x.Title).Distinct();

            foreach(var item in singleTasks)
            {
                model.Add(new PriceListViewModel
                {
                    Task = item,
                    Price = "$" + tasks.Where(x => x.Title == item).Min(x => x.Price).ToString() + " - " + tasks.Where(x => x.Title == item).Max(x => x.Price).ToString()
                });
            }

            return View(model);
        }
    }
}