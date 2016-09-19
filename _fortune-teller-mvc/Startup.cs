using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_fortune_teller_mvc.Startup))]
namespace _fortune_teller_mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
