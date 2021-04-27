using AutoMapper;
using RepairCompanyManagement.BusinessLogic.Dtos;
using RepairCompanyManagement.BusinessLogic.Interfaces;
using RepairCompanyManagement.WebUI.Filters;
using RepairCompanyManagement.WebUI.Identity;
using RepairCompanyManagement.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RepairCompanyManagement.WebUI.Controllers
{
    public class UsersController : IdentityBaseController
    {
        private RepairCompanyManagement.BusinessLogic.Interfaces.IUserService _userService { get; set; }
        private IMapper _mapper { get; set; }

        public UsersController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IUserService userService, IMapper mapper)
            : base(userManager, signInManager)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        [ExceptionFilter()]
        public ActionResult Index()
        {
            var managersIds = _userService.GetAllManagers().Select(x => x.IdentityUserID).ToList();
            var employeesIds = _userService.GetAllEmployees().Select(x => x.IdentityUserID).ToList();
            var customersIds = _userService.GetAllCustomers().Select(x => x.IdentityUserID).ToList();
            EmployeeFullInfoViewModel fullInfo;
            UsersIndexViewModel model = new UsersIndexViewModel()
            {
                Managers = new List<User>(),
                Customers = new List<User>(),
                Employee = new List<EmployeeFullInfoViewModel>()
            };

            foreach (var one in managersIds)
            {
                model.Managers.Add(UserManager.FindByIdAsync(one).Result);
            }
            foreach (var one in customersIds)
            {
                model.Customers.Add(UserManager.FindByIdAsync(one).Result);
            }
            foreach (var one in employeesIds)
            {
                fullInfo = _mapper.Map<EmployeeFullInfoViewModel>(UserManager.FindByIdAsync(one).Result);
                var brigadeJobPosition = _userService.GetEmployeeFullInfo(one);
                fullInfo.BrigadeName = brigadeJobPosition.Item1;
                fullInfo.JobPositionName = brigadeJobPosition.Item2;
                model.Employee.Add(fullInfo);
            }
            
            return View(model);
        }

        [HttpGet]
        [ExceptionFilter("Index")]
        public ActionResult Update(int id)
        {
            //var model = _mapper.Map<UsersIndexViewModel>(_orderService.GetManagerById(id));
            return View(/*model*/);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ManagerViewModel model)
        {
           /* if (model != null && ModelState.IsValid)
            {
                _orderService.UpdateManager(_mapper.Map<ManagerDto>(model));

                return RedirectToAction("Index", "Managers", null);
            }*/

            return View(model);
        }

        [HttpGet]
        [ExceptionFilter("Index")]
        public ActionResult Delete(int id)
        {
            //var model = _mapper.Map<ManagerViewModel>(_orderService.GetManagerById(id));

            return View(/*model*/);
        }

        [HttpGet]
        [ExceptionFilter("Index")]
        public ActionResult ConfirmDelete(int id)
        {
            /*var model = _mapper.Map<ManagerViewModel>(_orderService.GetManagerById(id));
            _orderService.DeleteManager(id);*/

            return RedirectToAction("Index", "Managers", null);
        }
    }
}