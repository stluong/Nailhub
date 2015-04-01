using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mybrus.Startup))]
namespace Mybrus
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
