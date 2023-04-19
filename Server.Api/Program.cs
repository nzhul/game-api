using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Server.Api;
using Server.Application;
using Server.Common.Middlewares;
using Server.Data;
using Server.Data.Users;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddDataServices(builder.Configuration);
builder.Services.AddAPIServices(builder.Configuration);

builder.Logging.ClearProviders();
builder.Logging.AddSimpleConsole(opt =>
{
    opt.SingleLine = false;
    opt.TimestampFormat = "yyyy/MM/d H:m: ";
    opt.ColorBehavior = LoggerColorBehavior.Enabled;
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
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
    catch (Exception ex)
    {
        Console.WriteLine("An error occurred while seeding the database.: " + ex.Message);
        throw;
    }
}

app.UseCors("CorsPolicy")
    .UseMiddleware<ErrorHandlingMiddleware>()
    .UseRouting()
    .UseAuthentication()
    .UseAuthorization()
    .UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

app.Run();
