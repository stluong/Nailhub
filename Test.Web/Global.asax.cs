using Test.Web.Models;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Collections.Generic;
using Generic.Core.Context;
using Entity;
using Generic.Core.Logging;
using System.Configuration;

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

            Generic.App.RegisterCore(typeof(MvcApplication).Assembly, ConfigurationManager.ConnectionStrings["AppContext"].ConnectionString, false);
            Generic.App.RegisterByConfig("autofac");
            
        }
    }
}
