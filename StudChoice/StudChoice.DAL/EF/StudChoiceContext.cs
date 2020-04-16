using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudChoice.DAL.Models;

namespace StudChoice.DAL.EF
{
    public class StudChoiceContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        public readonly DbSet<Subject> Subjects;

        public StudChoiceContext(DbContextOptions<StudChoiceContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
