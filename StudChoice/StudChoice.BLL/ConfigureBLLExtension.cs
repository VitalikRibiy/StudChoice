using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudChoice.BLL.Factories;
using StudChoice.BLL.Mappings.Profiles;
using StudChoice.BLL.Services.Implementations;
using StudChoice.BLL.Services.Interfaces;
using StudChoice.DAL;

namespace StudChoice.BLL
{
    public static class ConfigureBLLExtension
    {
        public static void ConfigureBLL(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureAutoMapper();
            services.ConfigureServices();

            services.AddScoped<IServiceFactory, ServiceFactory>();

            services.ConfigureDAL(configuration);
        }

        private static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<ISubjectService, SubjectService>();
        }

        private static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddSingleton(new MapperConfiguration(c =>
            {
                c.AddProfile(new SubjectProfile());
            }).CreateMapper());
        }
    }
}
