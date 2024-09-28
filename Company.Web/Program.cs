using Company.Data.Contexts;
using Company.Data.Models;
using Company.Repository.interfaces;
using Company.Repository.Repositories;
using Company.Service.Interfaces;
using Company.Service.Mapping;
using Company.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Company.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.//
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<CompanyDbContext>(op =>
    op.UseSqlServer(builder.Configuration.GetConnectionString("myconn")));

            // builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            //  builder.Services.AddScoped<IDepartmetRepository, DepartmetRepository>()
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped< IEmployeeService,EmployeeService> ();
            builder.Services.AddAutoMapper(x=>x.AddProfile(new EmployeeProfile()));
            builder.Services.AddAutoMapper(x => x.AddProfile(new DepartmentProfile()));
            builder.Services.AddIdentity<ApplicationUser,IdentityRole>(config=>
            {
                config.Password.RequiredUniqueChars = 2;
                config.Password.RequireDigit = true;
                config.Password.RequireLowercase = true;
                config.Password.RequireUppercase = true;
                config.Password.RequireNonAlphanumeric = true;
                config.User.RequireUniqueEmail = true;
                config.Lockout.AllowedForNewUsers = true;
                config.Lockout.MaxFailedAccessAttempts = 3;
                config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(1);

            }).AddEntityFrameworkStores<CompanyDbContext>()
              .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(Option=>
            {
                Option.Cookie.HttpOnly = true;
                Option.ExpireTimeSpan = TimeSpan.FromHours(1);
                Option.SlidingExpiration = true;
                Option.LogoutPath = "/Account//Logout";
                Option.LoginPath= "/Account//Login";
                Option.AccessDeniedPath= "/Account//AcressDenied";    
            });






            //   builder.Services.AddScoped<IGenericRepository<Employee>, GenericRepository<Employee>>();
            // builder.Services.AddScoped<IGenericRepository<Department>, GenericRepository<Department>>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
