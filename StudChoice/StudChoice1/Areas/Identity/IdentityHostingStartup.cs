using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudChoice.Areas.Identity;
using StudChoice.Areas.Identity.Data;

[assembly: HostingStartup(typeof(IdentityHostingStartup))]
namespace StudChoice.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<StudChoiceContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("DefaultConnection")));

                services.AddIdentity<IdentityUser<int>, IdentityRole<int>>(options =>
                        options.SignIn.RequireConfirmedAccount = false)
                    .AddRoleManager<RoleManager<IdentityRole<int>>>()
                    .AddUserManager<UserManager<IdentityUser<int>>>()
                    .AddSignInManager<SignInManager<IdentityUser<int>>>()
                    .AddDefaultTokenProviders()
                    .AddEntityFrameworkStores<StudChoiceContext>();
                    

                services
                    .Configure<IdentityOptions>(options =>
                    {
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequireDigit = false;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequiredLength = 1;
                        options.Password.RequiredUniqueChars = 0;
                    });
            });
        }
    }
}