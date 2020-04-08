using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using StudChoice.DAL.EF;
using StudChoice.DAL.Models;
using System;
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
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser<int>>>();

            #region Users
            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin",
                    Email = "admin@email.com",
                },
                "admin", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user",
                    Email = "user@email.com",
                },
                "user", "User");
            await SeedUserAsync(userManager,
    new IdentityUser<int>()
    {
        UserName = "admin1",
        Email = "admin1@email.com",
    },
    "admin1", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user1",
                    Email = "user1@email.com",
                },
                "user1", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin2",
                    Email = "admin2@email.com",
                },
                "admin2", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user2",
                    Email = "user2@email.com",
                },
                "user2", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin3",
                    Email = "admin3@email.com",
                },
                "admin3", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user3",
                    Email = "user3@email.com",
                },
                "user3", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin4",
                    Email = "admin4@email.com",
                },
                "admin4", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user4",
                    Email = "user4@email.com",
                },
                "user4", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin5",
                    Email = "admin5@email.com",
                },
                "admin5", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user5",
                    Email = "user5@email.com",
                },
                "user5", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin6",
                    Email = "admin6@email.com",
                },
                "admin6", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user6",
                    Email = "user6@email.com",
                },
                "user6", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin7",
                    Email = "admin7@email.com",
                },
                "admin7", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user7",
                    Email = "user7@email.com",
                },
                "user7", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin8",
                    Email = "admin8@email.com",
                },
                "admin8", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user8",
                    Email = "user8@email.com",
                },
                "user8", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin9",
                    Email = "admin9@email.com",
                },
                "admin9", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user9",
                    Email = "user9@email.com",
                },
                "user9", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin10",
                    Email = "admin10@email.com",
                },
                "admin10", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user10",
                    Email = "user10@email.com",
                },
                "user10", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin11",
                    Email = "admin11@email.com",
                },
                "admin11", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user11",
                    Email = "user11@email.com",
                },
                "user11", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin12",
                    Email = "admin12@email.com",
                },
                "admin12", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user12",
                    Email = "user12@email.com",
                },
                "user12", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin13",
                    Email = "admin13@email.com",
                },
                "admin13", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user13",
                    Email = "user13@email.com",
                },
                "user13", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin14",
                    Email = "admin14@email.com",
                },
                "admin14", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user14",
                    Email = "user14@email.com",
                },
                "user14", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin15",
                    Email = "admin15@email.com",
                },
                "admin15", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user15",
                    Email = "user15@email.com",
                },
                "user15", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin16",
                    Email = "admin16@email.com",
                },
                "admin16", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user16",
                    Email = "user16@email.com",
                },
                "user16", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin17",
                    Email = "admin17@email.com",
                },
                "admin17", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user17",
                    Email = "user17@email.com",
                },
                "user17", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin18",
                    Email = "admin18@email.com",
                },
                "admin18", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user18",
                    Email = "user18@email.com",
                },
                "user18", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin19",
                    Email = "admin19@email.com",
                },
                "admin19", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user19",
                    Email = "user19@email.com",
                },
                "user19", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin20",
                    Email = "admin20@email.com",
                },
                "admin20", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user20",
                    Email = "user20@email.com",
                },
                "user20", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin21",
                    Email = "admin21@email.com",
                },
                "admin21", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user21",
                    Email = "user21@email.com",
                },
                "user21", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin22",
                    Email = "admin22@email.com",
                },
                "admin22", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user22",
                    Email = "user22@email.com",
                },
                "user22", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin23",
                    Email = "admin23@email.com",
                },
                "admin23", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user23",
                    Email = "user23@email.com",
                },
                "user23", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin24",
                    Email = "admin24@email.com",
                },
                "admin24", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user24",
                    Email = "user24@email.com",
                },
                "user24", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin25",
                    Email = "admin25@email.com",
                },
                "admin25", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user25",
                    Email = "user25@email.com",
                },
                "user25", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin26",
                    Email = "admin26@email.com",
                },
                "admin26", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user26",
                    Email = "user26@email.com",
                },
                "user26", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin27",
                    Email = "admin27@email.com",
                },
                "admin27", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user27",
                    Email = "user27@email.com",
                },
                "user27", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin28",
                    Email = "admin28@email.com",
                },
                "admin28", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user28",
                    Email = "user28@email.com",
                },
                "user28", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin29",
                    Email = "admin29@email.com",
                },
                "admin29", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user29",
                    Email = "user29@email.com",
                },
                "user29", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin30",
                    Email = "admin30@email.com",
                },
                "admin30", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user30",
                    Email = "user30@email.com",
                },
                "user30", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin31",
                    Email = "admin31@email.com",
                },
                "admin31", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user31",
                    Email = "user31@email.com",
                },
                "user31", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin32",
                    Email = "admin32@email.com",
                },
                "admin32", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user32",
                    Email = "user32@email.com",
                },
                "user32", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin33",
                    Email = "admin33@email.com",
                },
                "admin33", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user33",
                    Email = "user33@email.com",
                },
                "user33", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin34",
                    Email = "admin34@email.com",
                },
                "admin34", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user34",
                    Email = "user34@email.com",
                },
                "user34", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin35",
                    Email = "admin35@email.com",
                },
                "admin35", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user35",
                    Email = "user35@email.com",
                },
                "user35", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin36",
                    Email = "admin36@email.com",
                },
                "admin36", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user36",
                    Email = "user36@email.com",
                },
                "user36", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin37",
                    Email = "admin37@email.com",
                },
                "admin37", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user37",
                    Email = "user37@email.com",
                },
                "user37", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin38",
                    Email = "admin38@email.com",
                },
                "admin38", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user38",
                    Email = "user38@email.com",
                },
                "user38", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin39",
                    Email = "admin39@email.com",
                },
                "admin39", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user39",
                    Email = "user39@email.com",
                },
                "user39", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin40",
                    Email = "admin40@email.com",
                },
                "admin40", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user40",
                    Email = "user40@email.com",
                },
                "user40", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin41",
                    Email = "admin41@email.com",
                },
                "admin41", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user41",
                    Email = "user41@email.com",
                },
                "user41", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin42",
                    Email = "admin42@email.com",
                },
                "admin42", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user42",
                    Email = "user42@email.com",
                },
                "user42", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin43",
                    Email = "admin43@email.com",
                },
                "admin43", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user43",
                    Email = "user43@email.com",
                },
                "user43", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin44",
                    Email = "admin44@email.com",
                },
                "admin44", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user44",
                    Email = "user44@email.com",
                },
                "user44", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin45",
                    Email = "admin45@email.com",
                },
                "admin45", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user45",
                    Email = "user45@email.com",
                },
                "user45", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin46",
                    Email = "admin46@email.com",
                },
                "admin46", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user46",
                    Email = "user46@email.com",
                },
                "user46", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin47",
                    Email = "admin47@email.com",
                },
                "admin47", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user47",
                    Email = "user47@email.com",
                },
                "user47", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin48",
                    Email = "admin48@email.com",
                },
                "admin48", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user48",
                    Email = "user48@email.com",
                },
                "user48", "User");
            await SeedUserAsync(userManager,
                new IdentityUser<int>()
                {
                    UserName = "admin49",
                    Email = "admin49@email.com",
                },
                "admin49", "Admin");

            await SeedUserAsync(
                userManager,
                new IdentityUser<int>()
                {
                    UserName = "user49",
                    Email = "user49@email.com",
                },
                "user49", "User");
#endregion
        }

        private static async Task SeedUserAsync(UserManager<IdentityUser<int>> userManager, IdentityUser<int> user, string password, string role)
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

            #endregion
        }
    }
}