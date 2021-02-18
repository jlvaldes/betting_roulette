using Roulette.Model;
using Roulette.Services.Model;
using System;
using System.Threading.Tasks;
namespace Roulette.Services
{
    public interface IRouletteService
    {
        Task<OperationDataResult<CreateRouletteResult>> CreateNewRouletteAsync();
        Task<OperationResult> OpenRouletteAsync(Guid id);
        Task<OperationResult> BetAsync(Guid id, Guid userId, BetInput body);
        Task<OperationDataResult<CloseRouletteResult>> CloseRouletteAsync(Guid id);
    }
}
