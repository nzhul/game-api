using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Server.Api.Behaviours;
using Server.Application.Features.Auth;
using Server.Application.Mappings;

namespace Server.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingContainer).Assembly);
            services.AddValidatorsFromAssembly(typeof(RegisterCommandValidator).Assembly);
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(LoginHandler).Assembly);
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            });
            return services;
        }
    }
}