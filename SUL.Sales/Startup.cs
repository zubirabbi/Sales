using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SUL.Sales.Startup))]
namespace SUL.Sales
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
