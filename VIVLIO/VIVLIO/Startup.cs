using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VIVLIO.Startup))]
namespace VIVLIO
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
