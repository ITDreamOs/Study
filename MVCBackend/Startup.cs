using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCBackend.Startup))]
namespace MVCBackend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
