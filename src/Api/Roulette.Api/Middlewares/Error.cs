using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Net;
namespace Roulette.Api.Middlewares
{
    public static class Error
    {
        public static HttpError CreateHttpError(IWebHostEnvironment environment, HttpStatusCode status, string code, string[] userMessage, string developerMessage)
        {
            var httpError = CreateHttpError(status, userMessage);
            httpError.Code = code;
            if (environment.IsDevelopment())
            {
                httpError.DeveloperMessage = developerMessage;
            }
            return httpError;
        }
        private static HttpError CreateHttpError(HttpStatusCode status, string[] userMessage)
        {
            return new HttpError
            {
                HttpStatusCode = (int)status,
                Errors = userMessage
            };
        }
        public static HttpError CreateHttpError(HttpStatusCode status, string[] userMessage, string[] validationErrors)
        {
            var httpError = CreateHttpError(status, userMessage);
            httpError.ValidationErrors = validationErrors;
            return httpError;
        }
    }
}
