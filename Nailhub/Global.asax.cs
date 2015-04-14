using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TNT.Core.Context;
using TNT.Core.Logging;
//using CFEntity.Models;
//using DFEntity;
using EFNailhub;
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

            TNT.App.NameOrConnectionString = "NailhubEntities";
            TNT.App.RegisterCore(typeof(MvcApplication).Assembly, false);

            //TNT.App.Builder.Register<IMyContext>(b =>
            //{
            //    var logger = b.Resolve<ILogger>();
            //    //var context = new NailhubsContext("name=AppContext");
            //    var context = new NailhubsEntities("name=AppContext");
            //    return context;
            //}).InstancePerRequest();
            //TNT.App.Builder.Register<IMyContextAsync>(b =>
            //{
            //    var logger = b.Resolve<ILogger>();
            //    //var context = new NailhubsContext("name=AppContext");
            //    var context = new NailhubsEntities("name=AppContext");
            //    return context;
            //}).InstancePerRequest();

            TNT.App.RegisterContext(() => new NailhubsEntities(TNT.App.NameOrConnectionString));

            TNT.App.RegisterByConfig("autofac");
            
        }
    }
}
