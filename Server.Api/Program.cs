using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Server.Data;
using Server.Data.Users;

namespace Server.Api
{
    // TODO: Refactor Program.cs and Startup.cs to use only one file.
    // See CleanArchitecture for reference
    // TODO: Extract the rest of the service configuration into proper places > API and Data projects
    // TODO: Check and refactor the Migrate and Seed logic like CleanArchitecture.
    // TODO: Remove /api/ from the route!
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<DataContext>();
                    context.Database.Migrate();
                    var userManager = services.GetRequiredService<UserManager<User>>();
                    var roleManager = services.GetRequiredService<RoleManager<Role>>();
                    Seeder.Initialize(context, userManager, roleManager);
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("An error occurred while seeding the database.: " + ex.Message);
                }
            }

            host.Run();

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("http://localhost:5000");
    }
}
