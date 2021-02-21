using Roulette.Model;
using Roulette.Services.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Roulette.Services
{
    public interface IInstanceService
    {
        ScaleUpStrategy ScaleUpStrategy { get; }
        Task<OperationDataResult<IRoulette>> CreateNewRouletteAsync();
        Task<OperationResult> OpenRouletteAsync(string rouletteCode);
        Task<OperationResult> BetAsync(string rouletteCode, string userId, BetInput body);
        Task<OperationDataResult<CloseRouletteResult>> CloseRouletteAsync(string rouletteCode);
        Task<OperationDataResult<IEnumerable<Roulette.Model.Roulette>>> GetRouletteListAsync();
    }
}
