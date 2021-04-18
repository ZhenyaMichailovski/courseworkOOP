using Autofac;
using RepairCompanyManagement.DataAccess.Repositories;

namespace RepairCompanyManagement.DataAccess
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
            // here we should add our repositories
            // to use them with Dependency Injection Autofac Container

            builder.RegisterType<SpecializationRepository>()
                .WithParameter("connectionString", _connectionString)
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterType<JobPositionRepository>()
                .WithParameter("connectionString", _connectionString)
                .AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
