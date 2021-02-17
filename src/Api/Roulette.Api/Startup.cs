using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Roulette.Api.Middlewares;
using Roulette.Data;
using Roulette.Model;
using Roulette.Services;
using Roulette.Services.Services;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Reflection;
namespace Roulette.Api
{
    public class Startup
    {
        private IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_configuration);
            AddOpenApi(services);
            ConfigureApiServices(services, _configuration);
            AddAllDependencies(services);
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
            })
            .UseStaticFiles()
            .UseSwagger()
            .UseSwaggerUI(options =>
            {
                options.ShowExtensions();
                options.DisplayRequestDuration();
                options.DocExpansion(DocExpansion.None);
                options.DocumentTitle = "Roulette API - powered jlvaldes";
                options.EnableDeepLinking();
                options.EnableFilter();
                options.EnableValidator();
                options.SwaggerEndpoint($"/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            _configuration = builder.Build();
        }
        private IServiceCollection AddOpenApi(IServiceCollection services)
        {
            return services.AddSwaggerGen(c =>
            {
                var apiName = "Roulette API";
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = $"{apiName} - powered by jlvaldes",
                    Description = $"{apiName} - powered by jlvaldes",
                    TermsOfService = new Uri("https://github.com/jlvaldes"),
                    Contact = new OpenApiContact
                    {
                        Name = apiName,
                        Email = string.Empty,
                        Url = new Uri("https://github.com/jlvaldes"),
                    }
                });
                var xmlFile = "Roulette.Api.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
        public IServiceCollection ConfigureApiServices(IServiceCollection services, IConfiguration _configuration)
        {
            var apiSettings = _configuration.GetSection(nameof(RouletteSettings)).Get<RouletteSettings>();
            return services
                .AddSingleton<IHttpErrorFactory, HttpErrorFactory>()
                .AddMvcCore()
                .AddApiExplorer()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly())).Services
                .AddApplicationInsightsTelemetry(_configuration)
                .AddCors(o => o.AddPolicy("AllRequests", builder =>
                {
                    foreach (var cors in apiSettings.Cors)
                    {
                        builder.WithOrigins(cors).
                        AllowAnyHeader().
                        AllowAnyMethod();
                    }
                }))
                .AddResponseCompression(options =>
                {
                    options.EnableForHttps = true;
                    options.MimeTypes = new[] { "text/plain", "application/json" };
                })
                .AddHealthCheck();
        }
        private IServiceCollection AddAllDependencies(IServiceCollection services)
        {
            services.AddScoped<IRepository<Model.Roulette>, RouletteMongoDbRepository>();
            services.AddScoped<IRepository<Model.Roulette>, RouletteRedisRepository>();
            services.AddScoped<IRepository<Bet>, BetRedisRepository>();
            services.AddScoped<IRepository<Bet>, BetMongoDbRepository>();
            services.AddSingleton<IRouletteService, RouletteService>();
            services.AddSingleton<IInstanceService, HostedInstanceService>();
            services.AddSingleton<IInstanceService, EmbeddedInstanceService>();
            return services;
        }
    }
}
