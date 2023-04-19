using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace Server.Api
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services, IConfiguration configuration)
        {
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("AppSettings:Token").Value);

            //services.AddLogging(c => c.AddSimpleConsole());

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddAuthorization(options =>
            {
                // those policies can be quite flexible. For example we can check for the current age of the user before allowing him to access.
                options.AddPolicy("RequireAdmin", policy => policy.RequireRole("Admin"));
                options.AddPolicy("RequireModerator", policy => policy.RequireRole("Admin", "Moderator"));
                options.AddPolicy("VipOnly", policy => policy.RequireRole("Admin", "VIP"));
            });

            services.AddControllers(options =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }).AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                // Don't try to follow circular references -> https://dotnetcoretutorials.com/2020/03/15/fixing-json-self-referencing-loop-exceptions/
                opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                // because -> System.Text.Json does not support IDictionary<enum, string> serialize, deserialize
                opt.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader()
                            .AllowAnyMethod()
                            .WithExposedHeaders("WWW-Authenticate")
                            .AllowAnyOrigin();
                    //.WithOrigins("http://localhost:3000")
                });
            });

            return services;
        }
    }
}
