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
using System.Reflection;
using Autofac.Configuration;

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
        /// <summary>
        /// Get autofac container builder to register more dependencies! Call SetResolver to resolve registers :)
        /// </summary>
        public static ContainerBuilder Builder { 
            get{
                return _builder = _builder?? new ContainerBuilder();
            } 
            set{
                _builder = value;
            } 
        }
        /// <summary>
        /// Resolve builder
        /// </summary>
        /// <param name="buider"></param>
        public static void SetResolver(ContainerBuilder buider) {
            DependencyResolver.SetResolver(new AutofacDependencyResolver(buider.Build()));
        }
        /// <summary>
        /// Register core dependencies, and whether all controllers in app assembly
        /// </summary>
        /// <param name="myAppAssembly">typeof(MvcApplication).Assembly</param>
        /// <param name="nameOrConnectionStringContext">Name or connection string for app context</param>
        /// <param name="setResolver">Default is true</param>
        /// <param name="initializeAdminIdentity">Initialize default admin identiy:)</param>
        public static void RegisterCore(Assembly myAppAssembly = null, string nameOrConnectionStringContext = null, bool setResolver = true, bool? initializeAdminIdentity = null)
        {
            var coreBuilder = Builder;
            if (myAppAssembly != null || MyAppAssembly != null) {
                coreBuilder.RegisterControllers(myAppAssembly ?? MyAppAssembly);
            }
            if (!string.IsNullOrEmpty(nameOrConnectionStringContext))
            {
                nameOrConnectionStringContext = (nameOrConnectionStringContext.Contains("name=") || nameOrConnectionStringContext.Contains("Initial Catalog="))
                    ? nameOrConnectionStringContext
                    : string.Format("name={0}", nameOrConnectionStringContext)
                ;
            }
            coreBuilder.RegisterModule<AutofacWebTypesModule>();
            coreBuilder.RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>))
                .As(typeof(IRepositoryAsync<>))
                .InstancePerLifetimeScope()
            ;
            coreBuilder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();
            coreBuilder.RegisterType(typeof(UnitOfWork))
                .As(typeof(IUnitOfWork))
                .As(typeof(IUnitOfWorkAsync))
                .InstancePerLifetimeScope()
            ;
            coreBuilder.RegisterType(typeof(RepositoryProvider)).As(typeof(IRepositoryProvider))
                .InstancePerLifetimeScope()
            ;
            coreBuilder.RegisterType(typeof(RepositoryFactory)).InstancePerLifetimeScope();

            coreBuilder.Register<IMyContext>(b =>
            {
                var logger = b.Resolve<ILogger>();
                var context = new MyContext(nameOrConnectionStringContext?? NameOrConnectionString, logger, initializeAdminIdentity?? InitializeAdminIdentity);
                return context;
            }).InstancePerLifetimeScope();
            coreBuilder.Register<IMyContextAsync>(b =>
            {
                var logger = b.Resolve<ILogger>();
                var context = new MyContext(nameOrConnectionStringContext ?? NameOrConnectionString, logger, initializeAdminIdentity?? InitializeAdminIdentity);
                return context;
            }).InstancePerLifetimeScope();

            coreBuilder.Register(b => NLogLogger.Instance).SingleInstance();
            //Register identity module
            coreBuilder.RegisterModule(new IdentityModule());
            //Testing register config
            //coreBuilder.RegisterModule(new ConfigurationSettingsReader("autofac"));
            
            if (setResolver) {
                var coreContainer = coreBuilder.Build();
                DependencyResolver.SetResolver(new AutofacDependencyResolver(coreContainer));
            } 
        }
        /// <summary>
        /// Register by autofac section name configuration
        /// </summary>
        /// <param name="autofacSectionName">autofac section name</param>
        /// <param name="setResolver">Default is true</param>
        public static void RegisterByConfig(string autofacSectionName, bool setResolver = true) {
            var configBuilder = Builder;
            configBuilder.RegisterModule(new ConfigurationSettingsReader(autofacSectionName));
            if (setResolver) {
                DependencyResolver.SetResolver(new AutofacDependencyResolver(configBuilder.Build()));
            }
        }
        /// <summary>
        /// Register by list of autofac modules
        /// </summary>
        /// <param name="autofacSectionName"></param>
        /// <param name="setResolver">Default is true</param>
        public static void RegisterByConfig(IList<Autofac.Module> modules, bool setResolver = true){
            var moduleBuilder = Builder;
            moduleBuilder.RegisterModule(new MyModule(modules));
            if (setResolver) {
                DependencyResolver.SetResolver(new AutofacDependencyResolver(moduleBuilder.Build()));
            }
        }
        
    }
}
