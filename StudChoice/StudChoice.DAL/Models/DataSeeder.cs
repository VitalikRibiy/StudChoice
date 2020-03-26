using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace StudChoice.Areas.Identity.Data
{
    public static class DataSeeder
    {
        public static async Task SeedEssentialAsync(this IApplicationBuilder app)
        {
            app.SeedRolesAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            app.SeedUsersAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public static async Task SeedRolesAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            await CreateRoleIfNotExists(roleManager, new IdentityRole("Admin"));
            await CreateRoleIfNotExists(roleManager, new IdentityRole("User"));
        }

        public static async Task SeedUsersAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            await SeedUserAsync(
                userManager,
                new IdentityUser()
                {
                    UserName = "admin",
                    Email = "admin@email.com",
                },
                "admin", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser()
                {
                    UserName = "user",
                    Email = "user@email.com",
                },
                "user", "User");

        }

        private static async Task SeedUserAsync(UserManager<IdentityUser> userManager, IdentityUser user, string password, string role)
        {
            if (await userManager.FindByNameAsync(user.UserName) == null)
            {
                var userCreatedResult = await userManager.CreateAsync(user, password);

                if (userCreatedResult.Succeeded)
                    userCreatedResult = await userManager.AddToRoleAsync(user, role);

                if (!userCreatedResult.Succeeded)
                    await userManager.DeleteAsync(user);
            }
        }

        private static async Task CreateRoleIfNotExists(RoleManager<IdentityRole> roleManager, IdentityRole role)
        {
            if (await roleManager.FindByNameAsync(role.Name) == null)
            {
                await roleManager.CreateAsync(role);
            }
        }
    }
}