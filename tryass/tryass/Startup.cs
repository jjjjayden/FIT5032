using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(tryass.Startup))]
namespace tryass
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
