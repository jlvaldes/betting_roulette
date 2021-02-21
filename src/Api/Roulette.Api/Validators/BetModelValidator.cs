using FluentValidation;
using Microsoft.AspNetCore.Http;
using Roulette.Services.Model;

namespace Roulette.Api.Validators
{
    public class BetModelValidator : AbstractValidator<BetInput>
    {
        public BetModelValidator()
        {
            RuleFor(x => x)
                .NotNull()
                .WithSeverity(Severity.Error)
                .WithErrorCode(StatusCodes.Status400BadRequest.ToString())
                .WithMessage("El cuerpo de la petición no puede estar vacío");
            RuleFor(x => x.Number)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(36)
                .WithSeverity(Severity.Error)
                .WithErrorCode(StatusCodes.Status400BadRequest.ToString())
                .WithMessage("El número a apostar debe estar entre 0 y 36");
            RuleFor(x => x.BetAmount)
                .GreaterThan(0)
                .LessThanOrEqualTo(10000)
                .WithSeverity(Severity.Error)
                .WithErrorCode(StatusCodes.Status400BadRequest.ToString())
                .WithMessage("La apuesta debe estar entre 0 y 10.000");
        }
    }
}
