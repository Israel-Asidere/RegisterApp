using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RegisterApp.Startup))]
namespace RegisterApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
