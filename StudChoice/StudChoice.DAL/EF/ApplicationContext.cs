using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using StudChoice.DAL.Models;
using System.Data.Entity;
using DbContext = System.Data.Entity.DbContext;

namespace StudChoice.DAL.EF
{
    public class ApplicationContext: DbContext // : IdentityDbContext<IdentityUser>
    {
        //public ApplicationContext(DbContextOptions options)
        //   : base(options)
        //{
        //}
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
        public System.Data.Entity.DbSet<Subject> Subjects { get; set; }

        static ApplicationContext()
        {
            Database.SetInitializer<ApplicationContext>(new StoreDbInitializer());
        }
        public ApplicationContext(string connectionString)
            : base(connectionString)
        {
        }
    }
    public class StoreDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationContext>
    {
        protected override void Seed(ApplicationContext db)
        {
            db.Subjects.Add(new Subject{ name = "Matanaliz", description = "I Love", type = "DVVS"});
            db.Subjects.Add(new Subject { name = "Tims", description = "TIMS", type = "DV" });
            db.Subjects.Add(new Subject { name = "CHMMF", description = "Slozhno", type = "DVVS" });
            db.Subjects.Add(new Subject { name = "HIstory", description = "History", type = "DV" });
            db.Subjects.Add(new Subject { name = "Math", description = "Math", type = "DVVS" });
            db.SaveChanges();
        }
    }

}

