using Autofac;
using RepairCompanyManagement.BusinessLogic.Services;

namespace RepairCompanyManagement.BusinessLogic
{
    public class DiModule : Module
    {
        private readonly string _connectionString;

        public DiModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new RepairCompanyManagement.DataAccess.DiModule(_connectionString));
            
            // here we should add our BusinessLogic services
            // to manipulate on repositories

            builder.RegisterType<BrigadeService>()
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<OrderService>()
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<UserService>()
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<ReportService>()
                .AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
