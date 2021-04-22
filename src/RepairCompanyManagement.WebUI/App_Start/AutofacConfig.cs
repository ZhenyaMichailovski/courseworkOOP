using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using RepairCompanyManagement.BusinessLogic.Mapper;
using RepairCompanyManagement.WebUI.Contexts;
using RepairCompanyManagement.WebUI.Identity;
using RepairCompanyManagement.WebUI.Mapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;

namespace RepairCompanyManagement.WebUI.App_Start
{
    public class AutofacConfig
    {
        private const string connectionStringName = "DefaultConnect";

        private AutofacConfig()
        {
        }

        public static void Run()
        {
            ConfigureContainer();
        }

        public static void ConfigureContainer()
        {
            var connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;

            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModule<AutofacWebTypesModule>();

            builder.RegisterType<UserStore<User>>().As<IUserStore<User>>().InstancePerRequest();
            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
            builder.Register<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
            builder.RegisterType<IdentityContext>().WithParameter("connectionString", connectionString)
                .AsSelf().As<DbContext>().InstancePerRequest();

            builder.RegisterModule(new RepairCompanyManagement.BusinessLogic.DiModule(connectionString));

            builder.Register(ctx => new MapperConfiguration(cfg =>
                cfg.AddProfiles(new List<Profile>
                    {
                        new BusinessLogicMapperProfile(),
                        new WebUIMapperProfile(),
                    })))
                .AsSelf().SingleInstance();
            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}