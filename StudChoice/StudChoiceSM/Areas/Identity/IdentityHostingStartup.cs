using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudChoice.DAL.Models;
using StudChoiceSM.Data;

[assembly: HostingStartup(typeof(StudChoiceSM.Areas.Identity.IdentityHostingStartup))]
namespace StudChoiceSM.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<StudChoiceSMContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("DefaultConnection")));

                services.AddDefaultIdentity<StudChoiceSMUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<StudChoiceSMContext>();
            });
        }
    }
}