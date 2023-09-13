using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Datepicker2.Startup))]
namespace Datepicker2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
