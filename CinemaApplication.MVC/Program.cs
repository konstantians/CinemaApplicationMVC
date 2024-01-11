using CinemaApplication.AuthenticationAndAuthorization.Authentication;
using CinemaApplication.AuthenticationAndAuthorization.Authorization;
using CinemaApplication.SharedModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using AuthenticationAndAuthorization.Authentication;
using CinemaApplication.AuthenticationAndAuthorization;

namespace CinemaApplication.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            IConfiguration configuration = builder.Configuration;

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultData"))
            );
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultAuthenticationAndAuthorization"))
            );

            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                //these are all the password options, should be enough
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 3;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;

                //these are all the lockout options. Maybe the only thing that is missing is a mechanism to 
                //increase lockout time in case of multiple lock outs
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

                //options.SignIn.RequireConfirmedEmail = true;
            });

            /*builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy =>
                {
                    policy.RequireRole("Admin");
                });
            });*/


            builder.Services.AddScoped<IAuthenticationProcedures, AuthenticationProcedures>();
            builder.Services.AddScoped<IAuthorizationProcedures, AuthorizationProcedures>();

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}