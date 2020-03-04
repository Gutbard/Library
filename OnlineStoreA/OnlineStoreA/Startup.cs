using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineStoreA.Startup))]
namespace OnlineStoreA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
