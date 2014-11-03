using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using TNT.Core.Context;

namespace Nailhub
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            TNT.App.RegisterCore(typeof(MvcApplication).Assembly, false);
            
            var builder = TNT.App.Builder;
            builder.Register<IMyContext>(c =>
            {
                return new CFEntity.Models.NailhubsContext("name=AppContext");
            }).InstancePerRequest();

            builder.Register<IMyContextAsync>(c =>
            {
                return new CFEntity.Models.NailhubsContext("name=AppContext");
            }).InstancePerRequest();

            TNT.App.SetResolver(builder);
           
        }
    }
}
