using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace StudChoice.Areas.Identity.Data
{
    public static class SeedExtension
    {
        public static void Seed(
            this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var env = scope.ServiceProvider.GetRequiredService<IHostEnvironment>();

            app.SeedEssentialAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}