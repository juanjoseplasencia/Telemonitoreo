using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Telemonitoreo.Startup))]
namespace Telemonitoreo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
