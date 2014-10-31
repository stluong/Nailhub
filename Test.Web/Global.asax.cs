using Test.Web.Models;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Collections.Generic;
using Generic.Core.Context;
using Generic.Core.Logging;
using System.Configuration;
using CFEntity.Models;
using Autofac;


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

            Generic.App.RegisterCore(typeof(MvcApplication).Assembly, false);

            Generic.App.Builder.Register<IMyContext>(b =>
            {
                var logger = b.Resolve<ILogger>();
                var context = new NailhubsContext("name=AppContext");
                return context;
            }).InstancePerRequest();
            Generic.App.Builder.Register<IMyContextAsync>(b =>
            {
                var logger = b.Resolve<ILogger>();
                var context = new NailhubsContext("name=AppContext");
                return context;
            }).InstancePerRequest();

            Generic.App.RegisterByConfig("autofac");
            
        }
    }
}
