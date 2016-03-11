using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StarForceMIS.Web.Startup))]
namespace StarForceMIS.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
