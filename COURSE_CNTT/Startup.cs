using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(COURSE_CNTT.Startup))]
namespace COURSE_CNTT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
