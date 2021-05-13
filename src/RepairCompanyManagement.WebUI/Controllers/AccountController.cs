using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using RepairCompanyManagement.BusinessLogic.Interfaces;
using RepairCompanyManagement.WebUI.Identity;
using RepairCompanyManagement.WebUI.Models;
using RepairCompanyManagement.WebUI.Identity;

namespace RepairCompanyManagement.WebUI.Controllers
{
    public class AccountController : IdentityBaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IUserService _userService;
        
        public AccountController()
            : base()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IUserService userService)
            : base(userManager, signInManager)
        {
            _userService = userService;
        }

        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnPath)
        {
            ViewBag.ReturnPath = returnPath;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnPath)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model != null)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, change to shouldLockout: true
                var result = SignInManager.PasswordSignIn(model.Email, model.Password, false, shouldLockout: false);
                switch (result)
                {
                    case SignInStatus.Success:
                        return RedirectToLocal(returnPath);
                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new { ReturnUrl = returnPath, RememberMe = false });
                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError("", "Invalid login attempt.");
                        return View(model);
                }
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }

        // GET: /Account/Register
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid && model != null)
            {
                var user = new User { UserName = model.Email, Email = model.Email, Surname = model.Surname, FirstName = model.FirstName, Balance = 0m, };
                var result = UserManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, IdentityConstants.CustomerRole);
                    SignInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                    _userService.CreateCustomer(new BusinessLogic.Dtos.CustomerDto { IdentityUserID = user.Id });
                    return RedirectToAction("Index", "Home");
                }

                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/ChangePassword
        [HttpGet]
        [Authorize(Roles = IdentityConstants.AdminRole+","+IdentityConstants.CustomerRole)]
        public ActionResult ChangePassword()
        {
            
            return View();
        }

        // POST: /Account/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return View(model);
            }

            var result = UserManager.ChangePassword(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = UserManager.FindById(User.Identity.GetUserId());
                if (user != null)
                {
                    SignInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                }

                return RedirectToAction("Index", "Profile", null);
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

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}