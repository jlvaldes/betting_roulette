using Microsoft.AspNetCore.Hosting;
using Roulette.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
namespace Roulette.Api.Middlewares
{
    public sealed class HttpErrorFactory : IHttpErrorFactory
    {
        private readonly IWebHostEnvironment _env;
        private readonly IDictionary<Type, Func<Exception, HttpError>> _factory;
        private readonly IDictionary<Type, Func<RouletteException, HttpError>> _factoryException;
        public HttpErrorFactory(IWebHostEnvironment env)
        {
            _env = env;
            _factory = new Dictionary<Type, Func<Exception, HttpError>>
            {
                { typeof(Exception), CreateInternalServerHttpError }
            };
            _factoryException = new Dictionary<Type, Func<RouletteException, HttpError>>
            {
                { typeof(RouletteException), CreateBusinessHttpError },
            };
        }
        public HttpError CreateFromException(Exception exception)
        {
            return _factory.TryGetValue(exception.GetType(), out Func<System.Exception, HttpError> func)
                ? func(exception)
                : _factory[typeof(Exception)](exception);
        }
        public HttpError CreateFromRouletteException(RouletteException exception)
        {
            return _factoryException.TryGetValue(exception.GetType(), out Func<RouletteException, HttpError> func)
                ? func(exception)
                : _factoryException[typeof(RouletteException)](exception);
        }
        private HttpError CreateInternalServerHttpError(Exception exception)
        {
            return Error.CreateHttpError(
                _env,
                status: HttpStatusCode.InternalServerError,
                code: string.Empty,
                userMessage: new[] { "Internal server error" },
                developerMessage: $"{exception.Message}\r\n{exception.StackTrace}");
        }
        private HttpError CreateBusinessHttpError(RouletteException exception)
        {
            return Error.CreateHttpError(
                _env,
                status: HttpStatusCode.OK,
                code: (exception).ErrorCode.ToString(),
                userMessage: new[] { $"{exception.Message}" },
                developerMessage: $"{exception.Message}\r\n{exception.StackTrace}");
        }
    }
}
