using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(eBikeWebsite.Startup))]
namespace eBikeWebsite
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
