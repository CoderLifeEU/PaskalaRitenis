using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PaskalaRitenis.Startup))]
namespace PaskalaRitenis
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
