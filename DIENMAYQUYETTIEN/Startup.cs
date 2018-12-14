using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DIENMAYQUYETTIEN.Startup))]
namespace DIENMAYQUYETTIEN
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
