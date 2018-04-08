using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_3esi_WebSite.Startup))]
namespace _3esi_WebSite
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
