using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TaskWebProject.Startup))]
namespace TaskWebProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
