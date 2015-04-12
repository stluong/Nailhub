﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using EFColuc;

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

            TNT.App.RegisterCore(typeof(MvcApplication).Assembly, false);
            TNT.App.RegisterContext(() => new CoLucEntities("name=CoLucEntities"));
            TNT.App.RegisterByConfig("autofac");
        }
    }
}
