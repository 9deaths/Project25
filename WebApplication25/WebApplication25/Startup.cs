using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebApplication25.Startup))]
namespace WebApplication25
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
