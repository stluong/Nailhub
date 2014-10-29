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
using Generic.Infracstructure.Identity;
using System.Data;

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
                _nameOrConnectionString = (value.Contains("name=") || value.Contains("Initial Catalog="))
                    ? value
                    : string.Format("name={0}", value)
                ;
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
        /// <param name="myAppAssembly">Typeof(MvcApplication).Assembly. It usually uses to register controllers</param>
        /// <param name="setResolver">Default is true</param>
        /// <param name="myContext">App context. If not specify, It will initiate MyContext</param>
        /// <param name="initializeAdminIdentity">Initialize default admin identiy:)</param>
        public static void RegisterCore(Assembly myAppAssembly = null, bool setResolver = true, string identityNameOrConnectionString = null, MyContext myContext = null, bool? initializeAdminIdentity = null)
        {
            var coreBuilder = Builder;
            if (myAppAssembly != null || MyAppAssembly != null) {
                coreBuilder.RegisterControllers(myAppAssembly ?? MyAppAssembly);
            }
            
            coreBuilder.RegisterModule<AutofacWebTypesModule>();
            coreBuilder.RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>))
                .As(typeof(IRepositoryAsync<>))
                .InstancePerRequest()
            ;
            coreBuilder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerRequest();
            coreBuilder.RegisterType(typeof(UnitOfWork))
                .As(typeof(IUnitOfWork))
                .As(typeof(IUnitOfWorkAsync))
                .InstancePerRequest()
            ;
            coreBuilder.RegisterType(typeof(RepositoryProvider)).As(typeof(IRepositoryProvider))
                .InstancePerRequest()
            ;
            coreBuilder.RegisterType(typeof(RepositoryFactory)).InstancePerRequest();

            //Register identiy context
            coreBuilder.Register<IIdentityContext>(b =>
            {
                var logger = b.Resolve<ILogger>();
                var identityContext = new IdentityContext(identityNameOrConnectionString?? NameOrConnectionString, logger, initializeAdminIdentity ?? InitializeAdminIdentity);
                return identityContext;
            }).InstancePerRequest();
            //Register identity module
            coreBuilder.RegisterModule(new IdentityModule());
            coreBuilder.Register(b => NLogLogger.Instance).InstancePerRequest();

            //Register my app context
            coreBuilder.Register<IMyContext>(b =>
            {
                var logger = b.Resolve<ILogger>();
                var context = myContext;
                return context;
            }).InstancePerRequest();
            coreBuilder.Register<IMyContextAsync>(b =>
            {
                var logger = b.Resolve<ILogger>();
                var context = myContext;
                return context;
            }).InstancePerRequest();
            
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
