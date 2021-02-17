using FluentValidation;
using Microsoft.AspNetCore.Http;
using Roulette.Api.Models;
namespace Roulette.Api.Validators
{
    public abstract class AbstractModelValidator<T> : AbstractValidator<T> where T : Input
    {
        internal AbstractModelValidator()
        {
            RuleFor(x => x.UserId)
                   .NotNull()
                   .WithSeverity(Severity.Error)
                   .WithErrorCode(StatusCodes.Status400BadRequest.ToString())
                   .WithMessage("El identificador del usuario es un valor requerido");
        }
    }
}
