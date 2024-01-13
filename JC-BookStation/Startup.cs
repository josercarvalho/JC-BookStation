using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JC_BookStation.Startup))]
namespace JC_BookStation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
