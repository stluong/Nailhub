using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Generic.Core.Context;
using Generic.Core.Logging;
using Generic.Core.Repository;
using Generic.Core.Service;
using Generic.Core.UnitOfWork;
using Generic.Infracstructure.Services;
using Generic.Infracstructure.UnitOfWorks;
using Generic.Infrastructure.Logging;
using Generic.Infrastructure.Repositories;
using Generic.Unity;

//using Test.Web;


//[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(DI), "RegisterDependencies")]

namespace Generic.Unity
{
    public class DI
    {
        /// <summary>
        /// Return builder, so we can register more
        /// </summary>
        /// <returns></returns>
        public static ContainerBuilder RegisterDependencies(bool initiateAdmin = false)
        {
            var builder = new ContainerBuilder();
            const string nameOrConnectionString = "name=AppContext";
            //builder.RegisterControllers(typeof(MvcApplication).Assembly);
            Register(builder, nameOrConnectionString);
            return builder;
        }
        /// <summary>
        /// Register all dependencies
        /// </summary>
        /// <param name="myAppAssembly">typeof(MvcApplication).Assembly</param>
        public static void RegisterDependencies(System.Reflection.Assembly myAppAssembly, bool initiateAdmin = false)
        {
            var builder = new ContainerBuilder();
            const string nameOrConnectionString = "name=AppContext";
            builder.RegisterControllers(myAppAssembly);
            Register(builder, nameOrConnectionString, initiateAdmin);
        }

        private static void Register(ContainerBuilder builder, string nameOrConnectionString, bool initiateAdmin = false)
        {
            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerHttpRequest();
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerHttpRequest();
            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerHttpRequest();
            builder.Register<IMyContext>(b =>
            {
                var logger = b.Resolve<ILogger>();
                var context = new MyContext(nameOrConnectionString, logger, initiateAdmin);
                return context;
            }).InstancePerHttpRequest();
            builder.Register(b => NLogLogger.Instance).SingleInstance();
            builder.RegisterModule(new IdentityModule());

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
