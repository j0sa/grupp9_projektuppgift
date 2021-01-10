using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(DatingHemsida_Grupp_9.Areas.Identity.IdentityHostingStartup))]

namespace DatingHemsida_Grupp_9.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}