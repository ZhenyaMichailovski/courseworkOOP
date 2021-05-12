using AutoMapper;
using Microsoft.AspNet.Identity;
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
    public class ProfileController : IdentityBaseController
    {
        private const decimal MinBalance = 0.0m;
        private RepairCompanyManagement.BusinessLogic.Interfaces.IUserService _userService { get; set; }
        private IMapper _mapper { get; set; }
        public ProfileController(IUserService userService, IMapper mapper, ApplicationUserManager userManager, ApplicationSignInManager signInManager)
            : base(userManager, signInManager)
        {
            _userService = userService;
            _mapper = mapper;
        }

        // GET: /Manage/Index
        [HttpGet]
        public ActionResult Index()
        {
            var userName = User.Identity.Name;
            var userData = UserManager.FindByNameAsync(userName).Result;
            decimal selary = 0;
            var employee = _userService.GetAllEmployees().FirstOrDefault(x => x.IdentityUserID == userData.Id);
            if (employee != null)
            {
                selary = _userService.GetSelaryByBrigadeId(employee.IdBrigade) * employee.Salary;
                
            }
            var model = new IndexProfileViewModel
            {
                Surname = userData.Surname,
                FirstName = userData.FirstName,
                Balance = userData.Balance,
                Email = userData.Email,
                Salary = selary,
            };

            return View(model);
        }

        // GET: /Profile/Edit
        [HttpGet]
        public ActionResult Edit()
        {
            var userData = UserManager.FindByNameAsync(User.Identity.Name).Result;

            var model = new EditProfileViewModel
            {
                Surname = userData.Surname,
                FirstName = userData.FirstName,
                Balance = userData.Balance,
                Email = userData.Email,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditProfileViewModel model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return View(model);
            }

            if (model.Balance < MinBalance)
            {
                ModelState.AddModelError("Balance", "Balance cannot be less than " + MinBalance);
                return View(model);
            }

            var user = UserManager.FindByNameAsync(User.Identity.Name).Result;
            user.Surname = model.Surname;
            user.FirstName = model.FirstName;
            user.Balance = model.Balance;
            user.Email = model.Email;

            var result = UserManager.UpdateAsync(user).Result;
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            AddErrors(result);
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}