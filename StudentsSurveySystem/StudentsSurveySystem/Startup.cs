using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudentsSurveySystem.Startup))]
namespace StudentsSurveySystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
