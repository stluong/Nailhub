using Test.Web.Models;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using System.Collections.Generic;
using Generic.Core.Context;
using Entity;
using Generic.Core.Logging;

namespace Test.Web
{
    // Note: For instructions on enabling IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=301868
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //register all dependency
            //var modules = new List<Module>();
            //modules.Add(new ServiceModule());
            //Generic.App.RegisterDependencies(typeof(MvcApplication).Assembly, modules);

            //var mybuilder = Generic.App.builder;
            //mybuilder.Register<IMyContext>(b =>
            //{
            //    var logger = b.Resolve<ILogger>();
            //    var context = new NailhubsEntities("name=AppContext", logger);
            //    return context;
            //}).InstancePerLifetimeScope();
            //mybuilder.Register<IMyContextAsync>(b =>
            //{
            //    var logger = b.Resolve<ILogger>();
            //    var context = new NailhubsEntities("name=AppContext", logger);
            //    return context;
            //}).InstancePerLifetimeScope();

            //Generic.App.MyAppAssembly = typeof(MvcApplication).Assembly;
            Generic.App.RegisterCore(typeof(MvcApplication).Assembly, "AppContext");
        }
    }
}
