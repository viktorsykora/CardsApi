using Application.Abstractions;
using Infrastructure.HttpClients.Litacka;
using Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Polly;

namespace Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<LitackaApiSettings>(config.GetSection("LitackaApiSettings"));

            services.AddHttpClient<ICardProvider, LitackaApiClient>((serviceProvider, client) =>
            {
                client.BaseAddress = new Uri(serviceProvider.GetRequiredService<IOptions<LitackaApiSettings>>().Value.ApiUrlBase);
            })
                .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(2)));
            return services;
        }
    }
}