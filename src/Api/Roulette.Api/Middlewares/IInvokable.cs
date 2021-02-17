using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Roulette.Api.Middlewares
{
    public interface IInvokable
    {
        Task Invoke(HttpContext context);
    }
}
