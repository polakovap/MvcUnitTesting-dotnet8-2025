using Microsoft.EntityFrameworkCore;
using MvcUnitTesting_dotnet8.Models;
using Tracker.WebAPIClient;

namespace MvcUnitTesting_dotnet8
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ActivityAPIClient.Track(StudentID: "S00239846", StudentName: "Petra Pol�kov�", activityName: "Rad302 2025 Week 3 Lab 3", Task: " Running Week 3 App");

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<BookDbContext>(options =>
                options.UseSqlServer(connectionString));
            // Register the repository as a service
            builder.Services.AddScoped<IRepository<Book>, WorkingBookRepository<Book>>();

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
