using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(lvwei8.MvcStore.Startup))]
namespace lvwei8.MvcStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
