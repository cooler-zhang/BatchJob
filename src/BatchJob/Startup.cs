using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BatchJob.Startup))]
namespace BatchJob
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
