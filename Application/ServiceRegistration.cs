using Application.Abstractions.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly);
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(QueryTimeLoggingPipelineBehaviour<,>));
            });

            return services;
        }
    }
}