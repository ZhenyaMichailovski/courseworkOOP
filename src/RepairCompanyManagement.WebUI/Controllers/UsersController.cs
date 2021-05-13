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
    public class UsersController : IdentityBaseController
    {
        private RepairCompanyManagement.BusinessLogic.Interfaces.IUserService _userService { get; set; }
        private RepairCompanyManagement.BusinessLogic.Interfaces.IBrigadeService _brigadeService { get; set; }
        private IMapper _mapper { get; set; }

        public UsersController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IUserService userService, IBrigadeService brigadeService, IMapper mapper)
            : base(userManager, signInManager)
        {
            _userService = userService;
            _brigadeService = brigadeService;
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
            var model = _mapper.Map<UsersIndexViewModel>(_userService.GetEmployeeById(id));
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

        [HttpGet]
        [ExceptionFilter("Ingex")]
        public ActionResult ChangeRole(string id, string roleName)
        {
            var user = UserManager.FindByIdAsync(id).Result;
            if (user is null)
            {
                throw new BusinessLogic.Exceptions.BusinessLogicException(BusinessLogic.Constants.UserNotFoundMassage);
            }
            if(roleName == IdentityConstants.EmployeeRole)
            {
                
                return RedirectToAction("ChangeSpecialization", new { id, }); // перенос на вьюшку для эмплоера
            }

            var remove = UserManager.RemoveFromRolesAsync(id, new string[]{ IdentityConstants.CustomerRole, IdentityConstants.EmployeeRole,
            IdentityConstants.ManagerRole, IdentityConstants.AdminRole}).Result;
            var add = UserManager.AddToRoleAsync(id, roleName).Result;
            _userService.RemoveFromRoles(id);

            if (roleName == IdentityConstants.CustomerRole)
                _userService.CreateCustomer(new CustomerDto { IdentityUserID = id });
            else if (roleName == IdentityConstants.ManagerRole)
                return RedirectToAction("SetSelary", new { id, }); //перенос на вьюшку для менеджера
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult ChangeEmployee(string id, int specId)
        {
            var jobPosition = _brigadeService.GetAllJobPositions();
            var brigade = _brigadeService.FindBrigadeBySpecialization(specId);
            ChangeEmployeeViewModel model = new ChangeEmployeeViewModel { Brigades = brigade.Select(x => new BrigadeItem{ Id = x.Id, Name = x.Title }).ToList(), JobPositions = jobPosition.Select(x => new JobPositionItem { Id = x.Id, Title = x.Title }).ToList(), IdentityUserId = id };
            return View(model);
        }
        [HttpGet]
        public ActionResult SetSelary(string id)
        {
            SetSelaryViewModel model = new SetSelaryViewModel { Selary = 0, IdentityUserId = id };
            return View(model);
        }
        [HttpPost]
        public ActionResult SetSelary(SetSelaryViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {

                _userService.CreateManager(new ManagerDto { Salary = model.Selary, IdentityUserID = model.IdentityUserId });

                return RedirectToAction("Index", "Users", null);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult ChangeEmployee(ChangeEmployeeViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var remov = UserManager.RemoveFromRolesAsync(model.IdentityUserId, new string[]{ IdentityConstants.CustomerRole, IdentityConstants.EmployeeRole,
                IdentityConstants.ManagerRole, IdentityConstants.AdminRole}).Result;
                var add = UserManager.AddToRoleAsync(model.IdentityUserId, IdentityConstants.EmployeeRole).Result;
                _userService.RemoveFromRoles(model.IdentityUserId);
                _userService.CreateEmployee(new EmployeeDto { IdBrigade = model.BrigadeId, IdJobPosition = model.JobPositionId , Salary = RepairCompanyManagement.BusinessLogic.Constants.SelaryCoefficient, IdentityUserID = model.IdentityUserId });
               
                return RedirectToAction("Index", "Users", null);
            }
            return View(model);
        }
        [HttpGet]
        [ExceptionFilter("Ingex")]
        public ActionResult ChangeSpecialization(string id)
        {
            var specializations = _brigadeService.GetAllSpecializations();
            var model = new UserChangeSpecializationViewModel
            {
                UserId = id,
                Specializations = new List<(int, string)>(),
            };

            foreach(var one in specializations)
            {
                model.Specializations.Add((one.Id, one.Name));
            }
            return View(model);
        }
    }
}