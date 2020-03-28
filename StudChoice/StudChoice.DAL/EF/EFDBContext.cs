
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using StudChoice.DAL.EF;
using StudChoice.DAL.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace StudChoice.DAL.EF
{
    public class EFDBContext : DbContext // : IdentityDbContext<IdentityUser
    {
        public EFDBContext(DbContextOptions options)
           : base(options)
        {
        }
        public Microsoft.EntityFrameworkCore.DbSet<Subject> Subjects;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Subject>().HasData(new Subject
            {
                id = 1,
                name = "MATAN",
                description = "Matan",
                type = "DVVS"
            });
        }
        //public System.Data.Entity.DbSet<Subject> Subjects { get; set; }

        //static ApplicationContext()
        //{
        //    Database.SetInitializer<ApplicationContext>(new StoreDbInitializer());
        //}
        //public ApplicationContext(string connectionString)
        //    : base(connectionString)
        //{
        //}
       
        //    public DbSet<Subject> Subjects { get; set; }

        //    static EFDBContext()
        //    {
        //        Database.SetInitializer<EFDBContext>(new StoreDbInitializer());
        //    }
        //    public EFDBContext(string connectionString)
        //        : base(connectionString)
        //    {
        //    }
    }
}

    //public class StoreDbInitializer : DropCreateDatabaseIfModelChanges<EFDBContext>
    //{
    //    protected override void Seed(EFDBContext db)
    //    {
    //        db.Subjects.Add(new Subject { name = "First", description = "Desc", type="DV" });
    //        db.Subjects.Add(new Subject { name = "First", description = "Desc", type = "DV" });
    //    db.SaveChanges();
    //    }
    //}


/// <summary>
/// For Migrations
/// </summary>
public class EFDBContextFactory : IDesignTimeDbContextFactory<EFDBContext>
{
    public  EFDBContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<EFDBContext>();
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=StudChoise2;Trusted_Connection=True;MultipleActiveResultSets=true", b => b.MigrationsAssembly("StudChoice1"));

        return new EFDBContext(optionsBuilder.Options);
    }
}


