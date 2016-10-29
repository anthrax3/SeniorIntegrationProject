using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VDNA.Startup))]
namespace VDNA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
