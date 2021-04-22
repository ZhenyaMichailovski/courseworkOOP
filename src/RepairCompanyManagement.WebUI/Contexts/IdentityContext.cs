using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using RepairCompanyManagement.WebUI.Identity;

namespace RepairCompanyManagement.WebUI.Contexts
{
    public class IdentityContext : IdentityDbContext<User>
    {
        public IdentityContext(string connectionString)
            : base(connectionString)
        {
            Identity = Guid.NewGuid();
        }

        public Guid Identity { get; }
    }
}
