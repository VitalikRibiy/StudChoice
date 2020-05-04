using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
                    FirstName = "FirstName",
                    LastName = "LastName",
                    MiddleName = "MiddleName",
                    UserName = "admin",
                    Email = "admin@email.com",
                    Course = Course.First,
                    Term = Term.First,
                },
                "admin", "Admin");

            await SeedUserAsync(
                userManager,
                new User()
                {
                    FirstName = "FirstName",
                    LastName = "LastName",
                    MiddleName = "MiddleName",
                    UserName = "user",
                    Email = "user@email.com",
                    FacultyId = 1,
                    CathedraId = 1,
                    Course = Course.First,
                    Term = Term.First,
                    AvaragePoints = 176.0
                },
                "user", "User");

            await SeedUserAsync(
                userManager,
                new User()
                {
                    FirstName = "FirstName",
                    LastName = "LastName",
                    MiddleName = "MiddleName",
                    UserName = "user",
                    Email = "user2@email.com",
                    FacultyId = 1,
                    CathedraId = 1,
                    Course = Course.First,
                    Term = Term.First,
                    AvaragePoints = 160.0
                },
                "user2", "User2");
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

            #region Faculties

            if (!context.Faculties.Any())
            {
                for(int i = 1; i <= 5; i++)
                {
                    var faculty = new Faculty()
                    {
                        DisplayName = $"Faculty{i}",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris imperdiet lobortis lorem et vestibulum. Nulla ac sollicitudin tortor. Nullam gravida posuere aliquet. Ut dictum sodales varius. Nulla imperdiet sagittis neque eget vulputate. Nunc vitae velit quis dui fringilla pretium. Maecenas et sagittis sem. Donec sed odio sed justo tincidunt convallis porttitor eget felis. In sed sem id erat posuere efficitur. Suspendisse dignissim turpis at enim rutrum, eu malesuada dui lacinia. Cras elementum hendrerit gravida. Fusce id velit id augue dictum egestas. Vivamus eget neque eu felis finibus efficitur. Vestibulum non lobortis magna."                        
                    };

                    context.Faculties.Add(faculty);
                }

                context.SaveChanges();
            }

            #endregion

            #region Cathedras

            if (!context.Cathedras.Any())
            {
                for (int i = 1; i <= 5; i++)
                {
                    var cathedra = new Cathedra()
                    {
                        DisplayName = $"Cathedra{i}",
                        Description = $"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris imperdiet lobortis lorem et vestibulum. Nulla ac sollicitudin tortor. Nullam gravida posuere aliquet. Ut dictum sodales varius. Nulla imperdiet sagittis neque eget vulputate. Nunc vitae velit quis dui fringilla pretium. Maecenas et sagittis sem. Donec sed odio sed justo tincidunt convallis porttitor eget felis. In sed sem id erat posuere efficitur. Suspendisse dignissim turpis at enim rutrum, eu malesuada dui lacinia. Cras elementum hendrerit gravida. Fusce id velit id augue dictum egestas. Vivamus eget neque eu felis finibus efficitur. Vestibulum non lobortis magna.",
                        FacultyId = i
                    };

                    context.Cathedras.Add(cathedra);
                }

                context.SaveChanges();
            }

            #endregion

            #region Professors

            if (!context.Professors.Any())
            {
                for (int i = 1; i <= 5; i++)
                {
                    var professor = new Professor()
                    {
                       FirstName = $"FirstName{i}",
                       LastName = $"LastName{i}",
                       MiddleName = $"MiddleName{i}",
                       FacultyId = i,
                       CathedraId = i
                    };

                    context.Professors.Add(professor);
                }

                context.SaveChanges();
            }

            #endregion

            #region Subjects

            if (!context.Subjects.Any())
            {
                var Subject1 = new Subject()
                {
                    Name = "Subject 1",
                    Description = "Description 1",
                    Type = "ДВВС",
                    ProfessorId = context.Professors.FirstOrDefault().Id,
                    FacultyId = context.Faculties.FirstOrDefault().Id,
                    CathedraId = context.Cathedras.FirstOrDefault().Id,
                    MinStudents = 15,
                    MaxStudents = 60,
                    AssignedStudentsCount = 0,
                    Course = Course.First
                };

                var Subject2 = new Subject()
                {
                    Name = "Subject 2",
                    Description = "Description 2",
                    Type = "ДВ",
                    ProfessorId = context.Professors.FirstOrDefault().Id,
                    FacultyId = context.Faculties.FirstOrDefault().Id,
                    CathedraId = context.Cathedras.FirstOrDefault().Id,
                    MinStudents = 20,
                    MaxStudents = 60,
                    AssignedStudentsCount = 0,
                    Course = Course.First
                };

                var Subject3 = new Subject()
                {
                    Name = "Subject 3",
                    Description = "Description 3",
                    Type = "ДВВС",
                    ProfessorId = context.Professors.FirstOrDefault().Id,
                    FacultyId = context.Faculties.FirstOrDefault().Id,
                    CathedraId = context.Cathedras.FirstOrDefault().Id,
                    MinStudents = 30,
                    MaxStudents = 90,
                    AssignedStudentsCount = 0,
                    Course = Course.Second
                };

                var Subject4 = new Subject()
                {
                    Name = "Subject 4",
                    Description = "Description 4",
                    Type = "ДВ",
                    ProfessorId = context.Professors.FirstOrDefault().Id,
                    FacultyId = context.Faculties.FirstOrDefault().Id,
                    CathedraId = context.Cathedras.FirstOrDefault().Id,
                    MinStudents = 20,
                    MaxStudents = 80,
                    AssignedStudentsCount = 0,
                    Course = Course.Second
                };

                context.Subjects.AddRange(Subject1, Subject2, Subject3, Subject4);
                context.SaveChanges();
            }

            #endregion
        }
    }
}