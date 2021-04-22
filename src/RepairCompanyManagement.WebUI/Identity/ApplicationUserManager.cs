using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace RepairCompanyManagement.WebUI.Identity
{
    public class ApplicationUserManager : UserManager<User>
    {
        public ApplicationUserManager(IUserStore<User> store)
            : base(store)
        {
        }

        public ApplicationUserManager(IUserStore<User> store, IdentityFactoryOptions<ApplicationUserManager> options)
        : base(store)
        {
            // Configure validation logic for usernames
            UserValidator = new UserValidator<User>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true,
            };

            // Configure validation logic for passwords
            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            UserLockoutEnabledByDefault = true;
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            MaxFailedAccessAttemptsBeforeLockout = 5;

            // manager.EmailService = new EmailService()
            if (options != null && options.DataProtectionProvider != null)
            {
                UserTokenProvider =
                    new DataProtectorTokenProvider<User>(options.DataProtectionProvider.Create("ASP.NET Identity"));
            }
        }
    }
}