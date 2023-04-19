using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Server.Data.Users;

namespace Server.Data
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {

            IdentityBuilder builder = services.AddIdentityCore<User>(opt =>
            {
                opt.SignIn.RequireConfirmedEmail = false;
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 4;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
            });

            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            builder.AddEntityFrameworkStores<DataContext>();
            builder.AddRoleValidator<RoleValidator<Role>>();
            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<User>>();
            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_";
                options.User.RequireUniqueEmail = true;
            });

            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            }, ServiceLifetime.Transient);
            return services;
        }
    }
}
