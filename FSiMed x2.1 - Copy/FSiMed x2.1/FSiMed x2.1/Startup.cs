using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FSiMed_x2._1.Startup))]
namespace FSiMed_x2._1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
