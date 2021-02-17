using Roulette.Api.Health;
namespace Microsoft.Extensions.DependencyInjection
{
    public static class HealthCheckExtensions
    {
        public static IServiceCollection AddHealthCheck(this IServiceCollection services)
        {
            services.AddHealthChecks().AddCheck<ApiHealthCheck>("api_health_check");
            return services;
        }
    }
}
