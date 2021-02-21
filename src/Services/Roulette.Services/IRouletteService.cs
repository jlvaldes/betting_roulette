using Roulette.Model;
using Roulette.Services.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Roulette.Services
{
    public interface IRouletteService
    {
        Task<OperationDataResult<CreateRouletteResult>> CreateNewRouletteAsync();
        Task<OperationResult> OpenRouletteAsync(string rouletteCode);
        Task<OperationResult> BetAsync(string rouletteCode, string userId, BetInput body);
        Task<OperationDataResult<CloseRouletteResult>> CloseRouletteAsync(string id);
        Task<OperationDataResult<IEnumerable<Roulette.Model.Roulette>>> GetRouletteListAsync();
    }
}
