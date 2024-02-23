using Microsoft.EntityFrameworkCore;
using SleepyFruitProject.Data;
using Microsoft.AspNetCore.Identity;

namespace SleepyFruitProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false).AddDefaultUI().AddDefaultTokenProviders()
                .AddEntityFrameworkStores<AppDbContext>();

            builder.Services.AddRazorPages();

			builder.Services.Configure<IdentityOptions>(options =>
			{
				//pasword settings
				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = true;
				options.Password.RequireUppercase = false;
				options.Password.RequiredLength = 10;
				options.Password.RequiredUniqueChars = 1;
				options.Password.RequireNonAlphanumeric = true;
				//lockout
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
				options.Lockout.MaxFailedAccessAttempts = 5;
				options.Lockout.AllowedForNewUsers = false;
				//user
				//options.User.AllowedUserNameCharacters =
				//"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890 -,_@+";
				options.User.RequireUniqueEmail = true;
				options.SignIn.RequireConfirmedAccount = false;
				options.SignIn.RequireConfirmedEmail = false;
				options.SignIn.RequireConfirmedPhoneNumber = false;
			});

			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

			app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();;

            app.UseAuthorization();

            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

			app.MapControllerRoute(
			   name: "FinishLine",
			   pattern: "/finishthetest",
			   defaults: new { controller = "Home", action = "FinishLine" });
			app.Run();
        }
    }
}