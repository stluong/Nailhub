﻿using System.Web.Mvc;
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

        /// <summary>
        /// Register core dependencies, and whether all controllers in app assembly
        /// </summary>
        /// <param name="myAppAssembly">typeof(MvcApplication).Assembly</param>
        /// <param name="nameOrConnectionStringContext">Name or connection string for app context</param>
        /// <param name="initializeAdminIdentity">Initialize default admin identiy:)</param>
        public static void RegisterCore(Assembly myAppAssembly = null, string nameOrConnectionStringContext = null, bool? initializeAdminIdentity = null)
        {
            var coreBuilder = new ContainerBuilder();
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

            var coreContainer = coreBuilder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(coreContainer));
           
        }
    }
}
