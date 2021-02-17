using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
namespace Roulette.Api.Middlewares
{
    public class MiddlewareHostStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return builder =>
            {
                builder.UseMiddleware<ExceptionHandlerMiddleware>();
                next(builder);
            };
        }
    }
}
