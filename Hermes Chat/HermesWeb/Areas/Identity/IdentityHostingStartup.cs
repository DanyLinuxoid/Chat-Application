using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(hermesChatAlhambra.Areas.Identity.IdentityHostingStartup))]
namespace hermesChatAlhambra.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}