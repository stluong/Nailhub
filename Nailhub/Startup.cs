using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Nailhub.Startup))]
namespace Nailhub
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
