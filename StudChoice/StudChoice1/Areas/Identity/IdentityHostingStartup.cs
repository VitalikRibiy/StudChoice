using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudChoice1.Data;

[assembly: HostingStartup(typeof(StudChoice1.Areas.Identity.IdentityHostingStartup))]
namespace StudChoice1.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<StudChoiceContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("StudChoiceContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddEntityFrameworkStores<StudChoiceContext>();
            });
        }
    }
}