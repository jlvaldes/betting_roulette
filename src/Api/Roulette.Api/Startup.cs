using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Roulette.Api.Middlewares;
using Roulette.Data;
using Roulette.Data.Providers.MongoDb;
using Roulette.Model;
using Roulette.Services;
using Roulette.Services.Services;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
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
            services.Configure<RouletteSettings>(_configuration.GetSection(nameof(RouletteSettings)));
            var rouletteSettings = _configuration.GetSection(nameof(RouletteSettings)).Get<RouletteSettings>();
            if (rouletteSettings.RouletteMongoDbSettings == null || string.IsNullOrEmpty(rouletteSettings.RouletteMongoDbSettings.ConnectionString) ||
                rouletteSettings.BetMongoDbSettings == null || string.IsNullOrEmpty(rouletteSettings.BetMongoDbSettings.ConnectionString))
            {
                throw new Exception("Debe configurar los parámetros de conexión al storage MongoDb en el appsetting");
            }
            services
                .AddMvcCore()
                .AddApiExplorer()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>()).Services
                .AddApplicationInsightsTelemetry(_configuration)
                .AddSwaggerGen(c =>
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
                })
                .AddSingleton<IHttpErrorFactory, HttpErrorFactory>()
                .AddCors(o => o.AddPolicy("AllRequests", builder =>
                {
                    foreach (var cors in rouletteSettings.Cors)
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
                })
                .UseResponseCompression()
                .UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
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
        private IServiceCollection AddAllDependencies(IServiceCollection services)
        {
            services.AddScoped<IMongoDb, MongoDb>();
            services.AddScoped<IBet, Bet>();
            services.AddScoped<IRoulette, Model.Roulette>();
            services.AddScoped<IRepository<Model.Roulette>, RouletteMongoDbRepository>();
            services.AddScoped<IRepository<IRoulette>, RouletteRedisRepository>();
            services.AddScoped<IRepository<IBet>, BetRedisRepository>();
            services.AddScoped<IRepository<Bet>, BetMongoDbRepository>();
            services.AddScoped<IRouletteService, RouletteService>();
            services.AddScoped<IInstanceService, EmbeddedInstanceService>();
            return services;
        }
    }
}
