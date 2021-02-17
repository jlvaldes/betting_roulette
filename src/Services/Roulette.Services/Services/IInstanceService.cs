using Roulette.Model;
using Roulette.Services.Model;
using System;
using System.Threading.Tasks;
namespace Roulette.Services
{
    public interface IInstanceService
    {
        ScaleUpStrategy ScaleUpStrategy { get; }
        Task<OperationDataResult<IRoulette>> CreateNewRouletteAsync();
        Task<OperationResult> OpenRouletteAsync(Guid id);
        Task<OperationResult> BetAsync(Guid id, Guid userId, BetInput body);
    }
}
