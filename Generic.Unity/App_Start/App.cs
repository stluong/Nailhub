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
using System.Collections.Generic;

//using Test.Web;

//[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(DI), "RegisterDependencies")]

namespace Generic
{
    public class App
    {
        private static ContainerBuilder _builder;

        public static ContainerBuilder builder { 
            get{
                return _builder?? new ContainerBuilder();
            } 
            set{
                _builder = value;
            } 
        }

        /// <summary>
        ///  Register all dependencies, except builder.RegisterControllers(typeof(MvcApplication).Assembly);
        /// </summary>
        /// <param name="initiateAdmin">default is false</param>
        public static void RegisterDependencies(bool initiateAdmin = false)
        {
            var coreBuilder = builder;//new ContainerBuilder();
            const string nameOrConnectionString = "name=AppContext";
            //builder.RegisterControllers(typeof(MvcApplication).Assembly);
            Register(coreBuilder, nameOrConnectionString: nameOrConnectionString);            
        }
        /// <summary>
        /// Register all dependencies including all controllers in myAppAssembly
        /// </summary>
        /// <param name="myAppAssembly">typeof(MvcApplication).Assembly</param>
        /// <param name="initiateAdmin">default is false</param>
        public static void RegisterDependencies(System.Reflection.Assembly myAppAssembly, bool initiateAdmin = false)
        {
            var coreBuilder = builder;//new ContainerBuilder();
            const string nameOrConnectionString = "name=AppContext";
            coreBuilder.RegisterControllers(myAppAssembly);
            Register(coreBuilder, nameOrConnectionString: nameOrConnectionString, initiateAdmin: initiateAdmin);
        }
        /// <summary>
        /// Register all dependencies
        /// </summary>
        /// <param name="myAppAssembly">typeof(MvcApplication).Assembly</param>
        /// <param name="initiateAdmin">default is false</param>
        public static void RegisterDependencies(System.Reflection.Assembly myAppAssembly, IList<Module> modules, string nameOrConnectionString = null, bool initiateAdmin = false)
        {
            var coreBuilder = builder;//new ContainerBuilder();
            nameOrConnectionString = nameOrConnectionString ?? "name=AppContext";
            coreBuilder.RegisterControllers(myAppAssembly);
            Register(coreBuilder, modules, nameOrConnectionString, initiateAdmin);
        }

        private static void Register(ContainerBuilder coreBuilder, IList<Module> modules = null, string nameOrConnectionString = null, bool initiateAdmin = false)
        {
            coreBuilder.RegisterModule<AutofacWebTypesModule>();
            coreBuilder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerRequest();
            coreBuilder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerRequest();
            coreBuilder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerRequest();
            coreBuilder.Register<IMyContext>(b =>
            {
                var logger = b.Resolve<ILogger>();
                var context = new MyContext(nameOrConnectionString, logger, initiateAdmin);
                return context;
            }).InstancePerRequest();
            coreBuilder.Register(b => NLogLogger.Instance).SingleInstance();
            //Register all my modules
            coreBuilder.RegisterModule(new MyModule(modules));

            var coreContainer = coreBuilder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(coreContainer));
           
        }
    }
}
