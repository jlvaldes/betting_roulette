namespace Microsoft.AspNetCore.Builder
{
    public static class HealthApplicationBuilderExtensions
    {
        public static IApplicationBuilder ConfigureHealthCheckEnpoint(this IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
            });
            return app;
        }
    }
}
