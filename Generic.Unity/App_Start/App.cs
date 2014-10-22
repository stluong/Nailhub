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
using Generic;
using System.Reflection;

//using Test.Web;

//[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(App), "RegisterCore")]

namespace Generic
{
    public class App
    {
        private static ContainerBuilder _builder;
        private static string _nameOrConnectionString;
        private static bool _initializeAdminIdentity;
        private static Assembly _myAppAssembly;

        public static Assembly MyAppAssembly
        {
            get
            {
                return _myAppAssembly;
            }
            set
            {
                _myAppAssembly = value;
            }
        }
        public static bool InitializeAdminIdentity
        {
            get
            {
                return _initializeAdminIdentity;
            }
            set
            {
                _initializeAdminIdentity = value;
            }
        }
        public static string NameOrConnectionString
        {
            get
            {
                return _nameOrConnectionString = string.IsNullOrEmpty(_nameOrConnectionString) ? "name=AppContext" : _nameOrConnectionString.Trim();
            }
            set
            {
                _nameOrConnectionString = value;
            }
        }
        public static ContainerBuilder Builder { 
            get{
                return _builder = _builder?? new ContainerBuilder();
            } 
            set{
                _builder = value;
            } 
        }

        ///// <summary>
        /////  Register all dependencies, except builder.RegisterControllers(typeof(MvcApplication).Assembly). Call Commit to resolve
        ///// </summary>
        ///// <param name="initiateAdmin">default is false</param>
        //public static void RegisterDependencies(bool initiateAdmin = false)
        //{
        //    var coreBuilder = builder;//new ContainerBuilder();
        //    const string nameOrConnectionString = "name=AppContext";
        //    //builder.RegisterControllers(typeof(MvcApplication).Assembly);
        //    Register(coreBuilder, nameOrConnectionString: nameOrConnectionString);            
        //}
        ///// <summary>
        ///// Register all dependencies including all controllers in myAppAssembly. Call Commit to resolve
        ///// </summary>
        ///// <param name="myAppAssembly">typeof(MvcApplication).Assembly</param>
        ///// <param name="initiateAdmin">default is false</param>
        //public static void RegisterDependencies(System.Reflection.Assembly myAppAssembly, bool initiateAdmin = false)
        //{
        //    var coreBuilder = builder;//new ContainerBuilder();
        //    const string nameOrConnectionString = "name=AppContext";
        //    coreBuilder.RegisterControllers(myAppAssembly);
        //    Register(coreBuilder, nameOrConnectionString: nameOrConnectionString, initiateAdmin: initiateAdmin);
        //}
        ///// <summary>
        ///// Register all dependencies. Call Commit to resolve
        ///// </summary>
        ///// <param name="myAppAssembly">typeof(MvcApplication).Assembly</param>
        ///// <param name="initiateAdmin">default is false</param>
        //public static void RegisterDependencies(System.Reflection.Assembly myAppAssembly, IList<Module> modules, string nameOrConnectionString = null, bool initiateAdmin = false)
        //{
        //    var coreBuilder = builder;//new ContainerBuilder();
        //    nameOrConnectionString = nameOrConnectionString ?? "name=AppContext";
        //    coreBuilder.RegisterControllers(myAppAssembly);
        //    Register(coreBuilder, modules, nameOrConnectionString, initiateAdmin);
        //}
        ///// <summary>
        ///// Resolve dependencies
        ///// </summary>
        //public static void Commit() {
        //    var coreContainer = builder.Build();
        //    DependencyResolver.SetResolver(new AutofacDependencyResolver(coreContainer));
        //}

        /// <summary>
        /// Register core dependencies, and whether all controllers in app assembly
        /// </summary>
        /// <param name="myAppAssembly">typeof(MvcApplication).Assembly</param>
        /// <param name="nameOrConnectionStringContext">Name or connection string for app context</param>
        public static void RegisterCore(Assembly myAppAssembly = null, string nameOrConnectionStringContext = null)
        {
            var coreBuilder = new ContainerBuilder();
            if (myAppAssembly != null || MyAppAssembly != null) {
                coreBuilder.RegisterControllers(myAppAssembly ?? MyAppAssembly);
            }
            if (!string.IsNullOrEmpty(nameOrConnectionStringContext)) {
                nameOrConnectionStringContext = nameOrConnectionStringContext.Contains("app=")
                    ? nameOrConnectionStringContext
                    : string.Format("app={0}", nameOrConnectionStringContext)
                ;
            }
            coreBuilder.RegisterModule<AutofacWebTypesModule>();
            coreBuilder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            coreBuilder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepositoryAsync<>)).InstancePerLifetimeScope();
            coreBuilder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();
            coreBuilder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerLifetimeScope();
            coreBuilder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWorkAsync)).InstancePerLifetimeScope();
            coreBuilder.RegisterType(typeof(RepositoryProvider)).As(typeof(IRepositoryProvider))
                .InstancePerLifetimeScope()
            ;
            coreBuilder.RegisterType(typeof(RepositoryFactory)).InstancePerLifetimeScope();

            coreBuilder.Register<IMyContext>(b =>
            {
                var logger = b.Resolve<ILogger>();
                var context = new MyContext(nameOrConnectionStringContext?? NameOrConnectionString, logger, InitializeAdminIdentity);
                return context;
            }).InstancePerLifetimeScope();
            coreBuilder.Register<IMyContextAsync>(b =>
            {
                var logger = b.Resolve<ILogger>();
                var context = new MyContext(nameOrConnectionStringContext ?? NameOrConnectionString, logger, InitializeAdminIdentity);
                return context;
            }).InstancePerLifetimeScope();

            coreBuilder.Register(b => NLogLogger.Instance).SingleInstance();
            //Register identity module
            coreBuilder.RegisterModule(new IdentityModule());

            var coreContainer = coreBuilder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(coreContainer));
           
        }
    }
}
