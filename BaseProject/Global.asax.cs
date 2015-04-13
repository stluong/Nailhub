using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TNT.Core.Context;
using TNT.Core.Logging;
//using CFEntity.Models;

using Autofac;
using DFEntity;


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

            //StevenLuong, 04/12/2015: Set connection string for IdentiyContext and NailhubsContext
            TNT.App.NameOrConnectionString = "name=AppContext"; //Default for IdentityContext is AppContext

            TNT.App.RegisterCore(typeof(MvcApplication).Assembly, false);

            TNT.App.Builder.Register<IMyContext>(b =>
            {
                var logger = b.Resolve<ILogger>();
                var context = new NailhubsContext(TNT.App.NameOrConnectionString);
                return context;
            }).InstancePerRequest();
            TNT.App.Builder.Register<IMyContextAsync>(b =>
            {
                var logger = b.Resolve<ILogger>();
                var context = new NailhubsContext(TNT.App.NameOrConnectionString);
                return context;
            }).InstancePerRequest();

            //Or just call register context to register context
            //TNT.App.RegisterContext(() => new NailhubsContext(TNT.App.NameOrConnectionString));

            TNT.App.RegisterByConfig("autofac");
            
        }
    }
}
