using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using StudChoice1.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using StudChoice.DAL.EF;
using Ninject.Modules;
using StudChoice1.Util;
using StudChoice.BLL.Infrastructure;
using Ninject;
using System.Web.Mvc;
using Ninject.Web.WebApi;
using StudChoice.BLL.Services.Interfaces;
using StudChoice.BLL.Services;
using StudChoice.DAL.UnitOfWork;

namespace StudChoice1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //var optionsBuilder = new DbContextOptionsBuilder<EFDBContext>();
            //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=StudChoise2;Trusted_Connection=True;MultipleActiveResultSets=true", b => b.MigrationsAssembly("StudChoise.DAL"));

            //services.AddDbContext<EFDBContext>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("DefaultConnection")));
            //var connection = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<EFDBContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            //services.AddDbContext<EFDBContext>(options => options.UseSqlServer(connection, b => b.MigrationsAssembly("StudChoice.DAL")));
            services.AddScoped<ISubjectService, SubjectService>();
            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddControllersWithViews();
            services.AddRazorPages();
  
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
         

        }
    }
}
