using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestChoi.Startup))]
namespace TestChoi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
