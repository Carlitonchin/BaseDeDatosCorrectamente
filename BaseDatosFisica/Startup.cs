using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BaseDatosFisica.Startup))]
namespace BaseDatosFisica
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
