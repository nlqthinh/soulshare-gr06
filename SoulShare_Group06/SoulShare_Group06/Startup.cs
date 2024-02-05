using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SoulShare_Group06.Startup))]

namespace SoulShare_Group06
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Map the SignalR hub
            app.MapSignalR();
        }
    }
}
