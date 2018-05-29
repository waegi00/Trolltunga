using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Trolltunga.Startup))]
namespace Trolltunga
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
