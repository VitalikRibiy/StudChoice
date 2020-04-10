using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using StudChoice.DAL.EF;
using StudChoice.DAL.Models;
using System.Linq;
using System.Threading.Tasks;

namespace StudChoice.Areas.Identity.Data
{
    public static class DataSeeder
    {
        public static async Task SeedEssentialAsync(this IApplicationBuilder app)
        {
            app.SeedRolesAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            app.SeedUsersAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            await app.SeedData();
        }

        public static async Task SeedRolesAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

            await CreateRoleIfNotExists(roleManager, new IdentityRole<int>("Admin"));
            await CreateRoleIfNotExists(roleManager, new IdentityRole<int>("User"));
        }

        public static async Task SeedUsersAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            #region Users
            await SeedUserAsync(
                userManager,
                new User()
                {
                    Name = "Name",
                    Surname = "Surname",
                    UserName = "admin",
                    Email = "admin@email.com",
                },
                "admin", "Admin");

            await SeedUserAsync(
                userManager,
                new User()
                {
                    Name = "Name",
                    Surname = "Surname",
                    UserName = "user",
                    Email = "user@email.com",
                },
                "user", "User");
            await SeedUserAsync(userManager,
    new User()
    {
        Name = "Name",
        Surname = "Surname",
        UserName = "admin1",
        Email = "admin1@email.com",
    },
    "admin1", "Admin");

            await SeedUserAsync(
                userManager,
                new User()
                {
                    Name = "Name",
                    Surname = "Surname",
                    UserName = "user1",
                    Email = "user1@email.com",
                },
                "user1", "User");
            await SeedUserAsync(userManager,
                new User()
                {
                    Name = "Name",
                    Surname = "Surname",
                    UserName = "admin2",
                    Email = "admin2@email.com",
                },
                "admin2", "Admin");

            await SeedUserAsync(
                userManager,
                new User()
                {
                    Name = "Name",
                    Surname = "Surname",
                    UserName = "user2",
                    Email = "user2@email.com",
                },
                "user2", "User");
            await SeedUserAsync(userManager,
                new User()
                {
                    Name = "Name",
                    Surname = "Surname",
                    UserName = "admin3",
                    Email = "admin3@email.com",
                },
                "admin3", "Admin");
            #endregion
        }

        private static async Task SeedUserAsync(UserManager<User> userManager, User user, string password, string role)
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

        private static async Task CreateRoleIfNotExists(RoleManager<IdentityRole<int>> roleManager, IdentityRole<int> role)
        {
            if (await roleManager.FindByNameAsync(role.Name) == null)
            {
                await roleManager.CreateAsync(role);
            }
        }

        private static async Task SeedData(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<StudChoiceContext>();

            if (context.Subjects.Any())
            {
                return;
            }
            #region Subjects

            var Subject1 = new Subject()
            {
                name = "Subject 1",
                description = "Description 1",
                type = "ДВВС"
            };

            var Subject2 = new Subject()
            {
                name = "Subject 2",
                description = "Description 2",
                type = "ДВ"
            };

            var Subject3 = new Subject()
            {
                name = "Subject 3",
                description = "Description 3",
                type = "ДВВС"
            };

            var Subject4 = new Subject()
            {
                name = "Subject 4",
                description = "Description 4",
                type = "ДВ"
            };

            context.Subjects.AddRange(Subject1, Subject2, Subject3, Subject4);
            context.SaveChanges();
            #endregion
        }
    }
}