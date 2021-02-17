using Roulette.Exceptions;
namespace Roulette.Api.Middlewares
{
    public interface IHttpErrorFactory
    {
        HttpError CreateFromException(System.Exception exception);
        HttpError CreateFromRouletteException(RouletteException exception);
    }
}
