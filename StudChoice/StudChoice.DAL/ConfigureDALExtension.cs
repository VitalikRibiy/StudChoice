using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudChoice.DAL.EF;
using StudChoice.DAL.Repositories.RepositoryImplementations;
using StudChoice.DAL.Repositories.RepositoryInterfaces;
using StudChoice.DAL.UnitOfWork;

namespace StudChoice.DAL
{
    public static class ConfigureDALExtension
    {
        public static void ConfigureDAL(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.ConfigureRepositories();
            services.ConfigureDbContext(configuration);
            services.ConfigureIdentity();
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        }

        private static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<ISubjectRepository, SubjectRepository>();
        }

        private static void ConfigureDbContext(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<StudChoiceContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        private static void ConfigureIdentity(this IServiceCollection services)
        {
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
        }
    }
}
