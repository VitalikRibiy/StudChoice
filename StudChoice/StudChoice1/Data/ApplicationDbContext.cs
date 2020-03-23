using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudChoice1.Models;

namespace StudChoice1.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }
        //public DbSet<Subject> Subjects;

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);

        //    builder.Entity<Subject>().HasData(new Subject
        //    {
        //        id = 1,
        //        name = "MATAN",
        //        description = "Matan",
        //        type = "DVVS"
        //    });
        //}
    }
  
}
