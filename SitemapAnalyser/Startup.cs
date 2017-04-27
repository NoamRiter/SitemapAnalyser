using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SitemapAnalyser.Startup))]
namespace SitemapAnalyser
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
