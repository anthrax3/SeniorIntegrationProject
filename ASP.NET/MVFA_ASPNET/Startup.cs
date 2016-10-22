using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVFA_ASPNET.Startup))]
namespace MVFA_ASPNET
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
