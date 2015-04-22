using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using EFColuc;
using TNTHelper;

namespace Mybrus
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //StevenLuong, 04/21/2014: Core IdentityContext is code first
            //Dictionary<string, string> efConnection = "name=CoLucEntities".ToEFConnection();
            //TNT.App.RegisterCore(typeof(MvcApplication).Assembly, false, efConnection["CF"]);
            //TNT.App.RegisterContext(() => new CoLucEntities(efConnection["DF"]), false);
            //TNT.App.RegisterByConfig("autofac");

            //Or use efconnection
            TNT.App.InitEFConnection("name=CoLucEntities".GetConnnectionString());
            TNT.App.RegisterCore(typeof(MvcApplication).Assembly, false);
            TNT.App.RegisterContext(() => new CoLucEntities(TNT.App.EFConnection.ConnectionString), false);
            TNT.App.RegisterByConfig("autofac");
        }

        
    }
}
