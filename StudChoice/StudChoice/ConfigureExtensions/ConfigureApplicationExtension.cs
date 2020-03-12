using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudChoice.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudChoice.ConfigureExtensions
{
    public static class ConfigureApplicationExtension
    {
        public static void ConfigureApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureBLL(configuration);
        }
    }
}
