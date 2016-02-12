using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IHAComponent.Startup))]
namespace IHAComponent
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
