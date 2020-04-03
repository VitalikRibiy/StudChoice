using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using StudChoice.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudChoice.DAL.EF
{
    public class EFDBContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int> // : DbContext
    {
        public EFDBContext(DbContextOptions<EFDBContext> options)
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
        public DbSet<Subject> Subjects;

    }
}
