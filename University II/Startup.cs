using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(University_II.Startup))]
namespace University_II
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
