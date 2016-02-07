using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MaraudersMapBLL.Startup))]
namespace MaraudersMapBLL
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
