using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudChoice.DAL.EF;
using StudChoice.DAL.Models;
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
            services.AddScoped<IFacultyRepository, FacultyRepository>();
            services.AddScoped<IProfessorRepository, ProfessorRepository>();
            services.AddScoped<ICathedraRepository, CathedraRepository>();
        }

        private static void ConfigureDbContext(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<StudChoiceContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), x=>x.MigrationsAssembly("StudChoice.DAL")));
        }

        private static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole<int>>(options =>
                        options.SignIn.RequireConfirmedAccount = false)
                    .AddRoleManager<RoleManager<IdentityRole<int>>>()
                    .AddUserManager<UserManager<User>>()
                    .AddSignInManager<SignInManager<User>>()
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
