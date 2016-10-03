using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(COS492_SIP.Startup))]
namespace COS492_SIP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
