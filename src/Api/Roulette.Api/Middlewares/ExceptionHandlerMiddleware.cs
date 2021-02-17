using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Roulette.Exceptions;
using Roulette.Model;
using System;
using System.Threading.Tasks;

namespace Roulette.Api.Middlewares
{
    public sealed class ExceptionHandlerMiddleware : IInvokable
    {
        private readonly RequestDelegate _next;
        private readonly IHttpErrorFactory _httpErrorFactory;
        private readonly ILogger<RouletteLogCategory> _logger;
        private readonly TelemetryClient _telemetryClient;
        public ExceptionHandlerMiddleware(
            RequestDelegate next,
            IHttpErrorFactory httpErrorFactory,
            ILogger<RouletteLogCategory> logger,
            TelemetryClient telemetryClient)
        {
            this._next = next ?? throw new ArgumentNullException(nameof(next));
            this._httpErrorFactory = httpErrorFactory ?? throw new ArgumentNullException(nameof(httpErrorFactory));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._telemetryClient = telemetryClient ?? throw new ArgumentNullException(nameof(telemetryClient));
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (RouletteException exception)
            {
                _telemetryClient.TrackException(exception);
                _logger.LogError(exception.HResult, exception, exception.Message);
                await CreateHttpError(context, exception);
            }
            catch (System.Exception exception)
            {
                _telemetryClient.TrackException(exception);
                _logger.LogError(exception.HResult, exception, exception.Message);
                await CreateHttpError(context, exception);
            }
        }
        private async Task CreateHttpError(HttpContext context, Exception exception)
        {
            HttpError error = exception is RouletteException
                              ? _httpErrorFactory.CreateFromRouletteException(exception as RouletteException)
                              : _httpErrorFactory.CreateFromException(exception);
            await WriteHttpResponseAsync(context, JsonConvert.SerializeObject(error), "application/json", error.HttpStatusCode);
        }
        private Task WriteHttpResponseAsync(HttpContext context, string content, string contentType, int statusCode)
        {
            context.Response.Headers["Content-Type"] = new[] { contentType };
            context.Response.Headers["Cache-Control"] = new[] { "no-cache, no-store, must-revalidate" };
            context.Response.Headers["Pragma"] = new[] { "no-cache" };
            context.Response.Headers["Expires"] = new[] { "0" };
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(content);
        }
    }
}
